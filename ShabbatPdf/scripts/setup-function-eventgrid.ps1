# Wire Event Grid blob events -> Function EventGridTriggers.
# Flex Consumption does not poll containers; without these subscriptions the functions never run.
#
# Two subscriptions (Azure Function destination — not webhook):
#   shabbat-service-staging -> CompressStagingPdf
#   shabbat-service         -> ProcessShabbatPdf (teaching extract)
#
# Webhook endpoint-type fails Flex handshake validation
# (Http POST response code Unknown). The working production
# subscription used --endpoint-type azurefunction.
#
# Prerequisites: az login, function already deployed (both CompressStagingPdf
# and ProcessShabbatPdf must exist on the app).
#
# From ShabbatPdf product root:
#   .\scripts\setup-function-eventgrid.ps1

param(
    [string] $AppName = "lmm-shabbat-pdf",
    [string] $ResourceGroup = "LmmWebAppGroup",
    [string] $StorageAccount = "livingmessiahstorage",
    [string] $StagingContainer = "shabbat-service-staging",
    [string] $ServiceContainer = "shabbat-service",
    [string] $CompressFunction = "CompressStagingPdf",
    [string] $ExtractFunction = "ProcessShabbatPdf"
)

$ErrorActionPreference = "Stop"
if (Get-Variable -Name PSNativeCommandUseErrorActionPreference -ErrorAction SilentlyContinue) {
    $PSNativeCommandUseErrorActionPreference = $false
}

Write-Host "Ensuring Microsoft.EventGrid provider is registered ..." -ForegroundColor Cyan
az provider register --namespace Microsoft.EventGrid --wait 2>$null

$storageId = az storage account show -n $StorageAccount -g $ResourceGroup --query id -o tsv
if ([string]::IsNullOrWhiteSpace($storageId)) {
    throw "Storage account '$StorageAccount' not found in resource group '$ResourceGroup'."
}

function Invoke-AzJson {
    param([Parameter(ValueFromRemainingArguments = $true)][string[]] $AzArgs)
    $prev = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    $out = & az @AzArgs 2>$null
    $code = $LASTEXITCODE
    $ErrorActionPreference = $prev
    return @{ ExitCode = $code; Text = ($out | Out-String) }
}

function New-BlobCreatedSubscription {
    param(
        [string] $ContainerName,
        [string] $FunctionName,
        [string] $SubscriptionName
    )

    $functionId = az functionapp function show `
        -g $ResourceGroup `
        -n $AppName `
        --function-name $FunctionName `
        --query id -o tsv
    if ([string]::IsNullOrWhiteSpace($functionId)) {
        throw "Function '$FunctionName' not found on $AppName. Deploy the Functions project first."
    }

    $subjectBegins = "/blobServices/default/containers/$ContainerName/blobs/"

    Write-Host "Creating/updating Event Grid subscription '$SubscriptionName' ($ContainerName -> $FunctionName) ..." -ForegroundColor Cyan
    Write-Host "  Function id: $functionId" -ForegroundColor DarkGray

    $show = Invoke-AzJson eventgrid event-subscription show `
        --source-resource-id $storageId `
        --name $SubscriptionName `
        -o json
    if ($show.ExitCode -eq 0 -and -not [string]::IsNullOrWhiteSpace($show.Text)) {
        Write-Host "Removing existing subscription ..." -ForegroundColor Yellow
        $del = Invoke-AzJson eventgrid event-subscription delete `
            --source-resource-id $storageId `
            --name $SubscriptionName
        if ($del.ExitCode -ne 0) {
            throw "Failed to delete existing Event Grid subscription '$SubscriptionName'."
        }
    } else {
        Write-Host "No existing subscription '$SubscriptionName' (will create)." -ForegroundColor DarkGray
    }

    $create = Invoke-AzJson eventgrid event-subscription create `
        --name $SubscriptionName `
        --source-resource-id $storageId `
        --endpoint $functionId `
        --endpoint-type azurefunction `
        --included-event-types Microsoft.Storage.BlobCreated `
        --subject-begins-with $subjectBegins `
        --subject-ends-with ".pdf" `
        --max-delivery-attempts 10 `
        --event-delivery-schema EventGridSchema `
        -o table

    $text = $create.Text
    $failed = ($create.ExitCode -ne 0) -or
        ($text -match 'handshake failed') -or
        ($text -match 'URL validation') -or
        ($text -match '(?i)\bERROR:')

    if ($failed) {
        Write-Host $text
        Write-Host ""
        Write-Host "CLI create failed. Create the subscription in Portal instead:" -ForegroundColor Yellow
        Write-Host "  1. Portal -> storage account livingmessiahstorage -> Events -> + Event Subscription"
        Write-Host "  2. Name: $SubscriptionName"
        Write-Host "  3. Event Types: Blob Created"
        Write-Host "  4. Endpoint Type: Azure Function (not Web Hook)"
        Write-Host "  5. Function app: $AppName  Function: $FunctionName"
        Write-Host "  6. Filters: Subject begins with $subjectBegins , ends with .pdf"
        throw "Event Grid subscription create failed for $SubscriptionName."
    }

    Write-Host $text
}

New-BlobCreatedSubscription `
    -ContainerName $StagingContainer `
    -FunctionName $CompressFunction `
    -SubscriptionName "lmm-shabbat-pdf-staging-created"

New-BlobCreatedSubscription `
    -ContainerName $ServiceContainer `
    -FunctionName $ExtractFunction `
    -SubscriptionName "lmm-shabbat-pdf-blob-created"

Write-Host "Done. Uploads to $StagingContainer -> $CompressFunction -> $ServiceContainer -> $ExtractFunction." -ForegroundColor Green
Write-Host "Tip: upload a test PDF to $StagingContainer to fire the chain." -ForegroundColor Yellow

#Requires -Version 5.1
<#
.SYNOPSIS
  Export paid live Stripe Checkout Sessions that came from Sukkot (not PWA donations).

.DESCRIPTION
  Uses Stripe CLI (must be logged in: stripe login).
  Keeps sessions where success_url contains sukkot.livingmessiah.com
  OR metadata.registrationId is present.
  Writes CSV to Downloads by default.

.EXAMPLE
  .\scripts\Export-SukkotStripeSessions.ps1

.EXAMPLE
  .\scripts\Export-SukkotStripeSessions.ps1 -Limit 200 -OutPath "$env:USERPROFILE\Desktop\sukkot.csv"
#>
[CmdletBinding()]
param(
	[int] $Limit = 100,
	[string] $OutPath = (Join-Path $env:USERPROFILE "Downloads\sukkot-stripe-sessions.csv")
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command stripe -ErrorAction SilentlyContinue)) {
	throw "Stripe CLI not found. Install from https://stripe.com/docs/stripe-cli and run: stripe login"
}

Write-Host "Fetching up to $Limit live completed Checkout Sessions..."
$json = stripe checkout sessions list --live --limit $Limit --status complete
if ($LASTEXITCODE -ne 0) {
	throw "stripe checkout sessions list failed (exit $LASTEXITCODE). Try: stripe login"
}

$obj = $json | ConvertFrom-Json
if (-not $obj.data) {
	Write-Warning "No sessions returned."
	return
}

$rows = foreach ($s in $obj.data) {
	$regId = $s.metadata.registrationId
	$isSukkot =
		($s.success_url -like "*sukkot.livingmessiah.com*") -or
		(-not [string]::IsNullOrWhiteSpace($regId))

	if (-not $isSukkot) { continue }
	if ($s.payment_status -ne "paid") { continue }

	[pscustomobject]@{
		RegistrationId = $regId
		Email          = $s.customer_email
		AmountUsd      = if ($null -ne $s.amount_total) { [decimal]$s.amount_total / 100 } else { $null }
		ReferenceId    = $s.id
		CreatedUtc     = ([DateTimeOffset]::FromUnixTimeSeconds([int64]$s.created)).UtcDateTime.ToString("u")
		SuccessUrl     = $s.success_url
		PaymentStatus  = $s.payment_status
	}
}

$dir = Split-Path -Parent $OutPath
if ($dir -and -not (Test-Path $dir)) {
	New-Item -ItemType Directory -Path $dir | Out-Null
}

$rows | Export-Csv -Path $OutPath -NoTypeInformation -Encoding UTF8
Write-Host "Wrote $($rows.Count) Sukkot row(s) to $OutPath"
$rows | Format-Table -AutoSize

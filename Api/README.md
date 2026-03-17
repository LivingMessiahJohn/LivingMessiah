# Azure Functions API for Living Messiah PWA

This folder contains Azure Functions that provide backend API functionality for the Blazor WebAssembly PWA deployed as an Azure Static Web App.

## Project Structure

```
Api/
├── Functions/
│   └── BlobInfoFunction.cs      # HTTP function to check blob existence and get info
├── Models/
│   ├── BlobInfo.cs              # Blob information model
│   ├── BlobOperationResult.cs   # Operation result wrapper
│   └── BlobInfoRequest.cs       # Request/response models
├── Api.csproj                   # Project file
├── host.json                    # Function host configuration
├── local.settings.json          # Local development settings (not committed)
└── Program.cs                   # Function app startup

```

## Functions

### GetBlobInfo
- **Endpoint**: `POST /api/blob-info`
- **Purpose**: Checks if a blob exists in Azure Storage and retrieves its metadata
- **Request Body**:
  ```json
  {
    "blobName": "example.pdf"
  }
  ```
- **Response**:
  ```json
  {
    "exists": true,
    "blobInfo": {
      "name": "example.pdf",
      "url": "https://...blob.core.windows.net/.../example.pdf",
      "sizeBytes": 12345
    },
    "message": "Blob info retrieved successfully",
    "isTransient": false
  }
  ```

## Local Development

### Prerequisites
- .NET 10 SDK
- Azure Functions Core Tools v4
- Azure Storage Account (or use Azurite for local testing)

### Configuration

1. Update `local.settings.json` with your Azure Storage credentials:
   ```json
   {
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
       "AzureStorageConnectionString": "YOUR_CONNECTION_STRING",
       "BlobContainerName": "YOUR_CONTAINER_NAME"
     }
   }
   ```

2. Run the function locally:
   ```bash
   cd Api
   func start
   ```

### Testing Locally

The function will be available at `http://localhost:7071/api/blob-info`. You can test it using:

```bash
curl -X POST http://localhost:7071/api/blob-info \
  -H "Content-Type: application/json" \
  -d '{"blobName": "your-file.pdf"}'
```

## Deployment to Azure Static Web Apps

### Via Azure Portal

1. **Navigate to your Static Web App** in Azure Portal
2. **Go to Configuration** > Application Settings
3. **Add these settings**:
   - `AzureStorageConnectionString`: Your Azure Storage connection string
   - `BlobContainerName`: Your blob container name

### Via Azure CLI

```bash
# Set Static Web App name and resource group
STATIC_WEB_APP="your-static-web-app-name"
RESOURCE_GROUP="your-resource-group"

# Add application settings
az staticwebapp appsettings set \
  --name $STATIC_WEB_APP \
  --resource-group $RESOURCE_GROUP \
  --setting-names \
    AzureStorageConnectionString="YOUR_CONNECTION_STRING" \
    BlobContainerName="YOUR_CONTAINER_NAME"
```

### GitHub Actions Deployment

Azure Static Web Apps automatically deploys the API when you push to your repository. Ensure your GitHub Actions workflow includes the `api_location` parameter:

```yaml
- name: Build And Deploy
  uses: Azure/static-web-apps-deploy@v1
  with:
    azure_static_web_apps_api_token: ${{ secrets.AZURE_STATIC_WEB_APPS_API_TOKEN }}
    repo_token: ${{ secrets.GITHUB_TOKEN }}
    action: "upload"
    app_location: "/PWA"          # Blazor WASM project location
    api_location: "/Api"           # Azure Functions project location
    output_location: "wwwroot"
```

## PWA Integration

The PWA calls this API using the `BlobApiService` class located in:
- `PWA/Features/Home/WeeklyDownload/BlobApiService.cs`

The service is registered in `Program.cs`:
```csharp
builder.Services.AddBlobApiService();
```

And used in components like:
- `PWA/Features/Home/WeeklyDownload/PdfButtonCardFooter.razor`

## Security Considerations

1. **Authorization**: Currently set to `Anonymous` for the Static Web App integration. Azure Static Web Apps provides built-in authentication.
2. **CORS**: Automatically configured when deployed as part of a Static Web App.
3. **Secrets**: Never commit `local.settings.json` or connection strings to source control.
4. **Storage Access**: The function uses a connection string with appropriate permissions. Consider using Managed Identity in production.

## Monitoring

- **Application Insights**: Configured via the `host.json` and NuGet packages
- **Logs**: View logs in Azure Portal under your Static Web App > Functions > Monitor

## Troubleshooting

### Function not accessible
- Verify the API is deployed: Check the GitHub Actions deployment logs
- Check Static Web App configuration: Ensure `api_location` is set correctly

### Blob access errors
- Verify connection string is configured in Application Settings
- Check container name is correct
- Ensure storage account firewall allows access

### CORS issues
- Static Web Apps handle CORS automatically for the `/api` path
- For local testing, CORS is not enforced by default

## References

- [Azure Static Web Apps Documentation](https://docs.microsoft.com/en-us/azure/static-web-apps/)
- [Azure Functions .NET Isolated Worker](https://docs.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide)
- [Azure Blob Storage SDK](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet)

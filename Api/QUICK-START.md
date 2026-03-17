# Quick Start Guide - Azure Functions API

## Prerequisites
- Azure Functions Core Tools: `npm install -g azure-functions-core-tools@4 --unsafe-perm true`
- Or install via: https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local

## Local Development Setup

### Step 1: Configure Local Settings
Edit `Api/local.settings.json`:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "AzureStorageConnectionString": "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net",
    "BlobContainerName": "YOUR_CONTAINER_NAME"
  }
}
```

### Step 2: Run the Azure Function Locally

```bash
cd Api
func start
```

Expected output:
```
Azure Functions Core Tools
Core Tools Version:       4.x.x
Function Runtime Version: 4.x.x

Functions:
    GetBlobInfo: [POST] http://localhost:7071/api/blob-info
```

### Step 3: Run the Blazor PWA

In a new terminal:
```bash
cd PWA
dotnet run
```

The PWA is configured to call `http://localhost:7071/api/blob-info` when in Development mode.

## Testing the Integration

### Test the Function Directly
```bash
curl -X POST http://localhost:7071/api/blob-info \
  -H "Content-Type: application/json" \
  -d "{\"blobName\": \"test.pdf\"}"
```

### Test from the PWA
1. Navigate to the page that uses `PdfButtonCardFooter`
2. Open browser DevTools (F12) > Network tab
3. Look for POST request to `/api/blob-info`
4. Check the Console for log messages

## Deployment to Azure Static Web Apps

### Prerequisites
- Azure Static Web App already created
- GitHub repository connected

### Step 1: Update GitHub Actions Workflow

Your workflow file should include:
```yaml
api_location: "/Api"
app_location: "/PWA"
output_location: "wwwroot"
```

### Step 2: Configure Azure Application Settings

Via Azure Portal:
1. Go to your Static Web App
2. Settings > Configuration
3. Add Application Settings:
   - `AzureStorageConnectionString` = [Your connection string]
   - `BlobContainerName` = [Your container name]

Via Azure CLI:
```bash
az staticwebapp appsettings set \
  --name your-static-web-app \
  --setting-names \
    AzureStorageConnectionString="YOUR_CONNECTION_STRING" \
    BlobContainerName="YOUR_CONTAINER"
```

### Step 3: Deploy
```bash
git add .
git commit -m "Add Azure Functions API"
git push
```

GitHub Actions will automatically build and deploy both the PWA and API.

## Verification

### Production URL
After deployment, your API will be available at:
```
https://your-app.azurestaticapps.net/api/blob-info
```

### Test Production
```bash
curl -X POST https://your-app.azurestaticapps.net/api/blob-info \
  -H "Content-Type: application/json" \
  -d "{\"blobName\": \"test.pdf\"}"
```

## Troubleshooting

### Function not starting locally
- Ensure Azure Functions Core Tools is installed
- Check that .NET 10 SDK is installed: `dotnet --version`
- Verify `local.settings.json` exists and is valid JSON

### PWA can't reach the API locally
- Ensure the function is running on port 7071
- Check `PWA/Program.cs` - should use `http://localhost:7071` in development
- Check browser console for CORS errors (shouldn't occur locally)

### Blob access errors
- Verify your connection string is correct
- Check the container name matches
- Ensure the container has public read access or proper authentication

### API not available after deployment
- Check GitHub Actions deployment logs
- Verify `api_location` is set to `/Api` in the workflow
- Ensure Application Settings are configured in Azure

## Common Issues

1. **"FUNCTIONS_WORKER_RUNTIME" missing**
   - Solution: Check `local.settings.json` has all required values

2. **Connection string errors**
   - Solution: Verify connection string format is correct
   - Try using Azure Storage Explorer to get the connection string

3. **Container not found**
   - Solution: Create the container in Azure Storage
   - Ensure the container name matches exactly (case-sensitive)

4. **CORS errors in browser**
   - Development: Shouldn't occur with Azure Functions locally
   - Production: Shouldn't occur with Static Web Apps (automatic CORS)
   - If it does: Check that you're using the correct base address

## Additional Resources

- [Azure Functions Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)
- [Azure Static Web Apps with Functions](https://docs.microsoft.com/en-us/azure/static-web-apps/apis)
- [Azure Blob Storage .NET SDK](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet)

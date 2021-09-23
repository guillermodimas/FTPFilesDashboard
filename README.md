# FTPFilesDashboard
Blazor Server App to locate and retrieve archived recordings from an FTP location.  The file is then temporarily uploaded to Azure as a blob, giving users the ability to share the file. 
![FTPFilesDashboard_Demo](https://user-images.githubusercontent.com/5040055/134526032-dd65fd4e-b08a-4a8e-839d-642e866fa5ab.gif)

# Dependencies
### SQL Server Configuration

Restore the SQL Server Database from the `ZoomDownloadsDatabase` project. 
Enter the SQL Server connection string in `appsettings.json`
```json
{

  "ConnectionStrings": {
    "ZoomDownloadsData": "INSERT SQL CONNECTION STRING HERE"
  }
  
}
```
### Azure Storage / Blob Container
#### 1. Create an Azure Storage Account
#### 2. Create a Container, make sure its public access level is "Blob"
#### 3. Make note of the Connection String under "Security + networking" and "Access Keys"
![image](https://user-images.githubusercontent.com/5040055/134527791-a33ac0e6-5e14-47c0-8912-c1b2f66fccd0.png)
#### 4. Enter the Azure Storage information in `appsettings.json`
```json
{
   "StorageConnectionString": "DefaultEndpointsProtocol=https;AccountName=INSERTSTORAGEACCOUNTNAMEHERE;AccountKey=INSERTYOURKEYHERE;EndpointSuffix=core.windows.net",
   "StorageBaseUrl": "https://STORAGEACCOUNTNAME.blob.core.windows.net/",
   "ContainerName": "YOUR_BLOB_CONTAINER_NAME",
}
```

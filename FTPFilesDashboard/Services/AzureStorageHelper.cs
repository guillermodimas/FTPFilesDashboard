using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Blazorise;
using Microsoft.Extensions.Configuration;

namespace FTPFilesDashboard.Services
{
    public class AzureStorageHelper
    {
        IConfiguration configuration;
        string baseUrl = "";
        string containerName = "";
        long uploadFileSize;

        public AzureStorageHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
            baseUrl = configuration["StorageBaseUrl"];
            containerName = configuration["ContainerName"];
        }

        public async Task<List<string>> GetFileList()
        {
            var files = new List<string>();
            var container = OpenContianer();
            if (container == null) return files;

            try
            {
                // get the list
                await foreach (BlobItem item in container.GetBlobsAsync())
                {
                    var Url = container.Uri.ToString() + "/" + item.Name.ToString();
                    files.Add(Url);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return files;
        }

        public async Task<string> UploadFile(string sourceFilename, string destFileName, bool overWrite)
        {
            var container = OpenContianer();
            var progressHandler = new Progress<long>();
            progressHandler.ProgressChanged += UploadProgressChanged;

            if (container == null) return "";
            try
            {
                var file = new FileInfo(sourceFilename);
                uploadFileSize = file.Length;

                // Specify the StorageTransferOptions
                BlobUploadOptions options = new BlobUploadOptions
                {
                    TransferOptions = new StorageTransferOptions
                    {
                        // Set the maximum length of a transfer to 50MB.
                        // If the file is bigger than 50MB it will be sent in 50MB chunks.
                        MaximumTransferSize = 50 * 1024 * 1024
                    },
                    ProgressHandler = progressHandler
                };

                BlobClient blob = container.GetBlobClient(destFileName);

                if (overWrite == true)
                {
                    blob.DeleteIfExists();
                }



                //await blob.UploadAsync(sourceFilename, options);
                using (Stream sr = File.OpenRead(sourceFilename))
                {
                    await container.UploadBlobAsync(destFileName, sr);
                }
                //using FileStream uploadFileStream = File.OpenRead(sourceFilename);

                //await blob.UploadAsync(uploadFileStream, options);

                //uploadFileStream.Close();
                // return the url to the blob
                return $"{baseUrl}{containerName}\\{ destFileName}";
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return "";
            }

        }
        private double GetProgressPercentage(double totalSize, double currentSize)
        {
            return (currentSize / totalSize) * 100;
        }
        private void UploadProgressChanged(object sender, long bytesUploaded)
        {
            var current = (int)GetProgressPercentage(uploadFileSize, bytesUploaded);

        }
        public async Task<string> DownloadFile(string sourceFilename, string destFileName)
        {
            var container = OpenContianer();
            if (container == null) return "";

            try
            {
                BlobClient blob = container.GetBlobClient(sourceFilename);

                BlobDownloadInfo download = await blob.DownloadAsync();

                using (FileStream downloadFileStream = File.OpenWrite(destFileName))
                {
                    await download.Content.CopyToAsync(downloadFileStream);
                    downloadFileStream.Close();
                }
                return "OK";
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return "";
            }
        }

        public BlobContainerClient OpenContianer()
        {
            try
            {
                string setting = configuration["StorageConnectionString"];

                // Create a BlobServiceClient object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(setting);

                // Create the container and return a container client object
                return blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}

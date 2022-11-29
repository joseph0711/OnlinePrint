using Microsoft.Azure;
using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Storage;

namespace OnlinePrint.Service
{
    public class BlobService
    {
        static string account = CloudConfigurationManager.GetSetting("StorageAccountName");
        static string key = CloudConfigurationManager.GetSetting("StorageAccountKey");
        static string connectionString;

        //To Get Blob ConnectionString.
        public static string GetConnectionString()
        {
            connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            return connectionString;
        }

        private string GetBlobSasUrl(string containerName, string blobName)
        {
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTime.UtcNow.AddMinutes(-2),
                ExpiresOn = DateTime.UtcNow.AddMinutes(10),
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Create |
                                        BlobSasPermissions.Read);

            var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(account, key));

            GetConnectionString();
            return $"{new BlobClient(connectionString, containerName, blobName).Uri}?{sasToken}";
        }

        //Upload file.
        public string UploadBlob(string containerName, string blobName, Stream fileToBeUploaded)
        {
            string sasUri = GetBlobSasUrl(containerName, blobName);
            var blobClient = new BlobClient(new Uri(sasUri));
            blobClient.Upload(fileToBeUploaded);

            return blobClient.Uri.ToString();
        }

        //Download file.
        public string DownloadBlob(string containerName, string blobName)
        {
            string sasUri = GetBlobSasUrl(containerName, blobName);
            return sasUri;
        }
    }
}
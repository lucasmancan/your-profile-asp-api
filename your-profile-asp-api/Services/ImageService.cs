using aspApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Services
{

    public class ImageService
    {
        private readonly IConfiguration _config;

        public ImageService(IConfiguration config)
        {
            _config = config;
        }

    
        public async Task<string> SaveFile(AppImage appImage)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(_config["StorageAccount:BlobConnection"]);

            CloudBlobClient blobClient = account.CreateCloudBlobClient();


            CloudBlobContainer container;

            if (appImage.storage == Storage.CoverStorage)
            {
                container = blobClient.GetContainerReference("cover-images");
            }
            else {
                container = blobClient.GetContainerReference("profile-images");
            }
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(appImage.fileName);

            await blockBlob.UploadFromByteArrayAsync(appImage.image, 0, appImage.image.Length);

            return blockBlob.Uri.ToString();
        }
    }
}

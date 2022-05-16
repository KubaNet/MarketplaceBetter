using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Specialized
{
    public class PhotoCloudService : IPhotoCloudService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;

        public PhotoCloudService(IConfiguration configuration)
        {
            _configuration = configuration;

            Account account = new Account(
                configuration["CloudinaryCloudName"],
                configuration["CloudinaryKey"],
                configuration["CloudinarySecret"]);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public PhotoUploadResult Upload(MemoryStream photoStream, string fileName)
        {
            string folder = _configuration["CloudinaryFolder"];
            string fullFileName = $"{folder}/{fileName}";

            ImageUploadParams parameters = new ImageUploadParams
            {
                AllowedFormats = new string[] { "jpg", "png" },
                PublicId = fullFileName,
                File = new FileDescription(fullFileName, photoStream)
            };

            ImageUploadResult result = _cloudinary.Upload(parameters);

            PhotoUploadResult uploadResult = new PhotoUploadResult();

            if (result.Error != null)
            {
                uploadResult.WasSuccessful = false;
                uploadResult.Error = result.Error.Message;
            }
            else
            {
                uploadResult.WasSuccessful = true;
                uploadResult.Url = result.Url.ToString();
            }

            return uploadResult;
        }
    }
}

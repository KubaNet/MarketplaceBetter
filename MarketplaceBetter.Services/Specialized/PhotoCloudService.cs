using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Specialized
{
    public class PhotoCloudService : IPhotoCloudService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly string _instanceFolder;
        private readonly string[] _allowedFormats = new string[] { "jpg", "png" };

        public PhotoCloudService(IConfiguration configuration)
        {
            _configuration = configuration;

            Account account = new Account(
                configuration["CloudinaryCloudName"],
                configuration["CloudinaryKey"],
                configuration["CloudinarySecret"]);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;

            _instanceFolder = _configuration["CloudinaryFolder"];
        }

        public PhotoUploadResult Upload(MemoryStream photoStream, string fileName)
        {
            fileName = ClearFileName(fileName);
            string fullFileName = $"{_instanceFolder}/{fileName}";

            ImageUploadParams parameters = new ImageUploadParams
            {
                AllowedFormats = _allowedFormats,
                PublicId = fullFileName,
                File = new FileDescription(fullFileName, photoStream)
            };

            ImageUploadResult result = _cloudinary.Upload(parameters);
            PhotoUploadResult uploadResult = CreatePhotoUploadResult(result);

            return uploadResult;
        }

        public PhotoUploadResult PreUpload(MemoryStream photoStream, string fileName)
        {
            fileName = ClearFileName(fileName);
            string fullFileName = $"preupload/{_instanceFolder}/{fileName}";

            ImageUploadParams parameters = new ImageUploadParams
            {
                AllowedFormats = _allowedFormats,
                PublicId = fullFileName,
                File = new FileDescription(fullFileName, photoStream)
            };

            ImageUploadResult result = _cloudinary.Upload(parameters);
            PhotoUploadResult uploadResult = CreatePhotoUploadResult(result);

            return uploadResult;
        }

        public string GetPhotoUrlForLists(string cloudId, string version)
        {
            return _cloudinary.Api.UrlImgUp.Version(version).Transform(new Transformation().Width(100)).BuildUrl(cloudId);
        }

        public string GetBiggerPhotoUrlForLists(string cloudId, string version)
        {
            return _cloudinary.Api.UrlImgUp.Version(version).Transform(new Transformation().Height(700)).BuildUrl(cloudId);
        }

        public void Delete(string cloudId)
        {
            _cloudinary.DeleteResources(ResourceType.Image, new string[] { cloudId });
        }

        private string ClearFileName(string fileName)
        {
            foreach (var format in _allowedFormats)
            {
                fileName = fileName.Replace($".{format}", string.Empty, StringComparison.OrdinalIgnoreCase);
            }

            return fileName;
        }

        private PhotoUploadResult CreatePhotoUploadResult(ImageUploadResult result)
        {
            PhotoUploadResult uploadResult = new PhotoUploadResult();

            if (result.Error != null)
            {
                uploadResult.WasSuccessful = false;
                uploadResult.Error = result.Error.Message;
            }
            else
            {
                uploadResult.WasSuccessful = true;
                uploadResult.CloudId = result.PublicId;
                uploadResult.Version = result.Version;
                uploadResult.Url = result.SecureUrl.ToString();
                uploadResult.Height = result.Height;
                uploadResult.Width = result.Width;
            }

            return uploadResult;
        }
    }
}

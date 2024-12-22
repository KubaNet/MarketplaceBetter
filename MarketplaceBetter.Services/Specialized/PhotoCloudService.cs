using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public PhotoUploadResult SaveFromPreUpload(string cloudId, string version, string fileName)
        {
            string fullFileName = $"{_instanceFolder}/{fileName}";
            string originalFileUrl = _cloudinary.Api.UrlImgUp.Version(version).BuildUrl(cloudId);

            ImageUploadParams parameters = new ImageUploadParams
            {
                AllowedFormats = _allowedFormats,
                PublicId = fullFileName,
                File = new FileDescription(fullFileName, originalFileUrl)
            };

            ImageUploadResult result = _cloudinary.Upload(parameters);

            PhotoUploadResult uploadResult = CreatePhotoUploadResult(result);

            return uploadResult;
        }

        public PhotoRenamingResult Rename(string cloudId, string newFileName)
        {
            string fullFileName = $"{_instanceFolder}/{newFileName}";

            RenameResult result = _cloudinary.Rename(cloudId, fullFileName);

            PhotoRenamingResult renamingResult = CreateRenamingResult(result);

            return renamingResult;
        }

        public string GetOriginalUrl(string cloudId, string version)
        {
            return _cloudinary.Api.UrlImgUp.Version(version).BuildUrl(cloudId);
        }

        public string GetFormat(string cloudId)
        {
            return _cloudinary.GetResource(cloudId).Format;
        }

        public string GetUrlForLists(string cloudId, string version, PhotoOnListSizeEnum size)
        {
            int width;
            switch (size)
            {
                case PhotoOnListSizeEnum.S100:
                    width = 100;
                    break;
                case PhotoOnListSizeEnum.S150:
                    width = 150;
                    break;
                case PhotoOnListSizeEnum.S200:
                    width = 200;
                    break;
                default:
                    throw new UnrecognizedEnumValueException<PhotoOnListSizeEnum>(size);
            }

            return _cloudinary.Api.UrlImgUp.Version(version).Transform(new Transformation().Width(width)).BuildUrl(cloudId);
        }

        public string GetBiggerUrlForLists(string cloudId, string version)
        {
            return _cloudinary.Api.UrlImgUp.Version(version).Transform(new Transformation().Height(700).Width(1000).Crop("limit")).BuildUrl(cloudId);
        }

        public void Delete(string cloudId)
        {
            Delete(new List<string> { cloudId });
        }

        public void Delete(IList<string> cloudIds)
        {
            foreach (var cloudIdsChunk in cloudIds.Chunk(50))
            {
                _cloudinary.DeleteResources(ResourceType.Image, cloudIdsChunk);
            }
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

        private PhotoRenamingResult CreateRenamingResult(RenameResult result)
        {
            PhotoRenamingResult renamingResult = new PhotoRenamingResult();

            if (result.Error != null)
            {
                throw new Exception($"There was error with renaming photo {result.PublicId}: {result.Error.Message}");
            }
            else
            {
                renamingResult.CloudId = result.PublicId;
                renamingResult.Version = result.Version;
                renamingResult.Url = result.SecureUrl.ToString();
            }

            return renamingResult;
        }
    }
}

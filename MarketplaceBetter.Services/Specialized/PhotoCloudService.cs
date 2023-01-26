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
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Specialized
{
    public class PhotoCloudService : IPhotoCloudService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly string _instanceFolder;

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
            string fullFileName = $"{_instanceFolder}/{fileName}";

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
                uploadResult.Id = result.PublicId;
                uploadResult.Url = result.SecureUrl.ToString();
            }

            return uploadResult;
        }

        public IList<PhotoUploadModel> GetPhotosToUpload()
        {
            SearchResult searchResult = _cloudinary.Search().Expression($"folder:upload/prod").MaxResults(500).Execute();

            IList<PhotoUploadModel> photos = new List<PhotoUploadModel>();

            foreach (var resource in searchResult.Resources)
            {
                PhotoUploadModel photo = new PhotoUploadModel();

                photo.CloudId = resource.PublicId;
                photo.FileName = resource.FileName;
                photo.Url= resource.Url;

                photos.Add(photo);
            }

            return photos;
        }

        public string GetPhotoUrlForLists(string cloudId)
        {
            return _cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(100)).BuildUrl(cloudId);
        }

        public void Delete(string cloudId)
        {
            _cloudinary.DeleteResources(ResourceType.Image, new string[] { cloudId });
        }
    }
}

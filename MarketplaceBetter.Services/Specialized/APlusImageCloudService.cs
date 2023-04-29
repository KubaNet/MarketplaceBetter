using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
	internal class APlusImageCloudService : IAPlusImageCloudService
	{
		private readonly IConfiguration _configuration;
		private readonly Cloudinary _cloudinary;
		private readonly string _instanceFolder;
		private readonly string[] _allowedFormats = new string[] { "jpg", "png" };

		public APlusImageCloudService(IConfiguration configuration)
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

		public APlusImageUploadResult Upload(MemoryStream imageStream, string fileName)
		{
			fileName = ClearFileName(fileName);
			string fullFileName = $"APlusImages/{_instanceFolder}/{fileName}";

			ImageUploadParams parameters = new ImageUploadParams
			{
				AllowedFormats = _allowedFormats,
				PublicId = fullFileName,
				File = new FileDescription(fullFileName, imageStream)
			};

			ImageUploadResult result = _cloudinary.Upload(parameters);
			APlusImageUploadResult uploadResult = CreateImageUploadResult(result);

			return uploadResult;
		}

		private string ClearFileName(string fileName)
		{
			foreach (var format in _allowedFormats)
			{
				fileName = fileName.Replace($".{format}", string.Empty, StringComparison.OrdinalIgnoreCase);
			}

			return fileName;
		}

		private APlusImageUploadResult CreateImageUploadResult(ImageUploadResult result)
		{
			APlusImageUploadResult uploadResult = new APlusImageUploadResult();

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

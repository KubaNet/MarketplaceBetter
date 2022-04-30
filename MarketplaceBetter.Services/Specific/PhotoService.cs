using CloudinaryDotNet;
using MarketplaceBetter.Services.Specific.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specific
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IConfiguration configuration)
        {
            Account account = new Account(
                configuration["CloudinaryCloudName"],
                configuration["CloudinaryKey"],
                configuration["CloudinarySecret"]);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }
    }
}

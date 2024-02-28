using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebAPI.Data.Interfaces;

namespace WebAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;

        public PhotoService(IConfiguration config)

        {
            Account account = new Account(
    config.GetSection("CloudinarySettings:Cloudname").Value,
    config.GetSection("CloudinarySettings:ApiKey").Value,
   config.GetSection("CloudinarySettings:ApiSecret").Value);

             this.cloudinary = new Cloudinary(account);
            this.cloudinary.Api.Secure = true;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
           var deleteParams=new DeletionParams(publicId);
           var result=await cloudinary.DestroyAsync(deleteParams);
           return result;
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if (photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, stream)
                  
                };

                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
    }
}
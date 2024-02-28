using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace WebAPI.Data.Interfaces
{
    public interface IPhotoService
    {
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
    }
}
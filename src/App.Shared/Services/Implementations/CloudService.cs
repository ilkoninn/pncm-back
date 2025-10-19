using App.Business.Helpers;
using App.Shared.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services.Implementations
{
    public class CloudService : ICloudService
    {
        private readonly Cloudinary _cloudinary;

        public CloudService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task DeleteFileFromCloudAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                return;

            var uri = new Uri(fileUrl);
            var path = uri.AbsolutePath;
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            string folder = segments[segments.Length - 2];
            string fileWithExtension = segments.Last();
            string publicId = $"{folder}/{fileWithExtension}";

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Raw // Use Raw for non-image files like PDF, Word, etc.
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result != "ok")
            {
                throw new Exception($"File deletion failed: {result.Error?.Message}");
            }
        }

        public async Task<string> UploadToCloudAsync(IFormFile file)
        {
            if (!FileChecker.BeAValidImage(file))
                throw new Exception("Invalid file format. Only image, PDF, Word, or PowerPoint files are allowed (maximum size: 20MB).");

            await EnsureCloudStorageAvailableAsync();

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "uploads"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult?.SecureUrl?.ToString();
            }
        }

        private async Task EnsureCloudStorageAvailableAsync()
        {
            var result = await _cloudinary.GetUsageAsync();

            var usedPercent = result?.Storage?.UsedPercent ?? 0;

            if (usedPercent >= 95)
                throw new Exception($"Cloudinary storage usage is at {usedPercent:F1}%. Uploading is blocked.");
        }
    }
}

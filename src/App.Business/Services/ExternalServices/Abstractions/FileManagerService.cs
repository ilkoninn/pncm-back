using App.Business.Helpers;
using App.Business.Services.ExternalServices.Interfaces;
using App.Shared.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.ExternalServices.Abstractions
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ICloudService _cloudService;

        public FileManagerService(IWebHostEnvironment environment, ICloudService cloudService)
        {
            _environment = environment;
            _cloudService = cloudService;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string fileName = await UploadLocalAsync(file);
            string fileUrl = $"https://api.pncm.az/uploads/{fileName}";

            return fileUrl;
        }

        public async Task RemoveFileAsync(string fileUrl)
        {
            await DeleteLocalAsync(fileUrl);
        }

        private async Task DeleteLocalAsync(string fileUrl)
        {
            var uploadsPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads");

            string encodedFileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
            string fileName = Uri.UnescapeDataString(encodedFileName); // decode %20, etc.

            var filePath = Path.Combine(uploadsPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                throw new FileNotFoundException("File not found.", fileName);
            }

            await Task.CompletedTask;
        }

        private async Task<string> UploadLocalAsync(IFormFile file)
        {
            if (!FileChecker.BeAValidImage(file))
                throw new Exception("Invalid file format. Only image, PDF, Word, or PowerPoint files are allowed (maximum size: 20MB).");

            var fileName = Guid.NewGuid().ToString() + "_" +
                Path.GetFileNameWithoutExtension(file.FileName) +
                Path.GetExtension(file.FileName);

            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services.Interfaces
{
    public interface ICloudService
    {
        Task<string> UploadToCloudAsync(IFormFile file);
        Task DeleteFileFromCloudAsync(string fileUrl);
    }
}

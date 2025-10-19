using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Helpers
{
    public static class FileChecker
    {
        public static bool BeAValidImage(IFormFile file)
        {
            return file != null
                && IsAllowedContentType(file.ContentType)
                && file.Length <= 20 * 1024 * 1024; // 20 MB
        }

        private static bool IsAllowedContentType(string contentType)
        {
            string[] allowedTypes = new[]
            {
                // Document formats
                "application/pdf",                                                      // .pdf
                "application/msword",                                                  // .doc
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // .docx
                "application/vnd.ms-powerpoint",                                       // .ppt
                "application/vnd.openxmlformats-officedocument.presentationml.presentation", // .pptx

                // Image formats
                "image/jpeg",     // .jpg, .jpeg
                "image/png",      // .png
                "image/gif",      // .gif
                "image/webp",     // .webp
                "image/bmp",      // .bmp
                "image/tiff",     // .tif, .tiff
                "image/svg+xml",  // .svg
                "image/x-icon",   // .ico
                "image/vnd.microsoft.icon", // .ico (alternative)
                "image/heif",     // .heif
                "image/heic"      // .heic
            };

            return allowedTypes.Contains(contentType);
        }

    }
}

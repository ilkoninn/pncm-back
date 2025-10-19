using App.Core.DTOs.Commons;
using App.Business.Services.ExternalServices.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles.Commons
{
    public class CustomMappingAction<TSource, TDestination> :
     IMappingAction<TSource, TDestination>
     where TSource : class
    {
        private readonly IFileManagerService _fileManagerService;

        public CustomMappingAction(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }

        public void Process(TSource source, TDestination destination, ResolutionContext context)
        {
            if (source is IAuditedFileEntityDTO fileEntity && fileEntity.File != null)
            {
                var uploadedUrl = _fileManagerService.UploadFileAsync(fileEntity.File).Result;
                SetPropertyValue(destination, "FileUrl", uploadedUrl);
            }
            else if (source is IAuditedImageEntityDTO imageEntity && imageEntity.Image != null)
            {
                var uploadedUrl = _fileManagerService.UploadFileAsync(imageEntity.Image).Result;
                SetPropertyValue(destination, "ImageUrl", uploadedUrl);
            }
            else if (source is IAuditedIconEntityDTO iconEntity && iconEntity.Icon != null)
            {
                var uploadedUrl = _fileManagerService.UploadFileAsync(iconEntity.Icon).Result;
                SetPropertyValue(destination, "IconUrl", uploadedUrl);
            }
        }

        private void SetPropertyValue(object destination, string propertyName, string value)
        {
            var property = destination.GetType().GetProperty(propertyName);
            if (property != null && property.PropertyType == typeof(string))
            {
                property.SetValue(destination, value);
            }
        }
    }

}

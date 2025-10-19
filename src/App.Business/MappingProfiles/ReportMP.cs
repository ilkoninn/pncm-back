using App.Core.DTOs.ReportDTOs;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class ReportMP : Profile
    {
        public ReportMP()
        {
            CreateMap<Report, ReportDTO>().ReverseMap();
            CreateMap<CreateReportDTO, Report>();
        }
    }
}

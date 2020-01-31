using AutoMapper;
using AutoMy.ServiceModels;
using AutoMyWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMyWebsite.Mapping
{
    public class ViewMapperProfiler : Profile
    {
        public ViewMapperProfiler()
        {
            CreateMap<AccountDTO, AccountViewModel>();
            CreateMap<AccountViewModel, AccountDTO>();
            CreateMap<PostDTO, PostViewModel>();
            CreateMap<PostViewModel, PostDTO>();
            CreateMap<CategoryViewModel, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<ReportViewModel, ReportDTO>();
            CreateMap<ReportDTO, ReportViewModel>();
        }
    }
}

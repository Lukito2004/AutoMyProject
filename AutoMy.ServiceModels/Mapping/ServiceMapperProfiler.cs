using AutoMapper;
using AutoMy.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.ServiceModels.Mapping
{
    public class ServiceMapperProfiler : Profile
    {
        public ServiceMapperProfiler()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Report, ReportDTO>();
            CreateMap<ReportDTO, Report>();
        }
    }
}

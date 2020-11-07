using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Entities;
using Routine.Api.Models;
namespace Routine.Api.Profiles
{
    public class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.Name,     //指定映射: dest中的Name与source中的Name映射
                opt => opt.MapFrom(src => src.Name));

            CreateMap<CompanyAddDTO, Company>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Entities;
using Routine.Api.Models;
using AutoMapper;
namespace Routine.Api.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()  //创建映射
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.GenderDisplay,
                    opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));

            CreateMap<EmployeeAddDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>();
        }
    }
}

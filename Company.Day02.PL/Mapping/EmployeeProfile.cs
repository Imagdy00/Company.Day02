using AutoMapper;
using Company.Day02.DAL.Models;
using Company.Day02.PL.Dtos;

namespace Company.Day02.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
                /*.ForMember(d => d.Name , o => o.MapFrom(s => $"{s.Name}"));*/
            CreateMap<Employee , CreateEmployeeDto>();

        }
    }
}

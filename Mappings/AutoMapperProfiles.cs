using AutoMapper;
using SampleWeb.API.Models;
using SampleWeb.API.Models.DTO;

namespace SampleWeb.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
        }
    }
}

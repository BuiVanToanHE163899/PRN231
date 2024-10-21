using AutoMapper;
using P1Solution.Models;

namespace P1Solution.Mapper
{
    public class AutoMapper : Profile
    {
       public AutoMapper() {
            CreateMap<Order, OrderDTO>().ForMember(o => o.customerName, opt => opt.MapFrom(src => src.Customer!.CompanyName))
                .ForMember(o => o.employeeName, opt => opt.MapFrom(src => src.Employee!.FirstName + " " + src.Employee!.LastName))
                .ForMember(o => o.employeeDepartmentId, opt => opt.MapFrom(src => src.Employee.Department!.DepartmentId))
                .ForMember(o => o.employeeDepartmentName, opt => opt.MapFrom(src => src.Employee.Department!.DepartmentName));
        }
        
    }
}

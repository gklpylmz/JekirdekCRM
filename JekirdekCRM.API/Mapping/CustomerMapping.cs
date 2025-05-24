using AutoMapper;
using JekirdekCRM.DTO.CustomerDtos;
using JekirdekCRM.ENTITIES.Models;

namespace JekirdekCRM.API.Mapping
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping() 
        {
            CreateMap<Customer,GetCustomerDto>().ReverseMap();
            CreateMap<Customer,CreateCustomerDto>().ReverseMap();
            CreateMap<Customer,UpdateCustomerDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Iktan.Ecommerce.Domain.Entity;
using Iktan.Ecommerce.App.DTO;

namespace Iktan.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customers, CustomerDTO>().ReverseMap();

            /* CreateMap<Customers, CustomerDTO>().ReverseMap()
                .ForMember(destination => destination.CustomerId, source => source.MapFrom(src => src.CustomerId))
                .ForMember(destination => destination.CompanyName, source => source.MapFrom(src => src.CompanyName)); */
        }


    }
}

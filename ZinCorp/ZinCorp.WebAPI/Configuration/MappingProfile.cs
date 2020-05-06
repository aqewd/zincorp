using AutoMapper;
using ZinCorp.BE;
using ZinCorp.DAL.Models;

namespace ZinCorp.WebAPI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbProduct, Product>().ReverseMap();
            CreateMap<DbProductImage, ProductImage>().ReverseMap();
            CreateMap<DbCustomer, Customer>().ReverseMap();
        }
    }
}

using AutoMapper;
using Core.Entities;
using OnlineShop.Dtos;

namespace OnlineShop.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturn>()
                .ForMember(d => d.ProductBrand,o=>o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(p => p.ProductType,o=>o.MapFrom(s=> s.ProductType.Name))
                .ForMember(d => d.PictureUrl,o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
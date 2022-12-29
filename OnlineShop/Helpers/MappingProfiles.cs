using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Dtos;
using Address = Core.Entities.Identity.Address;

namespace OnlineShop.Helpers
{
    public class MappingProfiles : Profile
    {
        private UserManager<AppUser> _userManager;
        public MappingProfiles()
        {
            
            CreateMap<Product, ProductToReturn>()
                .ForMember(d => d.ProductBrand,o=>o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(p => p.ProductType,o=>o.MapFrom(s=> s.ProductType.Name))
                .ForMember(d => d.PictureUrl,o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
            CreateMap<BrandDto,ProductBrand>();
            CreateMap<TypeDto,ProductType>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<ProductToCreateOrUpdate,Product>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl,o => o.MapFrom<OrderItemUrlResolver>());

        }
    }
}
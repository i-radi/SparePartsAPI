using AutoMapper;
using SpareParts.Domain.Dtos.IdentityDtos;
using SpareParts.Domain.Dtos.OrderDtos;
using SpareParts.Domain.Dtos.OrderItemDtos;
using SpareParts.Domain.Dtos.ProductDtos;
using SpareParts.Domain.Dtos.UserDtos;

namespace SpareParts.Domain.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //Source -> Target
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserReadDto>();

            CreateMap<Product, ProductReadDto>();
            CreateMap<OrderItem, OrderItemReadDto>();
            CreateMap<OrderItem, OrderReadDto>();

            CreateMap<ProductUpdateDto,Product>();
            CreateMap<OrderItemUpdateDto, OrderItem>();
            CreateMap<OrderItemUpdateDto, OrderItem>();

            CreateMap<ProductCreateDto, Product>();
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItemCreateDto, OrderItem>();
        }
    }
}

using AutoMapper;
using SpareParts.Data;

namespace SpareParts.Domain
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //Source -> Target
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Product, AddProductDto>().ReverseMap();
            CreateMap<Product, ReadProductDto>().ReverseMap();
            CreateMap<Product, SimpleProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<Brand, AddBrandDto>().ReverseMap();
            CreateMap<Brand, ReadBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();

            CreateMap<Category, AddCategoryDto>().ReverseMap();
            CreateMap<Category, ReadCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<ShoppingCart, UpdateShoppingCartDto>().ReverseMap();
            CreateMap<ShoppingCart, ChangeCountShoppingCartDto>().ReverseMap();
            CreateMap<ShoppingCart, AssignProductToCartDto>().ReverseMap();
            CreateMap<ShoppingCart, ReadShoppingCartDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImgPath, opt => opt.MapFrom(src => src.Product.ImgPath))
                .ForMember(dest => dest.ModelNumber, opt => opt.MapFrom(src => src.Product.ModelNumber))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ReverseMap();
        }
    }
}

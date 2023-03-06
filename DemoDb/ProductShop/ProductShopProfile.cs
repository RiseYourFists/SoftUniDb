using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<ImportCategoryDto, Category>();
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();

            this.CreateMap<Product, ExportProductInPriceRangeDto>()
                .ForMember(d => d.Seller, mo => mo.MapFrom(s =>
                    $"{s.Seller.FirstName} {s.Seller.LastName}"));
        }
    }
}

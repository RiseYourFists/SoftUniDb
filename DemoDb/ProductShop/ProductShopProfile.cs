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

            this.CreateMap<Product, ExportPartialSoldProductDto>()

                .ForMember(d => d.BuyerFirstName, mo =>
                    mo.MapFrom(s => s.Buyer.FirstName))

                .ForMember(d => d.BuyerLastName, mo =>
                    mo.MapFrom(s => s.Buyer.LastName));

            this.CreateMap<User, ExportSellerDto>()
                .ForMember(d => d.SoldProducts, mo =>
                    mo.MapFrom(s => s.ProductsSold.Where(p => p.BuyerId.HasValue)));

            this.CreateMap<Category, ExportCategoriesDto>()
                .ForMember(d => d.Category, mo => 
                    mo.MapFrom(s => s.Name))

                .ForMember(d => d.AveragePrice, mo =>
                    mo.MapFrom(s => s.CategoriesProducts.Select(p => p.Product.Price)
                        .Average()))

                .ForMember(d => d.ProductsCount, mo =>
                    mo.MapFrom(s => s.CategoriesProducts.Count))

                .ForMember(d => d.TotalRevenue, mo =>
                    mo.MapFrom(s => s.CategoriesProducts.Select(p => p.Product.Price)
                        .Sum()));
        }
    }
}

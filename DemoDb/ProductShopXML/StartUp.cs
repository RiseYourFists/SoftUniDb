﻿using ProductShop.Data;
using ProductShop.Models;
using System.ComponentModel.DataAnnotations;

using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

using ProductShop.DTOs.Export.Products;
using ProductShop.DTOs.Export.Categories;
using ProductShop.DTOs.Export.Users;

using ProductShop.DTOs.Import.Category;
using ProductShop.DTOs.Import.CategoryProducts;
using ProductShop.DTOs.Import.Product;
using ProductShop.DTOs.Import.User;

namespace ProductShop
{
    public class StartUp
    {
        private static string directory;
        public static void Main()
        {
            var context = new ProductShopContext();

            directory = InitializeExportDirectory("users-and-products.xml");

            //var xmlData = File.ReadAllText(directory);

            var output = GetUsersWithProducts(context);

            File.WriteAllText(directory, output);
        }

        /*          Import              */
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var xmlRoot = new XmlRootAttribute("Users");
            var xmlSerializer = new XmlSerializer(typeof(ImportUserDto[]), xmlRoot);

            using StringReader xmlReader = new StringReader(inputXml);
            var userDtos = (ImportUserDto[])xmlSerializer.Deserialize(xmlReader);

            var users = userDtos
                .Select(u => new User()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlRoot = new XmlRootAttribute("Products");
            var xmlSerializer = new XmlSerializer(typeof(ImportProductDto[]), xmlRoot);

            using var xmlReader = new StringReader(inputXml);
            var xmlProductsData = (ImportProductDto[])xmlSerializer.Deserialize(xmlReader);

            var products = new List<Product>();

            foreach (var importProductDto in xmlProductsData)
            {
                if (!IsValid(importProductDto))
                {
                    continue;
                }

                var product = new Product()
                {
                    Name = importProductDto.Name,
                    Price = importProductDto.Price,
                    BuyerId = importProductDto.BuyerId,
                    SellerId = importProductDto.SellerId,
                };

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlRoot = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(ImportCategoryDto[]), xmlRoot);

            using var xmlReader = new StringReader(inputXml);
            var xmlCategoryData = (ImportCategoryDto[])serializer.Deserialize(xmlReader);

            var categories = new List<Category>();

            foreach (var importCategoryDto in xmlCategoryData)
            {
                if (!IsValid(importCategoryDto))
                {
                    continue;
                }

                var category = new Category()
                {
                    Name = importCategoryDto.Name,
                };

                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlRoot = new XmlRootAttribute("CategoryProducts");
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), xmlRoot);

            var xmlReader = new StringReader(inputXml);
            var xmlCategoryProductsData = (ImportCategoryProductDto[])xmlSerializer.Deserialize(xmlReader);

            var productIds = context.Products
                .Select(p => p.Id)
                .ToArray();

            var categoryIds = context.Categories
                .Select(c => c.Id)
                .ToArray();

            var categoryProducts = new List<CategoryProduct>();

            foreach (var cpDto in xmlCategoryProductsData)
            {
                if (!IsValid(cpDto))
                {
                    continue;
                }
                if (!categoryIds.Contains(cpDto.CategoryId) || !productIds.Contains(cpDto.ProductId))
                {
                    continue;
                }

                var categoryProduct = new CategoryProduct()
                {
                    CategoryId = cpDto.CategoryId,
                    ProductId = cpDto.ProductId,
                };
                categoryProducts.Add(categoryProduct);
            }

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }


        /*          Export              */

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Include(p => p.Buyer)
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .Select(p => new ExportProductInPriceRangeDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerFullName = string.Join(" ", p.Buyer.FirstName, p.Buyer.LastName)//$"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .ToArray();

            var output = Serialize("Products", products);

            return output;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var products = context.Users
                .Where(u => u.ProductsSold.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(p => new ExportProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                })
                .ToArray();

            var output = Serialize("Users", products);

            return output;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Include(c => c.CategoryProducts)
                .ThenInclude(cp => cp.Product)
                .Select(c => new ExportCategoryDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            var output = Serialize("Categories", categories);

            return output;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(u => u.ProductsSold)
                .ThenInclude(ps => ps.CategoryProducts)
                .Where(u => u.ProductsSold.Any())
                .Select(u => new ExportUserWithProductsDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportSoldProductsDto()
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold
                            .Select(p => new ExportProductDto()
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .OrderByDescending(p => p.Price)
                            .ToArray()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .Take(10)
                .ToArray();

            var exportUserGeneralData = new ExportUsersDto()
            {
                Count = context.Users.Count(u => u.ProductsSold.Any()),
                Users = users
            };

            var output = Serialize("Users", exportUserGeneralData);

            return output;
        }


        /*          Helper Methods      */

        public static string InitializeImportDirectory(string fileName)
            => Path.Combine("../../../Datasets/", fileName);

        public static string InitializeExportDirectory(string fileName)
            => Path.Combine("../../../Results/", fileName);

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }

        public static string Serialize<T>(string rootName, T dto)
        {
            var xmlRootName = new XmlRootAttribute(rootName);

            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            var xmlSerializer = new XmlSerializer(typeof(T), xmlRootName);

            var settings = new XmlWriterSettings();
            settings.Indent = true;

            var sb = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(sb, settings))
            {
                xmlSerializer.Serialize(xmlWriter, dto, xmlNamespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs.Export.Categories;
using ProductShop.DTOs.Export.Products;
using ProductShop.DTOs.Export.Users;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        private static string directory;
        private static IMapper mapper;
        public static void Main()
        {
            var context = new ProductShopContext();

            directory = InitializeOutputDirectory("users-and-products.json");
            
            var json = GetUsersWithProducts(context);
            File.WriteAllText(directory, json);
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            InitializeMapper();

            var jsonUsersData = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);

            var users = new List<User>();
            foreach (var uDto in jsonUsersData)
            {
                var user = mapper.Map<User>(uDto);
                users.Add(user);
            }

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            InitializeMapper();

            var productsJsonData = JsonConvert
                .DeserializeObject<ImportProductDto[]>(inputJson);

            var products = new List<Product>();
            foreach (var pDto in productsJsonData)
            {
                if (!IsValid(pDto))
                {
                    continue;
                }
                var product = mapper.Map<Product>(pDto);
                products.Add(product);
            }

            context.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            InitializeMapper();

            var categoriesJsonData = JsonConvert.DeserializeObject<ImportCategoryDto[]>(inputJson);

            var categories = new List<Category>();

            foreach (var categoryDto in categoriesJsonData)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                var category = mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }

            context.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            InitializeMapper();

            var categoryProductsJsonData = JsonConvert.DeserializeObject<ImportCategoryProductDto[]>(inputJson);

            var categoryProducts = new List<CategoryProduct>();
            foreach (var categoryProductDto in categoryProductsJsonData)
            {
                if (!IsValid(categoryProductDto))
                {
                    continue;
                }

                var categoryProduct = mapper.Map<CategoryProduct>(categoryProductDto);
                categoryProducts.Add(categoryProduct);
            }

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";

        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            InitializeMapper();

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .ProjectTo<ExportProductInPriceRangeDto>(mapper.ConfigurationProvider)
                .OrderBy(p => p.Price)
                .ToList();

            var result = JsonConvert.SerializeObject(products, Formatting.Indented);
            
            return result;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            InitializeMapper();

            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId.HasValue))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<ExportSellerDto>(mapper.ConfigurationProvider)
                .ToArray();

            var output = JsonConvert.SerializeObject(users, Formatting.Indented);
            return output;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            InitializeMapper();

            var categories = context.Categories
                .ProjectTo<ExportCategoriesDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.ProductsCount)
                .ToList();

            categories.ForEach(c => 
            { 
               c.AveragePrice = Math.Round(c.AveragePrice, 2);
               c.TotalRevenue = Math.Round(c.TotalRevenue, 2);
            });

            var output = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return output;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            InitializeMapper();

            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId.HasValue))
                .OrderByDescending(u => u.ProductsSold.Count(p => p.BuyerId.HasValue))
                .ProjectTo<ExportUserShortDto>(mapper.ConfigurationProvider)
                .ToArray();

                var jsonSettings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                var allUsers = new ExportUserAllDto()
                {
                    UsersCount = users.Length,
                    Users = users
                };

            var output = JsonConvert.SerializeObject(allUsers, Formatting.Indented, jsonSettings);

            return output;
        }

        public static void InitializeMapper()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(ProductShopProfile))));
        }

        public static string InitializeInputDirectory(string fileName)
        => Path.Combine("../../../Datasets", fileName);

        public static string InitializeOutputDirectory(string fileName)
            => Path.Combine("../../../Results", fileName);

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }

    }
}
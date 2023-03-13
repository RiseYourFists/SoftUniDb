using ProductShop.Data;
using ProductShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
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
            
            directory = InitializeImportDirectory("categories-products.xml");

            var xmlData = File.ReadAllText(directory);

            var output = ImportCategoryProducts(context, xmlData);

            Console.WriteLine(output);
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
            throw new NotImplementedException();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            throw new NotImplementedException();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            throw new NotImplementedException();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            throw new NotImplementedException();
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
    }
}
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
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
            //directory = InitializeDirectory("users.json");
            directory = InitializeDirectory("products.json");
           
            var json = File.ReadAllText(directory);

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            var result = ImportProducts(context, json);
            Console.WriteLine(result);
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            mapper = new Mapper(new MapperConfiguration(cfg
                => cfg.AddProfile(typeof(ProductShopProfile))));

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
            mapper = new Mapper(new MapperConfiguration(cfg 
                => cfg.AddProfile(typeof(ProductShopProfile))));

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

        public static string InitializeDirectory(string fileName)
        => Path.Combine("../../../Datasets", fileName);

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }

    }
}
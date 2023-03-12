using ProductShop.Data;
using ProductShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using ProductShop.DTOs.Import.User;

namespace ProductShop
{
    public class StartUp
    {
        private static string directory;
        public static void Main()
        {
            var context = new ProductShopContext();
            
            directory = InitializeImportDirectory("users.xml");

            var xmlData = File.ReadAllText(directory);

            var output = ImportUsers(context, xmlData);

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
            throw new NotImplementedException();

            //return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            throw new NotImplementedException();

            //return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            throw new NotImplementedException();

            //return $"Successfully imported {categoryProducts.Count}";
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
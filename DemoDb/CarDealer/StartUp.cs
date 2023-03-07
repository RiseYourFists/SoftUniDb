using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import.SupplierDtos;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static IMapper mapper;
        public static string directory;

        public static void Main()
        {
            directory = InitializeImportDirectory("suppliers.json");
            var json = File.ReadAllText(directory);

            var context = new CarDealerContext();

            var output = ImportParts(context,json);

            Console.WriteLine(output);
        }

        /*                          Import Data                             */
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            InitializeMapper();

            var supplierJsonData = JsonConvert.DeserializeObject<ImportSupplierDto[]>(inputJson);

            var suppliers = new List<Supplier>();
            foreach (var supplierDto in supplierJsonData)
            {
                if (!IsValid(supplierDto))
                {
                    continue;
                }

                var supplier = mapper.Map<Supplier>(supplierDto);
                suppliers.Add(supplier);
            }

            context.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            /*
             * Import the parts from the provided file parts.json. If the supplierId doesn't exist, skip the record.
             */

            throw new NotImplementedException();

            // return $"Successfully imported {Parts.Count}.";

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            throw new NotImplementedException();

            //return $"Successfully imported {Cars.Count}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            throw new NotImplementedException();

            //return $"Successfully imported {Customers.Count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            throw new NotImplementedException();

            //return $"Successfully imported {Sales.Count}.";
        }

        /*                           Export Data                            */

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            /*Get all customers ordered by their birth date ascending. If two customers are born on the same date first print those who are not young drivers (e.g., print experienced drivers first). */

            throw new NotImplementedException();

            //return json;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            /*Get all cars from making Toyota and order them by model alphabetically and by traveled distance descending.*/

            throw new NotImplementedException();

            //return json;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            /*Get all suppliers that do not import parts from abroad. Get their id, name and the number of parts they can offer to supply. */

            throw new NotImplementedException();

            //return  json;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            /*Get all cars along with their list of parts. For the car get only make, model and traveled distance and for the parts get only name and price (formatted to 2nd digit after the decimal point). */

            throw new NotImplementedException();

            //return json;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            /*Get all customers that have bought at least 1 car and get their names, bought cars count and total spent money on cars. Order the result list by total spent money descending then by total bought cars again in descending order.*/

            throw new NotImplementedException();

            //return json;
        }

        /*                             Helper Methods                       */

        public static void InitializeMapper()
            => mapper = new Mapper(new MapperConfiguration(cfg 
                => cfg.AddProfile(typeof(CarDealerProfile))));

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
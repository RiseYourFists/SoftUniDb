using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import.CarDtos;
using CarDealer.DTOs.Import.PartDtos;
using CarDealer.DTOs.Import.SupplierDtos;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static IMapper mapper;
        public static string directory;

        public static void Main()
        {
            directory = InitializeImportDirectory("cars.json");
            var json = File.ReadAllText(directory);

            var context = new CarDealerContext();

            var output = ImportCars(context,json);

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
            InitializeMapper();

            var partsJsonData = JsonConvert.DeserializeObject<ImportPartDto[]>(inputJson);

            var parts = new List<Part>();

            var suppliers = context.Suppliers.Count();

            foreach (var partDto in partsJsonData)
            {
                if (!IsValid(partDto))
                {
                    continue;
                }

                if (!partDto.SupplierId.HasValue)
                {
                    continue;
                }

                if (partDto.SupplierId.Value > suppliers || partDto.SupplierId.Value <= 0)
                {
                    continue;
                }
                var part = mapper.Map<Part>(partDto);
                parts.Add(part);
            }

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            InitializeMapper();

            var carJsonData = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

            var cars = new List<Car>();

            foreach (var importCarDto in carJsonData)
            {
                if (!IsValid(importCarDto))
                {
                    continue;
                }

                var car = mapper.Map<Car>(importCarDto);

                var parts = context.Parts.Where(p => importCarDto.PartsId.Contains(p.Id)).ToList();

                foreach (var part in parts)
                {
                    var carPart = new PartCar()
                    {
                        Car = car,
                        Part = part
                    };

                    car.PartsCars.Add(carPart);
                }

                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
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
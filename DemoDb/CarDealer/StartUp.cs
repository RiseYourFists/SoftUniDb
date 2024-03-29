﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CarDealer.Data;
using CarDealer.Models;

using CarDealer.DTOs.Export.CustomerDtos;

using CarDealer.DTOs.Import.CarDtos;
using CarDealer.DTOs.Import.CustomerDtos;
using CarDealer.DTOs.Import.PartDtos;
using CarDealer.DTOs.Import.SaleDtos;
using CarDealer.DTOs.Import.SupplierDtos;

using Newtonsoft.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.DTOs.Export.CarDtos;
using CarDealer.DTOs.Export.PartCarDtos;
using CarDealer.DTOs.Export.PartDtos;
using CarDealer.DTOs.Export.SaleDtos;
using CarDealer.DTOs.Export.SupplierDtos;
using Microsoft.EntityFrameworkCore;

namespace CarDealer
{
    public class StartUp
    {
        public static IMapper mapper;
        public static string directory;

        public static void Main()
        {

            directory = InitializeExportDirectory("sales-discounts.json");
            //var json = File.ReadAllText(directory);

            var context = new CarDealerContext();

            var output = GetSalesWithAppliedDiscount(context);

            //Console.WriteLine(output);
            File.WriteAllText(directory, output);
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
            InitializeMapper();

            var customersJsonData = JsonConvert.DeserializeObject<ImportCustomerDto[]>(inputJson);

            var customers = new List<Customer>();

            foreach (var importCustomerDto in customersJsonData)
            {
                if (!IsValid(importCustomerDto))
                {
                    continue;
                }

                var customer = mapper.Map<Customer>(importCustomerDto);
                customers.Add(customer);
            }

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            InitializeMapper();

            var salesJsonData = JsonConvert.DeserializeObject<ImportSaleDto[]>(inputJson);

            var sales = new List<Sale>();

            foreach (var importSaleDto in salesJsonData)
            {
                if (!IsValid(importSaleDto))
                {
                    continue;
                }

                var sale = mapper.Map<Sale>(importSaleDto);
                sales.Add(sale);
            }

            sales.ForEach(s => context.Sales.Add(s));
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        /*                           Export Data                            */

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            InitializeMapper();
            
            var customers = context.Customers
                .ProjectTo<ExportCustomerDto>(mapper.ConfigurationProvider)
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .ToArray();

            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new DateTimeConverter() }
            };
            var json = JsonConvert.SerializeObject(customers, jsonSettings);

            return json;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            InitializeMapper();

            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ProjectTo<ExportCarDto>(mapper.ConfigurationProvider)
                .ToArray();

            var json = JsonConvert.SerializeObject(cars, Formatting.None);

            return json;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            InitializeMapper();
            
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .ProjectTo<ExportSupplierDto>(mapper.ConfigurationProvider);

            var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return  json;
        }

        [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            InitializeMapper();

            var carInfo = context.Cars
                .Include(c => c.PartsCars)
                .ThenInclude(pc => pc.Part)
                .Select(c => new ExportCarInfoDto()
                {
                    CarId = c.Id,
                    Parts = c.PartsCars
                        .Where(pc => pc.CarId == c.Id)
                        .Select( p => new ExportPartShortDto()
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .ToArray()
                })
                .ToArray();

            var cars = context.Cars
                .ProjectTo<ExportCarJsonShortDto>(mapper.ConfigurationProvider)
                .ToArray();

            

            foreach (var exportCarInfoDto in carInfo)
            {
                exportCarInfoDto.CarJson = cars.First(c => c.Id == exportCarInfoDto.CarId);
            }

            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(carInfo, jsonSettings);

            return json;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            InitializeMapper();
            
            var customerInfo = context.Customers
                .Where(c => c.Sales.Any())
                .ProjectTo<ExportCustomerSalesDto>(mapper.ConfigurationProvider)
                .ToArray();

            var parts = context.PartsCars
                .ProjectTo<ExportPartCarDto>(mapper.ConfigurationProvider)
                .ToArray();

            foreach (var exportCustomerSalesDto in customerInfo)
            {
                exportCustomerSalesDto.SpentMoney = parts
                    .Where(p => exportCustomerSalesDto.CarIds
                        .Contains(p.CarId)).Select(p => p.Price).Sum();
            }

            var result = customerInfo
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToArray();

            var json = JsonConvert
                .SerializeObject(result, Formatting.Indented);

            return json;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            InitializeMapper();

            var sales = context.Sales
                .Take(10)
                .Select(s => new ExportSalesDto()
                {
                    Car = new ExportCarShortDto()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car.PartsCars
                        .Where(pc => pc.CarId == s.CarId)
                        .Select(p => p.Part.Price)
                        .Sum()
                        
                    
                })
                .ToArray();
            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>() { new DecimalConverter() }
            };
            var json = JsonConvert.SerializeObject(sales, jsonSettings);

            return json;
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
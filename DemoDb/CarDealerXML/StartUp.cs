﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.DTOs.Export.Cars;
using CarDealer.DTOs.Export.Customers;
using CarDealer.DTOs.Export.Parts;
using CarDealer.DTOs.Export.Sales;
using CarDealer.DTOs.Export.Suppliers;
using CarDealer.DTOs.Import.Cars;
using CarDealer.DTOs.Import.Customers;
using CarDealer.DTOs.Import.Parts;
using CarDealer.DTOs.Import.Sales;
using CarDealer.DTOs.Import.Suppliers;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer
{
    public class StartUp
    {
        private static string directory;
        public static void Main()
        {
            var context = new CarDealerContext();
            
            directory = InitializeExportDirectory("sales-discounts.xml");

            var output = GetSalesWithAppliedDiscount(context);

            File.WriteAllText(directory, output);
        }

        /*                          Import Methods                  */
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var suppliersXmlData = Deserialize<ImportSupplierDto[]>(inputXml, "Suppliers");
            
            var suppliers = new List<Supplier>();

            foreach (var supplierDto in suppliersXmlData)
            {
                if(!IsValid(supplierDto))
                    continue;

                var supplier = new Supplier()
                {
                    Name = supplierDto.Name,
                    IsImporter = supplierDto.IsImporter,
                };

                suppliers.Add(supplier);
            }

            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var partsXmlData = Deserialize<ImportPartDto[]>(inputXml, "Parts");

            var parts = new List<Part>();

            foreach (var partDto in partsXmlData)
            {
                if (!IsValid(partDto))
                {
                    continue;
                }

                if (!context.Suppliers.Any(s => s.Id == partDto.SupplierId))
                {
                    continue;
                }

                var part = new Part()
                {
                    Name = partDto.Name,
                    Price = partDto.Price,
                    Quantity = partDto.Quantity,
                    SupplierId = partDto.SupplierId,
                };

                parts.Add(part);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsXmlData = Deserialize<ImportCarDto[]>(inputXml, "Cars");
            
            var cars = new List<Car>();
            foreach (var carDto in carsXmlData)
            {
                if(!IsValid(carDto))
                    continue;
                
                var car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TraveledDistance = carDto.TraveledDistance
                    
                };

                var parts = context.Parts
                    .Where(p => carDto.Parts
                        .Select(cd => cd.Id)
                        .Contains(p.Id))
                    .ToArray();

                foreach (var part in parts)
                {
                    car.PartsCars.Add(new PartCar()
                    {
                        Car = car,
                        Part = part
                    });
                }

                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersXmlData = Deserialize<ImportCustomerDto[]>(inputXml, "Customers");

            var customers = new List<Customer>();
            foreach (var customerDto in customersXmlData)
            {
                if (!IsValid(customerDto))
                {
                    continue;
                }

                var customer = new Customer()
                {
                    Name = customerDto.Name,
                    BirthDate = customerDto.BirthDate,
                    IsYoungDriver = customerDto.IsYoungDriver,
                };
                customers.Add(customer);
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var salesXmlData = Deserialize<ImportSaleDto[]>(inputXml, "Sales");

            ICollection<Sale> sales = new List<Sale>();

            foreach (var salesDto in salesXmlData)
            {
                if (!IsValid(salesDto))
                {
                    continue;
                }

                bool hasItems = context.Cars.Any(c => c.Id == salesDto.CarId);

                if (!hasItems)
                {
                    continue;
                }

                var sale = new Sale()
                {
                    CarId = salesDto.CarId,
                    CustomerId = salesDto.CustomerId,
                    Discount = salesDto.Discount,
                };
                
                sales.Add(sale);
            }
            
            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        /*                          Export Methods                  */

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TraveledDistance > 2_000_000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .Select(c => new ExportCarWithDistanceDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                })
                .ToArray();

            var rootName = "cars";
            var output = Serialize(rootName, cars);

            return output;
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new ExportCarsMakeBmwDto()
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .ToArray();

            var rootName = "cars";
            var output = Serialize(rootName, cars);

            return output;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new ExportSupplierDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToArray();

            var rootName = "suppliers";
            var output = Serialize(rootName, suppliers);

            return output;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .OrderByDescending(c => c.TraveledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .Select(c => new ExportCarWithPartsDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                    Parts = c.PartsCars
                        .Select(pc => new ExportPartDto()
                        {
                            Name = pc.Part.Name,
                            Price = pc.Part.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                })
                .ToArray();

            var rootName = "cars";
            var output = Serialize(rootName, cars);

            return output;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Include(c => c.Sales)
                .ThenInclude(s => s.Car)
                .ThenInclude(c => c.PartsCars)
                .ThenInclude(p => p.Part)
                .Where(c => c.Sales.Any())
                .ToArray()
                .Select(c => new ExportCustomerDto()
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney =Format( c.Sales
                        .Select(s => s.Car.PartsCars
                            .Select(pc => pc.Part.Price).Sum())
                        .Sum() * (1 - (c.IsYoungDriver ? 0.05m : 0m)), 100)

                })
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            var rootName = "customers";
            var output = Serialize(rootName, customers);

            return output;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new ExportSaleDto()
                {
                    Car = new ExportCarSaleDto()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance,
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartsCars.Select(pc => pc.Part.Price).Sum(),
                    PriceWithDiscount = (double)(s.Car.PartsCars.Sum(pc => pc.Part.Price)-
                                                 (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (s.Discount / 100)))

                })
                .ToArray();

            var rootName = "sales";
            var output = Serialize(rootName, sales);

            return output;
        }

        /*                          Helper Methods                  */
        public static string InitializeImportDirectory(string fileName)
            => Path.Combine("../../../Datasets/", fileName);

        public static string InitializeExportDirectory(string fileName)
            => Path.Combine("../../../Results/", fileName);

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }

        public static T Deserialize<T>(string xmlInput, string rootName)
        {
            var xmlRoot = new XmlRootAttribute(rootName);
            var xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

            var xmlReader = new StringReader(xmlInput);
            var output = (T)xmlSerializer.Deserialize(xmlReader);

            return output;
        }

        public static string Serialize<T>(string rootName, T dto)
        {
            var xmlRoot = new XmlRootAttribute(rootName);
            var xmlNameSpaces = new XmlSerializerNamespaces();
            xmlNameSpaces.Add(string.Empty, string.Empty);

            var xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.Indent = true;

            using (var writer = XmlWriter.Create(sb, settings))
            {
                xmlSerializer.Serialize(writer, dto, xmlNameSpaces);
            }

            return sb.ToString().TrimEnd();
        }

        public static decimal Format(decimal number , int precisionPoint)
        {
            number = Math.Floor((number * precisionPoint));
            return number / precisionPoint; 
        }
    }
}
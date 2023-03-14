using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.DTOs.Import.Cars;
using CarDealer.DTOs.Import.Customers;
using CarDealer.DTOs.Import.Parts;
using CarDealer.DTOs.Import.Sales;
using CarDealer.DTOs.Import.Suppliers;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        private static string directory;
        public static void Main()
        {
            var context = new CarDealerContext();
            
            directory = InitializeImportDirectory("sales.xml");

            var xmldata = File.ReadAllText(directory);

            var output = ImportSales(context, xmldata);

            Console.WriteLine(output);
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
            throw new NotImplementedException();
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            throw new NotImplementedException();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            throw new NotImplementedException();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            throw new NotImplementedException();
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            throw new NotImplementedException();
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            throw new NotImplementedException();
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

        public static T Deserialize<T>(string xmlInput, string root)
        {
            var xmlRoot = new XmlRootAttribute(root);
            var xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

            var xmlReader = new StringReader(xmlInput);
            var output = (T)xmlSerializer.Deserialize(xmlReader);

            return output;
        }

        public static string Serialize<T>(string root, T dto)
        {
            var xmlRoot = new XmlRootAttribute(root);
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
    }
}
using AutoMapper;
using CarDealer.DTOs.Export.CarDtos;
using CarDealer.DTOs.Export.CustomerDtos;
using CarDealer.DTOs.Export.PartCarDtos;
using CarDealer.DTOs.Export.PartDtos;
using CarDealer.DTOs.Export.SaleDtos;
using CarDealer.DTOs.Export.SupplierDtos;
using CarDealer.DTOs.Import.CarDtos;
using CarDealer.DTOs.Import.CustomerDtos;
using CarDealer.DTOs.Import.PartDtos;
using CarDealer.DTOs.Import.SaleDtos;
using CarDealer.DTOs.Import.SupplierDtos;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Import
            this.CreateMap<ImportSupplierDto, Supplier>();

            this.CreateMap<ImportPartDto, Part>()
                .ForMember(d => d.SupplierId, mo =>
                    mo.Condition(s => s.SupplierId.HasValue));

            this.CreateMap<ImportCarDto, Car>()
                .ForMember(d => d.TraveledDistance, mo =>
                    mo.MapFrom(s => s.TraveledDistance));

            this.CreateMap<ImportCustomerDto, Customer>();
            this.CreateMap<ImportSaleDto, Sale>();

            //Export
            this.CreateMap<Customer, ExportCustomerDto>()
                .ForMember(d => d.BirthDate, mo => 
                    mo.MapFrom(s => s.BirthDate));

            this.CreateMap<Car, ExportCarDto>()
                .ForMember(d => d.TraveledDistance, mo =>
                    mo.MapFrom(s => s.TraveledDistance));

            this.CreateMap<Supplier, ExportSupplierDto>()
                .ForMember(d => d.PartsCount, mo =>
                    mo.MapFrom(s => s.Parts.Count));

            this.CreateMap<Car, ExportCarJsonShortDto>();
            this.CreateMap<Part, ExportPartShortDto>();

            this.CreateMap<Customer, ExportCustomerSalesDto>()
                .ForMember(d => d.Name, mo =>
                    mo.MapFrom(s => s.Name))
                .ForMember(d => d.BoughtCars, mo =>
                    mo.MapFrom(s => s.Sales.Count))
                .ForMember(d => d.CarIds, mo =>
                    mo.MapFrom(s => s.Sales.Select(sa => sa.CarId)))
                .ForMember(d => d.SpentMoney, mo =>
                    mo.Ignore());


            this.CreateMap<PartCar, ExportPartCarDto>()
                .ForMember(d => d.Price, mo =>
                    mo.MapFrom(s => s.Part.Price));

            this.CreateMap<Car, ExportCarShortDto>();

        }
    }
}

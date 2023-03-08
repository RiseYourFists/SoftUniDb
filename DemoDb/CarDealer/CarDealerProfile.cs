using AutoMapper;
using CarDealer.DTOs.Import.CarDtos;
using CarDealer.DTOs.Import.PartDtos;
using CarDealer.DTOs.Import.SupplierDtos;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDto, Supplier>();

            this.CreateMap<ImportPartDto, Part>()
                .ForMember(d => d.SupplierId, mo =>
                    mo.Condition(s => s.SupplierId.HasValue));

            this.CreateMap<ImportCarDto, Car>()
                .ForMember(d => d.TravelledDistance, mo =>
                    mo.MapFrom(s => s.TraveledDistance));
        }
    }
}

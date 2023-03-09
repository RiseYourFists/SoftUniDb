using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CarDealer.DTOs.Export.CarDtos;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.SaleDtos
{
    [JsonObject]
    public class ExportSalesDto
    {

        [JsonProperty("car")]
        public ExportCarShortDto Car { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("priceWithDiscount")]
        public decimal PriceWithDiscount 
            => this.Price - (this.Price * (this.Discount / 100));

    }
}

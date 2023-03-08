using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Import.SaleDtos
{
    [JsonObject]
    public class ImportSaleDto
    {
        [Required]
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [Required]
        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [Required]
        [JsonProperty("carId")]
        public int CarId { get; set; }

    }
}

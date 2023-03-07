using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Import.PartDtos
{
    [JsonObject]
    public class ImportPartDto
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [Required]
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [Required]
        [JsonProperty("supplierId")]
        public int? SupplierId { get; set; }
    }
}

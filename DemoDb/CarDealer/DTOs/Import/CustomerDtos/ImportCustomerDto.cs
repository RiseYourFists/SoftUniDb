using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Import.CustomerDtos
{
    [JsonObject]
    public class ImportCustomerDto
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [Required]
        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [Required]
        [JsonProperty("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}

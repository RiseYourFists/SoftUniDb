using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Import.SupplierDtos
{
    [JsonObject]
    public class ImportSupplierDto
    {
        [JsonProperty("name")] 
        [Required]
        public string Name { get; set; } = null!;

        [JsonProperty("isImporter")]
        [Required]
        public bool IsImporter { get; set; }
    }
}

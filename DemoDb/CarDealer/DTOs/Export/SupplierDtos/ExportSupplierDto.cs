using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.SupplierDtos
{
    [JsonObject]
    public class ExportSupplierDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = null!;

        [JsonProperty("PartsCount")]
        public int PartsCount { get; set; }
    }
}

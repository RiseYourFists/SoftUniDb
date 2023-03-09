using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.PartDtos
{
    [JsonObject]
    public class ExportPartShortDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Price { get; set; }
    }
}

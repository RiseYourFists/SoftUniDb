using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.CustomerDtos
{
    [JsonObject]
    public class ExportCustomerDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = null!;

        [JsonProperty("BirthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("IsYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}

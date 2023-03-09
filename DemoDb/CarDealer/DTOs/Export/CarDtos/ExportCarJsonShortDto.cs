using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.CarDtos
{
    [JsonObject]
    public class ExportCarJsonShortDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("Make")]
        public string Make { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("TraveledDistance")]
        public long TraveledDistance { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.CarDtos
{
    [JsonObject]
    public class ExportCarDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Make")]
        public string Make { get; set; } = null!;

        [JsonProperty("Model")]
        public string Model { get; set; } = null!;

        [JsonProperty("TraveledDistance")]
        public long TraveledDistance { get; set; }
    }
}

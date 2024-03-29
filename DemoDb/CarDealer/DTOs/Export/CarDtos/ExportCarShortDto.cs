﻿using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.CarDtos
{
    [JsonObject]
    public class ExportCarShortDto
    {
        [JsonProperty("Make")]
        public string Make { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("TraveledDistance")]
        public long TraveledDistance { get; set; }
    }
}

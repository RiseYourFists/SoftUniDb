using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.DTOs.Export.PartDtos;
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export.CarDtos
{
    [JsonObject]
    public class ExportCarInfoDto
    {
        [JsonIgnore]
        public int CarId { get; set; }

        [JsonProperty("car")]
        public ExportCarShortDto Car { get; set; }

        [JsonProperty("parts")]
        public ExportPartShortDto[] Parts { get; set; }
    }
}

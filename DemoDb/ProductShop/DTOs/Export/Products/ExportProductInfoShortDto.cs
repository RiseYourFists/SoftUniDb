using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductShop.DTOs.Export.Products
{
    [JsonObject]
    public class ExportProductInfoShortDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}

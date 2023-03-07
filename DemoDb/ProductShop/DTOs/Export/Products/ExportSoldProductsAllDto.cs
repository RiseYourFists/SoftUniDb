using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductShop.DTOs.Export.Products
{
    [JsonObject]
    public class ExportSoldProductsAllDto
    {
        [NotMapped]
        [JsonProperty("count")]
        public int Count => Products.Any() ? Products.Length : 0;

        [JsonProperty("products")]
        public ExportProductInfoShortDto[] Products { get; set; }
    }
}

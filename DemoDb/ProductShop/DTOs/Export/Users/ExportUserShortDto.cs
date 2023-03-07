using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductShop.DTOs.Export.Products;

namespace ProductShop.DTOs.Export.Users
{
    [JsonObject]
    public class ExportUserShortDto
    {
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("soldProducts")]
        public ExportSoldProductsAllDto SoldProducts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductShop.DTOs.Import
{
    [JsonObject]
    public class ImportCategoryProductDto
    {
        [JsonProperty("CategoryId")]
        [Required]
        public int CategoryId { get; set; }

        [JsonProperty("ProductId")]
        [Required]
        public int ProductId { get; set; }
    }
}

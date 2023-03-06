using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductShop.DTOs.Import
{
    public class ImportCategoryDto
    {
        [JsonProperty("name")]
        [Required] 
        public string Name { get; set; } = null!;
    }
}

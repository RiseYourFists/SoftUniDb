using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductShop.DTOs.Export.Users
{
    [JsonObject]
    public class ExportUserAllDto
    {
        [JsonProperty("usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty("users")]
        public ExportUserShortDto[] Users { get; set; }
    }
}

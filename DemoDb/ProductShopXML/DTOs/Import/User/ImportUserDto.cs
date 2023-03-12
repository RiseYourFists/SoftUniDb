using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import.User
{
    [XmlType("User")]
    public class ImportUserDto
    {
        [XmlElement("firstName")]
        [Required]
        public string FirstName { get; set; } = null!;

        [XmlElement("lastName")]
        [Required]
        public string LastName { get; set; } = null!;

        [XmlElement("age")]
        public int? Age { get; set; }
    }
}

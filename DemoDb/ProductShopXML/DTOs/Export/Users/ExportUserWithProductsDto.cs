using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProductShop.DTOs.Export.Products;

namespace ProductShop.DTOs.Export.Users
{
    [XmlType("User")]
    public class ExportUserWithProductsDto
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public ExportSoldProductsDto SoldProducts { get; set; }
    }
}

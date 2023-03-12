using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import.Product
{
    [XmlType("Product")]
    public class ImportProductDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyerId")]
        public int? BuyerId { get; set; }

        [Required]
        [XmlElement("sellerId")]
        public int SellerId { get; set; }
    }
}

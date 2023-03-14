using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import.Parts
{
    [XmlType("Part")]
    public class ImportPartDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("price")]
        public decimal Price { get; set; }

        [Required]
        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [Required]
        [XmlElement("supplierId")]
        public int SupplierId { get; set; }
    }
}

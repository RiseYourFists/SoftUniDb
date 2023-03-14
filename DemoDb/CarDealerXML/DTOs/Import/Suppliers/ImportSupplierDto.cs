﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import.Suppliers
{
    [XmlType("Supplier")]
    public class ImportSupplierDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}

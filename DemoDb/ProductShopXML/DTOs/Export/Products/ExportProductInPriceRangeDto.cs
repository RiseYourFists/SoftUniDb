﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export.Products
{
    [XmlType("Product")]
    public class ExportProductInPriceRangeDto
    {
        [XmlElement("name")] 
        public string Name { get; set; } = null!;

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")] 
        public string BuyerFullName { get; set; } = null!;

        //public bool ShouldSerializeBuyerFullName()
        //    => !string.IsNullOrWhiteSpace(this.BuyerFullName);
    }
}

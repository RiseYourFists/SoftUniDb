﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import.Category
{
    [XmlType("Category")]
    public class ImportCategoryDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; } = null!;
    }
}

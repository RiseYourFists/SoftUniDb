﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CarDealer.DTOs.Export.Parts;

namespace CarDealer.DTOs.Export.Cars
{
    [XmlType("car")]
    public class ExportCarWithPartsDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("traveled-distance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public ExportPartDto[] Parts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CarDealer.DTOs.Import.Parts;

namespace CarDealer.DTOs.Import.Cars
{
    [XmlType("Car")]
    public class ImportCarDto
    {
        [Required]
        [XmlElement("make")]
        public string Make { get; set; } = null!;

        [Required]
        [XmlElement("model")]
        public string Model { get; set; } = null!;

        [XmlElement("traveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public ImportPartIdDto[] Parts { get; set; }

    }
}

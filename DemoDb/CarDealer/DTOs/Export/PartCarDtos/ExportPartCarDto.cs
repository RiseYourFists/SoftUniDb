using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Export.PartCarDtos
{
    public class ExportPartCarDto
    {
        public int CarId { get; set; }
        public int PartId { get; set; }
        public decimal Price { get; set; }
    }
}

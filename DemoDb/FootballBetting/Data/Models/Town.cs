using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Town
    {
        public Town()
        {
            Teams = new HashSet<Team>();
        }
        //⦁	Town – TownId, Name, CountryId
        [Key]
        public int TownId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        /*Relations*/
        [Required]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

    }
}

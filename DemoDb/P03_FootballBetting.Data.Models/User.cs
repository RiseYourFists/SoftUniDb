using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            Bets = new HashSet<Bet>();
        }
        //⦁	User – UserId, Username, Password, Email, Name, Balance
        [Key]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(35)")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        public decimal Balance { get; set; }

        /*Relations*/
        public virtual ICollection<Bet> Bets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Bet
    {
        //⦁	Bet – BetId, Amount, Prediction, DateTime, UserId, GameId
        [Key]
        public int BetId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(100)]
        public string Prediction { get; set; }
        [Required]
        public DateTime DateTime { get; set; }


        /*Relations*/
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}

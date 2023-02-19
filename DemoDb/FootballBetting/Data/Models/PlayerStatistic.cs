﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        //⦁	PlayerStatistic – GameId, PlayerId, ScoredGoals, Assists, MinutesPlayed

        [Required]
        public int ScoredGoals { get; set; }

        [Required]
        public int Assists { get; set; }

        [Required]
        public int MinutesPlayed { get; set; }


        /*Relations*/

        [Required]
        public int PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }
        [Required]

        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }


    }
}

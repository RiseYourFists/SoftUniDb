using System;
using System.Linq;
using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new FootballBettingContext();

            var players = context.Players
                .Where(p => !p.IsInjured);
        }
    }
}

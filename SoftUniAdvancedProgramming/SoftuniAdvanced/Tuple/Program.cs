using System;
using System.Collections.Immutable;
using System.Linq;

namespace Tuple
{
    public class Program
    {
        static void Main(string[] args)
        {
            var personInfo = Console.ReadLine().Split();
            var name = personInfo.Take(2).Aggregate((x, y) => $"{x} {y}");
            var adress = personInfo[2];
            var city = personInfo.Last();

            var beerInfo = Console.ReadLine().Split();
            var drunk = beerInfo.Last() == "drunk";

            var bankInfo = Console.ReadLine().Split();

            var person = new MyTuple<string, string, string>(name, adress, city);
            var beer = new MyTuple<string, int, bool>(beerInfo[0], int.Parse(beerInfo[1]), drunk);
            var num = new MyTuple<string, double, string>(bankInfo[0], double.Parse(bankInfo[1]), bankInfo[2]);

            Console.WriteLine(person.ToString());
            Console.WriteLine(beer.ToString());
            Console.WriteLine(num.ToString());
        }
    }
}

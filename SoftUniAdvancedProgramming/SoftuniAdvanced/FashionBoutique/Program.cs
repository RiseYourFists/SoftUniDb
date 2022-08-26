using System;
using System.Collections.Generic;
using System.Linq;

namespace FashionBoutique
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clothesBox = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var clothesStack = new Stack<int>(clothesBox);
            var rackCapacity = int.Parse(Console.ReadLine());

            if(rackCapacity <= 0)
            {
                Console.WriteLine(0);
                return;
            }

            var racksPacked = 1;
            var currRackSum = 0;

            while (clothesStack.Count > 0)
            {
                var currCloth = clothesStack.Pop();

                if(currCloth + currRackSum > rackCapacity)
                {
                    currRackSum = currCloth;
                    racksPacked++;
                    continue;
                }

                currRackSum += currCloth;
            }

            Console.WriteLine(racksPacked);

        }
    }
}

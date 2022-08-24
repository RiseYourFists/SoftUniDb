using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicStackOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputParameters = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var elementsCount = inputParameters[0];
            var elementsToPop = inputParameters[1];
            var target = inputParameters[2];

            var stackInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var numberStack = new Stack<int>(stackInput);

            if(!HasElements(numberStack))
            {
                Console.WriteLine(0);
                return;
            }

            for (int i = 0; i < elementsToPop; i++)
            {
                numberStack.Pop();
            }

            if (!HasElements(numberStack))
            {
                Console.WriteLine(0);
                return;
            }

            var smallest = int.MaxValue;

            foreach (var item in numberStack)
            {
                if (item == target)
                {
                    Console.WriteLine("true");
                    return;
                }

                if (item < smallest)
                {
                    smallest = item;
                }
            }

            Console.WriteLine(smallest);
        }

        private static bool HasElements(Stack<int> numberStack)
        {
            if(numberStack.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}

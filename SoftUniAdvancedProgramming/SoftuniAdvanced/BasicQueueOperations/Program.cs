using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicQueueOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputParameters = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var elementsCount = inputParameters[0];
            var elementsToPop = inputParameters[1];
            var target = inputParameters[2];

            var queueInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var numberQueue = new Queue<int>(queueInput);

            if (!HasElements(numberQueue))
            {
                Console.WriteLine(0);
                return;
            }
            for (int i = 0; i < elementsToPop; i++)
            {
                numberQueue.Dequeue();
            }

            if (!HasElements(numberQueue))
            {
                Console.WriteLine(0);
                return;
            }
            var smallest = int.MaxValue;

            foreach (var item in numberQueue)
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

        private static bool HasElements(Queue<int> numberStack)
        {
            if (numberStack.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}

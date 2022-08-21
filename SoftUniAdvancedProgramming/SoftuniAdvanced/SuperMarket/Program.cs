using System;
using System.Collections.Generic;

namespace SuperMarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customerQueue = new Queue<string>();

            while (true)
            {
                var input = Console.ReadLine();

                if(input == "End")
                {
                    break;
                }

                if(input == "Paid")
                {
                    PrintAndClearCustomerList(customerQueue);
                    continue;
                }

                customerQueue.Enqueue(input);
            }

            Console.WriteLine($"{customerQueue.Count} people remaining.");
        }

        private static void PrintAndClearCustomerList(Queue<string> customerQueue)
        {
            while (customerQueue.Count > 0)
            {
                Console.WriteLine(customerQueue.Dequeue());
            }
        }
    }
}

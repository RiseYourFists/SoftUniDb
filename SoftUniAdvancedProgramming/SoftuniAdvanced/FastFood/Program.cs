using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFood
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var initialFoodQuantity = int.Parse(Console.ReadLine());

            var orders = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var orderQueue = new Queue<int>(orders);
            var biggest = int.MinValue;
            while (orderQueue.Count > 0)
            {
                if(biggest < orderQueue.Peek())
                {
                    biggest = orderQueue.Peek();
                }

                if(orderQueue.Peek() > initialFoodQuantity)
                {
                    break;
                }

                initialFoodQuantity -= orderQueue.Dequeue();
            }

            Console.WriteLine(biggest);

            if(orderQueue.Count > 0)
            {
                Console.Write("Orders left: ");

                while (orderQueue.Count > 0)
                {
                    Console.Write(orderQueue.Dequeue() + " ");
                }
                return;
            }

            Console.WriteLine("Orders complete");
        }
    }
}

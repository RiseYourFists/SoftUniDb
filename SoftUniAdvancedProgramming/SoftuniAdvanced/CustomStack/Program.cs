using System;
using System.Linq;

namespace CustomStack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            var myStack = new MyStack<int>();

            while ((input = Console.ReadLine()) != "END")
            {
                var tokens = input.Split(" ");
                switch (tokens[0])
                {
                    case "Push":

                        var items = string.Join("", tokens.Skip(1));
                        var elements = items.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                        elements.ForEach(x => myStack.Push(x));
                        break;

                    case "Pop":

                        try { myStack.Pop(); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); };
                        break;
                }
            }

            for (int i = 1; i <= 2; i++)
            {
                Print(myStack);
            }
        }

        private static void Print(MyStack<int> myStack)
        {
            foreach (var item in myStack)
            {
                Console.WriteLine(item);
            }
        }
    }
}

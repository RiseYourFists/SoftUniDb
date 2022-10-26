using System;
using System.Linq;

namespace IteratosAndComparators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');
            var command = input[0];
            var collection = new ListyIterator<string>(input.Skip(1).ToArray());

            while (command != "END")
            {
                switch (command)
                {
                    case "HasNext":
                        Console.WriteLine(collection.HasNext());
                        break;
                    case "Print":
                        collection.Print();
                        break ;
                    case "Move":
                        Console.WriteLine(collection.Move());
                        break;
                    case "PrintAll":
                        foreach (var item in collection)
                        {
                            Console.Write($"{item} ");
                        }
                        Console.WriteLine();
                        break;
                }

                command = Console.ReadLine();
            }
        }
    }
}

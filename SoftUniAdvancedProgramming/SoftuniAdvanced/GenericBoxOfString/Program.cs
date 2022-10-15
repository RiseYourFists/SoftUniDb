using System;

namespace GenericBoxOfString
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
           var array = new Box<int>[size];

            for (int i = 0; i < size; i++)
            {
                var num = int.Parse(Console.ReadLine());
                array[i] = new Box<int>(num);
            }

            foreach (var item in array)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}

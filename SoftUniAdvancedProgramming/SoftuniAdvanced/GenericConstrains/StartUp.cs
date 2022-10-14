namespace GenericScale
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var equal1 = new EqualityScale<int>(5, 5);
            Console.WriteLine(equal1.AreEqual());
        }
    }
}

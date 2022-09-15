using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var shops = new Dictionary<string, Dictionary<string, double>>();

            while (true)
            {
                var input = Console.ReadLine();

                if(input == "Revision")
                {
                    break;
                }

                var tokens = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                var shopName = tokens[0];
                var product = tokens[1];
                var productPrice = double.Parse(tokens[2]);

                if(shops.ContainsKey(shopName))
                {
                    if (shops[shopName].ContainsKey(product))
                    {
                        shops[shopName][product] = productPrice;
                        continue;
                    }

                    shops[shopName].Add(product, productPrice);
                    continue;
                }

                shops.Add(shopName, new Dictionary<string, double>());
                shops[shopName].Add(product, productPrice);
            }

            var result = shops.OrderBy(x => x.Key).ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}->");
                foreach (var product in item.Value)
                {
                    Console.WriteLine($"Product: {product.Key}, Price: {product.Value}");
                }
            }
        }
    }
}

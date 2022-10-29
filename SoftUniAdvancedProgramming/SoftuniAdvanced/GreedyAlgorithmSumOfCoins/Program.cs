namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class StartUp
    {
        static void Main(string[] args)
        {
            var coinFormat = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var desiredSum = int.Parse(Console.ReadLine());

            try
            {
                var coinHolder = ChooseCoins(coinFormat, desiredSum);
                var result = coinHolder.Where(x => x.Value > 0).ToList();

                Console.WriteLine($"Number of coins to take: {result.Sum(x => x.Value)}");

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Value} coin(s) with value {item.Key}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Dictionary<int, int> ChooseCoins(List<int> coinFormat, int desiredSum)
        {
            var coinCollection = new Dictionary<int, int>();
            coinFormat.Sort();
            coinFormat.Reverse();

            var currentSum = desiredSum;
            var coinIndex = 0;
            while (currentSum > 0 && coinIndex < coinFormat.Count)
            {
                var currCoin = coinFormat[coinIndex];
                if (!coinCollection.ContainsKey(currCoin))
                {
                    coinCollection.Add(currCoin, 0);
                }
                coinCollection[currCoin] = currentSum / currCoin;
                currentSum -= coinCollection[currCoin] * currCoin;

                coinIndex++;
            }

            if (currentSum != 0)
            {
                throw new InvalidOperationException("Error");
            }

            return coinCollection;
        }
    }
}

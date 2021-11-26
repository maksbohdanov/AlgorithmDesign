using System;
using static KnapsackProblem.Constants;

namespace KnapsackProblem
{
    public class Item : ICloneable
    {
        public int Profit { get; set; }
        public int Weight { get; set; }
        public double Ratio { get; set; }
        public double Probability { get; set; }
        public bool Included { get; set; }

        public Item()
        {
            var rand = new Random();
            Profit = rand.Next(ProfitBottom, ProfitUpper);
            Weight = rand.Next(WeightBottom, WeightUpper);
            Ratio = (double)Profit / Weight;
        }

        public static Item[] GenerateRandomItems()
        {
            var rand = new Random();
            var result = new Item[Quantity];

            for (int i = 0; i < result.Length;  i++)
            {
                result[i] = new Item();
                if (rand.NextDouble() > 0.5) result[i].Included = true;
            }

            return result;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

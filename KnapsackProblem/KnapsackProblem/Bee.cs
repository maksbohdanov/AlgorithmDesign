using System;
using System.Linq;
using static KnapsackProblem.Constants;

namespace KnapsackProblem
{
    public class Bee : ICloneable
    {
        public Item[] Items { get; set; } = new Item[Quantity];
        private int profit;
        public int Profit
        {
            get => profit;
            set => profit = value;

        }
        public double Probability { get; set; }


        public Bee()
        {
            for (int i = 0; i < Quantity; i++)
            {
                Items[i] = new Item();
            }
        }
        public Bee(Item[] items)
        {
            Items = items;
            CalculateItemsProbabilities();
            profit = GetTotalProfit();
        }

        public int GetTotalWeight() => Items.Where(x => x.Included).Sum(s => s.Weight);
        private int GetTotalProfit() => Items.Where(x => x.Included).Sum(s => s.Profit); 
        private double GetMeanRatio() => Items.Where(x => x.Included).Average(p => p.Ratio);
        public bool IsPacked() => GetTotalWeight() > P;
        public double GetProbabilitiesSum() => Items.Sum(p => p.Probability);
        public void CalculateItemsProbabilities()
        {
            foreach (var item in Items)
            {
                item.Probability = (double)GetTotalWeight() / P * GetMeanRatio() / item.Ratio;
            }
        }
        public void UpdateProfit() => profit = GetTotalProfit();
    

        public void Fit()
        {            
            if ( !IsPacked() ) return;

            var rand = new Random();
            while ( IsPacked() )
            {
                var index = rand.Next(0, Items.Length);
                if ( Items[index].Included ) Items[index].Included = false; 
            }
        }        
        public object Clone()
        {
            var newItems = new Item[Quantity];
            for (int i = 0; i < newItems.Length; i++)
            {
                newItems[i] = (Item)Items[i].Clone();
            }
            Bee newBee = new Bee
            {
                Probability = this.Probability,
                Profit = this.Profit,
                Items = newItems
            };            
            return newBee;
            
        }
    }
}

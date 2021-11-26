using System;
using System.Collections.Generic;
using System.Linq;
using static KnapsackProblem.Constants;

namespace KnapsackProblem
{
    public class ABC_Algorithm
    {
        public Parameter Solve(int workBeesAmount, int scoutBeesAmount, int areasAmount, int iterations)
        {
            var areas = GenerateInitialSolutions(areasAmount);
            var bestSolution = areas.OrderByDescending(p => p.Profit).ToList()[0];

            for (int i = 0; i < iterations; i++) // Loop through iterations
            {
                
                var scouts = FlowerDance(areas, scoutBeesAmount);               


                var sum = ProfitsSum(scouts); // calculate bees probabilities 
                foreach (var bee in scouts) { bee.Probability = Math.Pow(bee.Profit, 2) / sum;  }

                for (int j = 1; j < scouts.Count; j++) { scouts[j].Probability += scouts[j - 1].Probability; }

                var indexesAndItems = new List<(int, Bee)>();

                for (int k = 0; k < workBeesAmount; k++) //Loop through work bees
                {
                    var beeIndex = ChooseArea(scouts);
                    var scoutInAreas = areas.IndexOf(scouts[beeIndex]);
                    var workBee = (Bee)scouts[beeIndex].Clone();
                    workBee.CalculateItemsProbabilities();
                    
                    var changeableAmount = ProbabilityToChange(i, iterations);
                    for (int y = 0; y < changeableAmount; y++)
                    {
                        var itemIndex = ChooseItem(workBee);                        
                        workBee.Items[itemIndex].Included = !workBee.Items[itemIndex].Included;
                        workBee.UpdateProfit();
                        
                        if (!(workBee.IsPacked()) && workBee.Profit > scouts[beeIndex].Profit)
                        {
                            var tuple = (scoutInAreas, workBee);
                            indexesAndItems.Add(tuple);
                        }
                    }                    
                }

                foreach (var tuple in indexesAndItems)
                {
                    var index = tuple.Item1;
                    var bee = tuple.Item2;
                    if (areas[index].Profit + bee.Profit <= P) areas[index] = bee;
                }               



                bestSolution = areas.OrderByDescending(p => p.Profit).ToList()[0];               
            }
            
            Console.WriteLine($"{workBeesAmount}\t{scoutBeesAmount}\t{areasAmount}\t{bestSolution.Profit}\t{bestSolution.GetTotalWeight()}");
            return new Parameter
            {
                Workers = workBeesAmount,
                Scouts = scoutBeesAmount,
                Areas = areasAmount,
                Profit = bestSolution.Profit,
                Weight = bestSolution.GetTotalWeight(),
            };
    }

        private List<Bee> GenerateInitialSolutions(int amount)
        {
            var solutions = new List<Bee>();
            for (int i = 0; i < amount; i++)
            {
                var bee = new Bee( Item.GenerateRandomItems() );
                bee.Fit();
                solutions.Add(bee);
            }
            return solutions;
        }

        private List<Bee> FlowerDance(List<Bee> bees, int scouters)
        {
            var selectedBees = new List<Bee>();
            var bestAmount = (int)Math.Round(scouters * 0.75);
            var randomAmount = scouters - bestAmount;

            selectedBees = bees.OrderByDescending(b => b.Profit).Take(bestAmount).ToList();

            var rand = new Random();
            for (int i = 0; i < randomAmount; i++)
            {
                var index = rand.Next(0, bees.Count);
                selectedBees.Add( bees[index] );
            }
            return selectedBees;
        }
        private int ProfitsSum(List<Bee> bees) => bees.Sum(p => (int)Math.Pow(p.Profit,2) ) ;
        private int ChooseArea(List<Bee> scouts)
        {
            if (scouts.Count < 2) return 0;
            
            var randomNum = new Random().NextDouble();
            var index = 1;
            while (scouts[index].Probability <= randomNum)
            {
                index++;
            }
            return index;
        }
        private int ChooseItem(Bee bee)
        {
            var sum = bee.GetProbabilitiesSum();

            foreach (var item in bee.Items) { item.Probability /=  sum; }

            for (int i = 1; i < bee.Items.Length; i++) { bee.Items[i].Probability += bee.Items[i - 1].Probability; }

            var randomNum = new Random().NextDouble();
            var index = 1;
            while (bee.Items[index].Probability <= randomNum)
            {
                index++;
            }
            return index;
        }
        private  int ProbabilityToChange(int currentIteration, int maxIteration)
        {
            var rand = new Random().NextDouble();
            var prob = (double)(maxIteration - currentIteration) / maxIteration ;
            if (rand <  prob) return  (int)Math.Round(prob * 6) ;
            else return 1;
        }                
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using static Lab3.Constants;



namespace Lab3
{
    public class Map
    {
        public int Lmin { get; set; }
        public int Lcurrent { get; set; }
        private bool[] VisitedCities { get; set; } = new bool[N];
        private bool IsFinished { get; set; } = false;
        public List<List<Road>> Roads { get; set; } = new List<List<Road>>();
        public bool[] AntsInCities { get; set; } = new bool[N];

        public Map()
        {
            for (int i = 0; i < N; i++)
            {
                var listToAdd = new List<Road>();
                for (int j = 0; j < N; j++)
                {
                    var road = new Road(new Point(i, j));
                    listToAdd.Add(road);
                }
                Roads.Add(listToAdd);
            }

            for (int i=0; i< M; i++) // place ants in random cities
            {
                var rand = new Random();
                bool flag = true;
                while (flag)
                {
                    var city = rand.Next(0, N);
                    if (!AntsInCities[city])
                    {
                        AntsInCities[city] = true;
                        flag = false;
                    }
                }
               
            }
            FindMinPath();
        }


        private void FindMinPath(int index = 0)
        {

            VisitedCities[index] = true;
            var city = Roads[index]; // row

            var sortedRoads = city.OrderBy(s => s.Length);
            foreach (var item in sortedRoads)
            {
                if (VisitedCities[city.IndexOf(item)]) continue;

                Lmin += item.Length;
                
                FindMinPath( city.IndexOf(item) ); 
            }

            if ( VisitedCities.All(a => true) && !IsFinished)
            {
                IsFinished = true;
                Lmin += city[0].Length;
            }

        }
        private void FindMinPathByPheromones(int index = 0)
        {
            //Console.Write($"{index} ");
            VisitedCities[index] = true;
            
            var sortedRoads = Roads[index].OrderByDescending(s => s.Pheromone);
            foreach (var item in sortedRoads)
            {
                if (VisitedCities[Roads[index].IndexOf(item)]) continue;

                Lcurrent += item.Length;

                FindMinPathByPheromones(Roads[index].IndexOf(item));
            }
            if (VisitedCities.All(a => true) && !IsFinished)
            {
                IsFinished = true;
                Lcurrent += Roads[index][0].Length;
            }
            

        }
    
        private void FindPath(int index, Ant ant)
        {
            if (!VisitedCities[index]) Console.Write($"{index} ");

            VisitedCities[index] = true;
            ant.VisitedCities[index] = true;

            var city = Roads[index]; // row
            var sum = ProbabilitiesSum(ant, city);
            ant.TransitionProbabilities = Enumerable.Repeat(0.0, N).ToList();

            double totalSum = 0;
            foreach (var road in city) // calculate probabilities
            {
                if (ant.VisitedCities[road.FromTo.Y]) continue;      
                
                

                var probability = Math.Pow(road.Pheromone, Alpha) * Math.Pow(road.Visibility, Beta) / sum;
                totalSum += probability;
               
                ant.TransitionProbabilities[road.FromTo.Y] = probability;                
            }

            for (int i = 1; i < N; i++)
            {
                ant.TransitionProbabilities[i] += ant.TransitionProbabilities[i - 1];
            }
            Random rand = new Random();
            var randPath = rand.NextDouble();
            var flag = true;

            foreach (var probab in ant.TransitionProbabilities)
            {
                var checkIndex = ant.TransitionProbabilities.IndexOf(probab);
                if (checkIndex == -1 || ant.VisitedCities[ant.TransitionProbabilities.IndexOf(probab)]) continue;

                if(flag && randPath <= probab)
                {
                    var nextIndex = ant.TransitionProbabilities.IndexOf(probab);
                    var nextRoad = Roads[index][nextIndex];
                    ant.VisitedRoads.Add( nextRoad );
                    ant.PathLength += nextRoad.Length;
                    flag = false;
                    FindPath(nextIndex, ant);
                }
            }
           
            if ( ant.CheckIfFinished() )
            {
                var firstRoad = ant.VisitedRoads.First();
                var lastRoad = ant.VisitedRoads.Last();

                ant.PathLength += Roads[lastRoad.FromTo.Y][firstRoad.FromTo.X].Length;
                return;
            }
        }

        private double ProbabilitiesSum(Ant ant, List<Road> city)
        {
            double result = 0;
            foreach (var road in city)
            {
                if (ant.VisitedCities[road.FromTo.Y]) continue;
                result += Math.Pow(road.Pheromone, Alpha) * Math.Pow(road.Visibility, Beta);
            }
            return result;
        }

        public void FindSolution(int iteration)
        {          
            var ants = new List<Ant>();
            foreach (var individ in AntsInCities) // START loop by ants
            {
                if (individ)
                {
                    var ant = new Ant();                    
                    var cityIndex = Array.IndexOf(AntsInCities, individ);
                    FindPath(cityIndex, ant);
                    ants.Add(ant);

                }
            }// END loop by ants

            //looking for Lcurrent
            for (int i = 0; i < VisitedCities.Length; i++)   

            {
                VisitedCities[i] = false;
            }
            Lcurrent = 0;
            FindMinPathByPheromones();

            foreach (var city in Roads)   //START loop by pheromone
            {
                foreach (var road in city)
                {
                    road.Pheromone *= (1 - P);
                }
            }

            foreach (var ant in ants)
            {
                foreach (var road in ant.VisitedRoads)
                {
                    road.Pheromone = road.Pheromone + 1.0 * Lmin / ant.PathLength;

                }
            }            //END loop by pheromone
                       

            if (iteration % 20 == 0 || iteration <=20 )
            {
                //Console.WriteLine($"{iteration}:\tL = {Lcurrent}");
                Console.WriteLine($"{iteration} {Lcurrent}");

                //foreach (var city in Roads)      //printing pheromone map
                //{
                //    foreach (var road in city)
                //    {
                //        Console.Write(Math.Round(road.Pheromone, 4) + " ");
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine();
            }

        }

    }
}

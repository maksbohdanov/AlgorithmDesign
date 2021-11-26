using System;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackProblem
{
    class Program
    {
        static void Main(string[] args)
        {            
            var AlgorithmProcessor = new ABC_Algorithm();

            Console.WriteLine("Workers\tScouts\tAreas\tProfit\tWeight");
            var solution = AlgorithmProcessor.Solve(45, 4, 35, 100);
            //var solutions = new List<Parameter>();
            //for (int i = 5; i <= 100; i += 5)
            //{
            //    var solution = AlgorithmProcessor.Solve(50, 5, i, 100);
            //    solutions.Add(solution);
            //}

            //var bestArea = solutions.OrderByDescending(s => s.Profit).ToList()[0];
            //Console.WriteLine($"\nBest amount of areas = {bestArea.Areas}\t Profit: {bestArea.Profit}\n");

            //solutions.Clear();
            //for (int i = 10; i <= 100; i += 5)
            //{
            //    var solution = AlgorithmProcessor.Solve(i, i / 10, bestArea.Areas, 100);
            //    solutions.Add(solution);
            //}

            //var bestBees = solutions.OrderByDescending(s => s.Profit).ToList()[0];
            //Console.WriteLine($"\nBest amount of bees:\nWorkers = {bestBees.Workers}\tScouts = {bestBees.Scouts}\t Profit = {bestArea.Profit}\n");


        }
    }
}

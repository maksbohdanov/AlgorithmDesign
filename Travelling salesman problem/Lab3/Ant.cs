using System;
using System.Collections.Generic;
using static Lab3.Constants;
using System.Linq;

namespace Lab3
{
    public class Ant
    {
        public List<Road> VisitedRoads { get; set; } = new List<Road>();
        public bool[] VisitedCities { get; set; } = new bool[N];
        public int PathLength { get; set; }
        private bool IsFinished { get; set; } = false;
        public List<double> TransitionProbabilities { get; set; } = new List<double>();

       
        public bool CheckIfFinished()
        {
            if (VisitedCities.All(a => true) ) IsFinished = true;
            return IsFinished;
        }
    }
}

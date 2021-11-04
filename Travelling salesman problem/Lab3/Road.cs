using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using static Lab3.Constants;

namespace Lab3
{
    public class Road
    {
        public Point FromTo { get; set; }
        public int Length { get; set; }
        public double Visibility { get; set; }
        public double Pheromone { get; set; }        

        public Road(Point point)
        {
            FromTo = point;

            var rand = new Random();
            if(point.X != point.Y)
            {
                Length = rand.Next(BottomBound, UpperBound + 1);
                Visibility = 1.0 / Length;

            }
            Pheromone = 0.3;
        }
    }

}

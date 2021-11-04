using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using static Lab3.Constants;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {                        
            var map = new Map();
            Console.WriteLine("Lmin = " + map.Lmin +"\n\n");

            for (int i = 0; i < 1001; i++)
            {
                map.FindSolution(i);
            }

        }
    }
}

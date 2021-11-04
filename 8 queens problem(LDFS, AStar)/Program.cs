using System;
using System.Diagnostics;
using System.Collections.Generic;
 

namespace Lab1
{

    class Program
    {

        static void Main(string[] args)
        {
            for (int i = 0; i < 2; i++)
            {
                var timer = new Stopwatch();
                int[] board = new int[8];

                for (int j = 0; j < board.Length; j++)
                {
                    board[j] = new Random().Next(0, 8);
                }


                PrintBoard(board);

                Console.WriteLine("\n\nLDSF:\n");

                timer.Start();

                var solvedLDFS = LDFS.FindSolution(board);

                timer.Stop();
                Console.WriteLine($"Time: {timer.Elapsed.TotalSeconds} s");
                PrintBoard(solvedLDFS.State);

                ////////


                Console.WriteLine("\n\nA*:\n");

                timer.Start();

                var solvedAStar = AStar.FindSolution(board);

                timer.Stop();
                Console.WriteLine($"Time: {timer.Elapsed.TotalSeconds} s");
                PrintBoard(solvedAStar.State);

                Console.WriteLine("\n__________________________________________________________________________________________\n\n");



            }
        }

        static void PrintBoard(int[] board)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((j + i) % 2 == 0 && board[j] == i)
                    {
                        Console.Write(" X");                        
                    }
                    else if ((j + i) % 2 == 0 && board[j] != i)
                    {
                        Console.Write(" ∙");                       
                    }
                    else if ((i + j) % 2 != 0 && board[j] != i)
                    {
                        Console.Write(" ∙");                       
                    }
                    else
                    {
                        Console.Write(" X");                        
                    }
                }                
                Console.WriteLine();
            }
            Console.ResetColor();

        }

    }
}
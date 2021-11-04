using DotNetty.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public static class AStar
    {
        public static int BoardSize { get; } = 8;
        public static int Iterations { get; set; }
        public static int StatesInMemory { get; set; }
        public static int States { get; set; } 

        public static NodeAstar FindSolution(int[] state)
        {
            NodeAstar start = new NodeAstar(state);
            NodeAstar current = start;
            start.Depth = 0;
            PriorityQueue<NodeAstar> open = new PriorityQueue<NodeAstar>(new NodeAstarComparer());
            HashSet<NodeAstar> closed = new HashSet<NodeAstar>();

            while(current.Heuristic != 0)
            {
                Iterations++;

                for (int i = 1; i < state.Length; i++)
                {
                    int[] nextState = new int[8];
                    Array.Copy(current.State, nextState, 8);

                    if (current.Depth >= 8) current.Depth -= 8;
                    nextState[current.Depth] += i;

                    if (nextState[current.Depth] >= 8) nextState[current.Depth] -= 8;

                    NodeAstar next = new NodeAstar(nextState, current);
                    current.Accessors.Add(next);
                    closed.Add(current);
                    States++;
                }

                foreach (var el in current.Accessors)
                {
                    open.Enqueue(el);
                }
                NodeAstar nextNode;

                while (true)
                {
                    nextNode = open.Dequeue();
                    StatesInMemory++;
                    if (closed.Contains(nextNode))
                    {
                        Console.WriteLine("STOP");
                        continue;
                    }
                    else break;
                }
                current = nextNode;
            }
            Console.WriteLine("\nIterations: " + Iterations);
            Console.WriteLine("States:" + States);
            Console.WriteLine($"States in memory: {States - StatesInMemory}");

            return current;

        }


    }

    class NodeAstarComparer : IComparer<NodeAstar>
    {
        public int Compare(NodeAstar node1, NodeAstar node2)
        {
            if (node1.Heuristic > node2.Heuristic)
                return 1;
            else if (node1.Heuristic < node2.Heuristic)
                return -1;
            else
                return 0;
        }
                
    }

}


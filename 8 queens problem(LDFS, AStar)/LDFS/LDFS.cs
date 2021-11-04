using System;

namespace Lab1
{
    public static class LDFS
    {
        public static int Iterations { get; set; }
        public static int States { get; set; } = 1;
        public static int StatesInMemory { get; set; } = 1;
        public static int LimitReachedCounter { get; set; }

        public static int BoardSize { get; } = 8;


        public static NodeLDFS FindSolution(int[] state)
        {
            NodeLDFS start = new NodeLDFS(state);
            NodeLDFS current = start;
            int depth = 0;

            while (!current.IsGoal())
            {
                Iterations++;

                if (depth == BoardSize)
                {
                    current = current.Parent;
                    depth--;
                    LimitReachedCounter++;
                    continue;
                }

                if (!current.IsVisited)
                {
                    for (int i = 1; i < state.Length; i++)
                    {
                        int[] nextState = new int[8];
                        Array.Copy(current.State, nextState, 8);

                        nextState[depth] += i;

                        if (nextState[depth] >= 8) nextState[depth] -= 8;

                        NodeLDFS next = new NodeLDFS(nextState, current);
                        current.Successors.Push(next);

                        States++;

                    }
                    current.IsVisited = true;
                }

                if (current.Successors.Count != 0)
                {
                    current = current.Successors.Pop();
                    StatesInMemory++;
                    depth++;
                }
                else
                {
                    current = current.Parent;
                    depth--;
                }
            }

            Console.WriteLine("\nIterations: " + Iterations);
            Console.WriteLine("States: " + States);
            Console.WriteLine($"States in memory: { States - StatesInMemory}");
            Console.WriteLine("Depth limit: " + LimitReachedCounter);

            return current;
        }

    }

}


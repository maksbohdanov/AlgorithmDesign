using System.Collections.Generic;

namespace Lab1
{
    public class NodeLDFS
    {
        public int[] State { get; set; }
        public NodeLDFS Parent { get; set; }
        public Stack<NodeLDFS> Successors { get; set; } = new Stack<NodeLDFS>();
        public bool IsVisited { get; set; }


        public NodeLDFS(int[] state)
        {
            State = state;

        }
        public NodeLDFS(int[] state, NodeLDFS parent)
        {
            State = state;
            Parent = parent;
        }




        public bool IsGoal()
        {
            for (int i = 0; i < State.Length; i++)
            {
                for (int j = 0; j < State.Length; j++)
                {
                    if (i == j) continue;

                    if (State[i] == State[j] || State[i] + (j - i) == State[j] || State[i] - (j - i) == State[j])
                    {
                        return false;
                    }

                }
            }
            return true;
        }


    }

}





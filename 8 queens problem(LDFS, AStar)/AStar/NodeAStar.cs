using System.Collections.Generic;

namespace Lab1
{
    public class NodeAstar
    {
        public int[] State { get; set; }
        public NodeAstar Parent { get; set; }
        public List<NodeAstar> Accessors { get; set; } = new List<NodeAstar>();
        public bool IsVisited { get; set; }
        public int Heuristic { get; set; }
        public int Depth { get; set; }

        public NodeAstar(int[] state)
        {
            State = state;
            Heuristic = GetHeuristic() ;
        }

        public NodeAstar(int[] state, NodeAstar parent)
        {
            State = state;
            Heuristic = GetHeuristic();
            Parent = parent;
            Depth = parent.Depth + 1;
        }

        private int GetHeuristic()
        {
            int count = 0;
            for (int i = 0; i < State.Length; i++)
            {
                for (int j = 0; j < State.Length; j++)
                {
                    if (i == j) continue;

                    if (State[i] == State[j] || State[i] + (j - i) == State[j] || State[i] - (j - i) == State[j]) count++;
                }
            }
            return count / 2;
        }

    }

}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Board
    {
        public int BoardSize { get; } = 8;

        public bool[,] Positions { get; }
        public bool[,] InvalidPositions { get; }

        public Board ParentNode { get; set; }

        public Board(int boardSize, bool[,] positions)
        {
            BoardSize = boardSize;
            Positions = positions;
            InvalidPositions = new bool[boardSize, boardSize];
        }

        public bool ValidateWithoutInvalid()
        {
            for(var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if (!IsSafe(i, j)) return false;
                   
                }
            }
            return true;
        }

        public bool Validate()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if ( !IsSafe(i, j) && !InvalidPositions[i,j])
                    {
                        InvalidPositions[i, j] = true;
                    }
                }
            }

            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if (InvalidPositions[i, j]) return false;
                    
                }
            }
            return true;
        }


        bool IsSafe(int row, int column)
        {
            return IsRowSafe(row, column) && IsColSafe(row, column) && IsDiagonalsSafe(row, column);
        }

        bool IsRowSafe(int row, int column)
        {
            for(var colToCheck = 0; colToCheck < BoardSize; colToCheck++)
            {
                if(colToCheck == column) continue;
                if (IsFilled(row, colToCheck)) return false;
                
            }
            return true;
        }

        bool IsColSafe(int row, int column)
        {
            for (var rowToCheck = 0; rowToCheck < BoardSize; rowToCheck++)
            {
                if (rowToCheck == row) continue;
                if ( IsFilled(rowToCheck, column)) return false;
            }

            return true;
        }

        bool IsDiagonalsSafe(int row, int column)
        {
            //// Left

            // Upper
            for (int rowToCheck = row - 1, col = column - 1;
                rowToCheck < BoardSize && rowToCheck >= 0 && col < BoardSize && col >= 0;
                rowToCheck--, col--)
            {
                if (IsFilled(rowToCheck, col)) return false;
            }

            // Bottom
            for (int rowToCheck = row + 1, col = column + 1;
                rowToCheck < BoardSize && rowToCheck >= 0 && col < BoardSize && col >= 0;
                rowToCheck++, col++)
            {
                if (IsFilled(rowToCheck, col)) return false;
            }

            //// Right

            // Upper
            for (int rowToCheck = row - 1, col = column + 1;
                rowToCheck < BoardSize && rowToCheck >= 0 && col < BoardSize && col >= 0;
                rowToCheck--, col++)
            {
                if (IsFilled(rowToCheck, col)) return false;
            }

            // Bottom
            for (int rowToCheck = row + 1, col = column - 1;
                rowToCheck < BoardSize && rowToCheck >= 0 && col < BoardSize && col >= 0;
                rowToCheck++, col--)
            {
                if (IsFilled(rowToCheck, col)) return false;
            }

            return true;
        }
        bool IsFilled(int row, int col)
        {
            return Positions[row, col];
        }


        public void Move(int row, int column, int newRow, int newColumn)
        {
            if ( Positions[row, column] ) return;
            Positions[row, column] = false;
            Positions[newRow, newColumn] = true;
        }

        public void MoveBack(int row, int column, int newRow, int newColumn)
        {
            if (Positions[row, column]) return;
            Positions[row, column] = true;
            Positions[newRow, newColumn] = false;
        }

        public List<Board> Extend()
        {
            var safeCells = new List<Board>();

            for(var i=0;i<BoardSize;i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    InvalidPositions[i, j] = false;
                }
            }
            Validate();


            for (var j = 0; j < BoardSize; j++)
            {
                for (var i = 0; i < BoardSize; i++)
                {

                    for (var row = 0; row < BoardSize; row++)
                    {
                        if (IsFilled(row, j)) continue;

                        if (IsSafe(row, j))
                        {

                        }
                    }
                    

                }
            }

            

            

            return safeCells;
        }

        

    }
}

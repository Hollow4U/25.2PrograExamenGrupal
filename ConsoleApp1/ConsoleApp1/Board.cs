using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Board
    {
        private const int Rows = 10;
        private const int Columns = 10;

        internal List<object>[,] _cells { get; private set; }
        
        private string[] _rowLabels;
        private string[] _columnLabels;

        internal Board()
        {
            _cells = new List<object>[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _cells[i, j] = new List<object>();
                }
            }
            _rowLabels = new string[Rows];
            _columnLabels = new string[Columns];
            for (int i = 0; i < Rows; i++)
            {
                _rowLabels[i] = ((char)('A' + i)).ToString();
            }
            for (int j = 0; j < Columns; j++)
            {
                _columnLabels[j] = (j + 1).ToString();
            }
        }

        internal void Display()
        {
            Console.Write("   ");
            for (int j = 0; j < Columns; j++)
            {
                Console.Write($"{_columnLabels[j],3}");
            }
            Console.WriteLine();
            for (int i = 0; i < Rows; i++)
            {
                Console.Write($"{_rowLabels[i],3}");
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write("[ ]");
                }
                Console.WriteLine();
            }
        }

        internal bool MovePiece(Pieces piece, int oldRow, int oldCol, int newRow, int newCol)
        {
            if (newRow < 0 || newRow >= _cells.GetLength(0) || newCol < 0 || newCol >= _cells.GetLength(0)) return false;

            _cells[oldRow, oldCol].Remove(piece);
            _cells[newRow, newCol].Add(piece);

            return true;
        }
    }
}

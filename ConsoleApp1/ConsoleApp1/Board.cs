using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Board
    {
        private const int Rows = 10;
        private const int Columns = 10;
        private List<object>[,] _cells;
        private string[] _rowLabels;
        private string[] _columnLabels;

        public Board()
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

        public void Display()
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
    }
}

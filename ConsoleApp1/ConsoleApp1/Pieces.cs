using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Pieces
    {
        bool ally;
        int movement = 3;

        public int Row {  get; set; }
        public int Column { get; set; }
       
        internal Pieces(bool ally, int movement, int startRow, int startCol)
        {
            this.ally = ally;
            this.movement = movement;
            Row = startRow;
            Column = startCol;
        }
        internal void Move(Board board,int newRow, int newCol)
        {
            if (movement <= 0) return;

            bool moved = board.MovePiece(this, Row, Column, newRow, newCol);
            
            if(moved)
            {
                Row = newRow;
                Column = newCol;
                movement--;
            }
        }
    }
}

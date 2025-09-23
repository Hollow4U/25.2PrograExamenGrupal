using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
           /* Board board = new Board();
            Pieces p1 = new Pieces(true, 3, 0, 0);
            board._cells[1, 0].Add(p1);
            //board.Display();
            p1.Move(board, 2, 1);
            Console.WriteLine($"Ficha en: fila {p1.Row}, columna {p1.Column}");
            Console.ReadLine();*/
            Game game = new Game();
            game.Turn();
        }
    }
}

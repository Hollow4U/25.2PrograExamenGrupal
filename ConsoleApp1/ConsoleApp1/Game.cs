using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Game
    {




            bool turn = true;
        public void Turn()
        {
            while (turn)
            {
                ChooseAction();
            }
        }

        void ChooseAction()
        {
            Console.WriteLine("Es su turno, escoja una acción");
            Console.WriteLine("1. Moverse");
            Console.WriteLine("2. Ver tablero");
            Console.WriteLine("3. Ver casilla");
            Console.WriteLine("4. Pasar turno");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    SelectionPiece();
                    break;
                case "2":
                    Board board = new Board();
                    board.Display();
                    Console.ReadLine();
                    break;
                case "3":

                    break;
                case "4":
                    turn = false;
                    break;
                default:
                    break;
            }
        }

        void SelectionPiece()
        {
            bool selectionPiece = true;
            while (selectionPiece)
            {
                Console.WriteLine("1. Seleccionar una ficha");
                Console.WriteLine("2. Regresar");
                string moveOption = Console.ReadLine();
                switch (moveOption)
                {
                    case "1":

                        break;
                    case "2":
                        selectionPiece = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

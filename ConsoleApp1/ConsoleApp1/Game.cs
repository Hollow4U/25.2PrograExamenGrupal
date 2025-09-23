using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Game
    {
        List<Pieces> startPieces = new List<Pieces>();
        List<Pieces> piecesWithMovement = new List<Pieces>();


        bool turn = true;
        public void Turn()
        {
            CreateListStartPieces();
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
                    Console.WriteLine("Opción no válida");
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
                        CreateListPiecesWithMovements();
                        for(int i = 0; i < piecesWithMovement.Count; i++)
                        {
                            Console.WriteLine($"{i}. {piecesWithMovement[i].name} está en la posición {piecesWithMovement[i].Row}" +
                                $" {piecesWithMovement[i].Column} y tiene {piecesWithMovement[i].movement} movimientos");
                        }
                        Console.WriteLine("Escoja su ficha");
                        Console.ReadLine();
                        try
                        {

                        }
                        catch
                        {

                        }
                        Board board = new Board();
                        //p1.Move(board, 2, 1);
                        break;
                    case "2":
                        selectionPiece = false;
                        break;
                    default:
                        break;
                }
            }
        }

        void CreateListStartPieces()
        {
            Pieces p1 = new Pieces(true, 3, 0, 0, "ficha1");
            Pieces p2 = new Pieces(true, 3, 0, 0, "ficha2");
            Pieces p3 = new Pieces(true, 3, 0, 1, "ficha3");
            Pieces p4 = new Pieces(true, 3, 1, 0, "ficha4");
            startPieces.Add(p1);
            startPieces.Add(p2);
            startPieces.Add(p3);
            startPieces.Add(p4);

        }
        void CreateListPiecesWithMovements()
        {            
            foreach (Pieces piece in startPieces)
            {
                if(piece.movement > 0)
                {
                    piecesWithMovement.Add(piece);
                }
                else
                {
                    piecesWithMovement.Remove(piece);
                }
            }
        }
        /*private List<Pieces> GetAllEnemyPieces()
        {
            List<Pieces> enemies = new List<Pieces>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    foreach (object obj in _board._cells[i, j])
                    {
                        Pieces piece = obj as Pieces;
                        if (piece != null && !piece.ally)
                        {
                            enemies.Add(piece);
                        }
                    }
                }
            }
            return enemies;
        }*/
    }
}

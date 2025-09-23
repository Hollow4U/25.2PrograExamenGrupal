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
        static Board board = new Board();
        Pieces p1 = new Pieces(true, 3, 0, 0, "ficha1");
        Pieces p2 = new Pieces(true, 3, 0, 4, "ficha2");
        Pieces p3 = new Pieces(true, 3, 0, 9, "ficha3");
        Pieces p4 = new Pieces(false, 3, 9, 0, "ficha4");
        Pieces p5 = new Pieces(false, 3, 9, 4, "ficha5");
        Pieces p6 = new Pieces(false, 3, 9, 9, "ficha6");

        bool turn = true;
        EnemyAI ai = new EnemyAI(board);
            
        public void StartGame()
        {
            CreateListStartPieces();
            bool game = true;
            while (game)
            {
                if (turn)
                {
                    Turn();
                }
                else
                {
                    ai.TakeTurn();
                    int turns = 5;
                    for (int t = 1; t <= turns; t++)
                    {
                        ai.TakeTurn();
                        Console.WriteLine($"\nBoard after enemy turn {t}:");
                        board.Display();
                        Combat();
                    }
                    ResetMovement();
                    turn = true;
                }
            }
        // Combat function: checks for cells with both ally and enemy pieces, performs dice combat, removes losing pieces
        void Combat()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    List<Pieces> allies = new List<Pieces>();
                    List<Pieces> enemies = new List<Pieces>();
                    foreach (object obj in board._cells[i, j])
                    {
                        Pieces piece = obj as Pieces;
                        if (piece != null)
                        {
                            if (piece.ally)
                                allies.Add(piece);
                            else
                                enemies.Add(piece);
                        }
                    }
                    if (allies.Count > 0 && enemies.Count > 0)
                    {
                        bool combatResolved = false;
                        while (!combatResolved)
                        {
                            int allySum = 0;
                            int enemySum = 0;
                            foreach (var a in allies)
                                allySum += rand.Next(1, 7); // d6 per ally
                            foreach (var e in enemies)
                                enemySum += rand.Next(1, 7); // d6 per enemy
                            Console.WriteLine($"Combat at cell ({i},{j}): Ally {allySum} vs Enemy {enemySum}");
                            if (allySum > enemySum)
                            {
                                // Remove all enemy pieces from cell
                                foreach (var e in enemies)
                                    board._cells[i, j].Remove(e);
                                Console.WriteLine($"Allies win at ({i},{j})!");
                                combatResolved = true;
                            }
                            else if (enemySum > allySum)
                            {
                                // Remove all ally pieces from cell
                                foreach (var a in allies)
                                    board._cells[i, j].Remove(a);
                                Console.WriteLine($"Enemies win at ({i},{j})!");
                                combatResolved = true;
                            }
                            else
                            {
                                Console.WriteLine($"Tie at ({i},{j}), rolling again...");
                                // Tie, repeat
                            }
                        }
                    }
                }
            }
        }
            
        }
        public void Turn()
        {
            GetAllAlyPieces().Clear();
            GetAllAlyPieces();
            
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
                        int piece = int.Parse(Console.ReadLine());
                        try
                        {
                            
                            Console.WriteLine("Escoja la fila a mover");
                            int row = int.Parse(Console.ReadLine());
                            Console.WriteLine("Escoja la columna a mover");
                            int column = int.Parse(Console.ReadLine());
                            if (row >= 0 && row <= 10 && row < piecesWithMovement[piece].Row + 2 && row > piecesWithMovement[piece].Row - 2
                                && column >= 0 && column <= 10 && column < piecesWithMovement[piece].Column + 2 && column > piecesWithMovement[piece].Column - 2
                                && (row != piecesWithMovement[piece].Row || column != piecesWithMovement[piece].Column))
                            {
                                piecesWithMovement[piece].Move(board, row, column);
                                Console.WriteLine($"Ficha en: fila {piecesWithMovement[piece].Row}, columna {piecesWithMovement[piece].Column}");
                                board.Display();
                            }                            
                        }
                        catch
                        {
                            Console.WriteLine("Opción no válida");
                        }
                        
                        
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
            startPieces.Add(p1);
            board._cells[p1.Row, p1.Column].Add(p1);
            startPieces.Add(p2);
            board._cells[p2.Row, p2.Column].Add(p2);
            startPieces.Add(p3);
            board._cells[p3.Row, p3.Column].Add(p3);

            board._cells[p4.Row, p4.Column].Add(p4);
            board._cells[p5.Row, p5.Column].Add(p5);
            board._cells[p6.Row, p6.Column].Add(p6);
        }
        void ResetMovement()
        {
            List<Pieces> all = new List<Pieces>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    foreach (object obj in board._cells[i, j])
                    {
                        Pieces piece = obj as Pieces;
                        if (piece != null)
                        {
                            all.Add(piece);
                            for (int p = 0; p < all.Count; p++)
                            {                                
                                all[p].movement = 3;
                            }

                        }
                    }
                }
            }
           
            
        }
        void CreateListPiecesWithMovements()
        {
            piecesWithMovement.Clear();
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
        private List<Pieces> GetAllAlyPieces()
        {
            List<Pieces> allies = new List<Pieces>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    foreach (object obj in board._cells[i, j])
                    {
                        Pieces piece = obj as Pieces;
                        if (piece != null && piece.ally)
                        {
                            allies.Add(piece);
                        }
                    }
                }
            }
            return allies;
        }
    }
}

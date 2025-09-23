using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class EnemyAI
    {
        private Board _board;
        private Random _random;

        internal EnemyAI(Board board)
        {
            _board = board;
            _random = new Random();
        }

        internal void TakeTurn()
        {
            List<Pieces> enemyPieces = GetAllEnemyPieces();
            int piecesToMove = DecideHowManyToMove();
            List<Pieces> prioritized = PrioritizePieces(enemyPieces, enemyPieces.Count); 
            HashSet<Pieces> alreadyMoved = new HashSet<Pieces>();
            int moved = 0;
            foreach (Pieces piece in prioritized)
            {
                if (moved >= piecesToMove)
                    break;
                if (!CanMoveThisTurn(piece))
                    continue;
                (int newRow, int newCol) = GetMoveForPiece(piece);
                piece.Move(_board, newRow, newCol);
                alreadyMoved.Add(piece);
                moved++;
            }
        }

        
        private bool CanMoveThisTurn(Pieces piece)
        {
            
            if (piece.movement <= 0)
                return false;
            foreach (object obj in _board._cells[piece.Row, piece.Column])
            {
                Pieces other = obj as Pieces;
                if (other != null && other.ally)
                    return false;
            }
            return true;
        }

        
        private int DecideHowManyToMove()
        {
            
            int allyCount = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    foreach (object obj in _board._cells[i, j])
                    {
                        Pieces piece = obj as Pieces;
                        if (piece != null && piece.ally)
                        {
                            allyCount++;
                        }
                    }
                }
            }
            
            int toMove = Math.Max(1, allyCount / 2);
            return toMove;
        }

        
        private List<Pieces> PrioritizePieces(List<Pieces> enemyPieces, int count)
        {
            List<Pieces> allies = new List<Pieces>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    foreach (object obj in _board._cells[i, j])
                    {
                        Pieces piece = obj as Pieces;
                        if (piece != null && IsAlly(piece))
                        {
                            allies.Add(piece);
                        }
                    }
                }
            }
            
            enemyPieces.Sort((a, b) =>
            {
                int da = allies.Count == 0 ? 100 : MinDistance(a, allies);
                int db = allies.Count == 0 ? 100 : MinDistance(b, allies);
                return da.CompareTo(db);
            });
            if (enemyPieces.Count > count)
                return enemyPieces.GetRange(0, count);
            return enemyPieces;
        }

        private int MinDistance(Pieces enemy, List<Pieces> allies)
        {
            int min = int.MaxValue;
            foreach (var ally in allies)
            {
                int dist = Math.Abs(enemy.Row - ally.Row) + Math.Abs(enemy.Column - ally.Column);
                if (dist < min) min = dist;
            }
            return min;
        }

        private List<Pieces> GetAllEnemyPieces()
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
        }

        private bool IsAlly(Pieces piece)
        {
            return piece.ally;
        }
        

        private (int, int) GetMoveForPiece(Pieces piece)
        {

            int maxMove = 3;
            int newRow = piece.Row;
            int newCol = piece.Column;
            int tries = 0;
            while (tries < 10)
            {
                int dRow = _random.Next(-maxMove, maxMove + 1);
                int dCol = _random.Next(-maxMove, maxMove + 1);
                if (Math.Abs(dRow) + Math.Abs(dCol) <= maxMove)
                {
                    int candidateRow = piece.Row + dRow;
                    int candidateCol = piece.Column + dCol;
                    if (candidateRow >= 0 && candidateRow < 10 && candidateCol >= 0 && candidateCol < 10)
                    {
                        newRow = candidateRow;
                        newCol = candidateCol;
                        break;
                    }
                }
                tries++;
            }
            return (newRow, newCol);
        }
    }
}

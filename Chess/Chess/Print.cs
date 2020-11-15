using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Figures.Properties;

namespace Chess
{
    class Print
    {      

        public static void PrintBoard(Board chessBoard, List<Position> possibleMoves = null)
        {
            possibleMoves = possibleMoves == null ? new List<Position>() : possibleMoves;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   A B C D E F G H\n");
            Console.ResetColor();

            Figure Figure;
            bool MoveIsPossible = false;

            for (int x = chessBoard.Rows-1; x >= 0 ; x--)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write( x+1 + "  ");
                Console.ResetColor();

                for (int y = 0; y <= chessBoard.Columns - 1; y++)
                {                 
                    Figure = chessBoard.GetFigureFromPosition(x, y);
                    int test = possibleMoves.Where(el => el.Row == x && el.Column == y).Count();
                    MoveIsPossible = possibleMoves.Where(el => el.Row == x && el.Column == y).Count() == 1;

                    PrintPiece(Figure, MoveIsPossible);
                }
                Console.WriteLine();


            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   A B C D E F G H\n");
            Console.ResetColor();

            PrintTakenFigures(chessBoard);
        }

        private static void PrintPiece(Figure figure, bool isPossbile)
        {
            if (figure == null && !isPossbile)
                Console.Write("+ ");
            else if (figure == null && isPossbile)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("+ ");
                Console.ResetColor();
            }
            else
            {
                if (figure.Color == Figure.ColorList.White)
                {
                    if (isPossbile)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(figure + " ");
                        Console.ResetColor();
                    }
                    else
                        Console.Write(figure + " ");
                }
                else
                {
                    if (isPossbile)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(figure + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(figure + " ");
                        Console.ResetColor();
                    }
                }
            }
        }

        public static Position GetPositionFrom_User(Game game)
        {
            int x = -1, y = -1;
            Board chessboard = game.Chessboard;
            Position position;

            while (true)
            {
                Console.Write("From:");
                var PositionFrom = Console.ReadLine();

                x = Char.IsDigit(PositionFrom[0]) ? (int)Char.GetNumericValue(PositionFrom[0]) -1 : -1;
                y = Board.GetLetterMap(PositionFrom[1]);

                position = new Position(x, y);

                if (!chessboard.ExistFigure(position))
                {
                    Console.WriteLine("No figure in this possition. Please enter again.");
                    continue;
                }
                else if (chessboard.GetFigureFromPosition(position).Color != game.CurrentPlayer)
                {
                    Console.WriteLine("Can't move a {0} piece as the {1} player. Please enter again.", chessboard.GetFigureFromPosition(position).Color, game.CurrentPlayer);
                    continue;
                }
                else
                    break;
            }

            return position;
        }

        public static Position GetPositionTo_User(Game game, List<Position> possibleMoves)
        {
            int x = -1, y = -1;
            Board chessboard = game.Chessboard;
            Position position;

            while(true)
            {
                Console.WriteLine("To:");
                var PositionFrom = Console.ReadLine();
                if (PositionFrom.ToUpper() == "X")
                    return null;

                x = Char.IsDigit(PositionFrom[0]) ? (int)Char.GetNumericValue(PositionFrom[0]) -1 : -1;
                y = Board.GetLetterMap(PositionFrom[1]);

                position = new Position(x, y);

                if (!chessboard.ValidatePosition(position) || !possibleMoves.Exists(z => z.Row == x && z.Column == y))
                {
                    Console.WriteLine("Not a valid position. Please enter again.");
                    continue;
                }
                else if (chessboard.GetFigureFromPosition(position)?.Color == game.CurrentPlayer)
                {
                    Console.WriteLine("Can't move a {0} piece over a {1} peice. Please enter again.", chessboard.GetFigureFromPosition(position).Color, game.CurrentPlayer);
                    continue;
                }
                else
                    break;
            }

            return position;
        }

        private static void PrintTakenFigures(Board chessboard) // ¬¬¬¬ rework
        {
            Console.Write("White player taken figures: ");
            foreach (Figure f in chessboard.TakenFigures)
            {
                if(f.Color == Figure.ColorList.White)
                    Console.Write(" {0},",(f));
            }

            Console.WriteLine();

            Console.Write("Black player taken figures: ");
            foreach (Figure f in chessboard.TakenFigures)
            {
                if (f.Color == Figure.ColorList.Black)
                    Console.Write(" {0},", (f));
            }

            Console.WriteLine("\n\n");
        }

    } 
}

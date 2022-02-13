using System;
using System.Collections.Generic;
using System.Linq;
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

            Figure figure;
            bool moveIsPossible;

            for (int x = chessBoard.Rows-1; x >= 0 ; x--)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write( x+1 + "  ");
                Console.ResetColor();

                for (int y = 0; y <= chessBoard.Columns - 1; y++)
                {                 
                    figure = chessBoard.GetFigureFromPosition(x, y);
                    moveIsPossible = possibleMoves.Where(el => el.Row == x && el.Column == y).Count() == 1;

                    PrintPiece(figure, moveIsPossible);
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
            else if (figure == null)
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
            int x, y;
            Board chessboard = game.Chessboard;
            Position position;

            while (true)
            {
                Console.Write("From:");
                string positionFrom = Console.ReadLine() ?? "";

                if (positionFrom.Count() !=2)
                {
                    Console.WriteLine("Not a valid position. Please enter again.");
                    continue;
                }

                x = Char.IsDigit(positionFrom[0]) ? (int)Char.GetNumericValue(positionFrom[0]) -1 : -1;
                y = Board.GetLetterMap(positionFrom[1]);

                position = new Position(x, y);

                if (!chessboard.ExistFigure(position))
                {
                    Console.WriteLine("No figure in this position. Please enter again.");
                }
                else if (chessboard.GetFigureFromPosition(position).Color != game.CurrentPlayer.Color)
                {
                    Console.WriteLine("Can't move a {0} piece as the {1} player. Please enter again.", chessboard.GetFigureFromPosition(position).Color, game.CurrentPlayer);
                }
                else
                    break;
            }

            return position;
        }

        public static Position GetPositionTo_User(Game game, List<Position> possibleMoves)
        {
            int x, y;
            Board chessboard = game.Chessboard;
            Position position;

            while(true)
            {
                Console.Write("To:");
                string positionFrom = Console.ReadLine() ?? "";
                if (positionFrom.ToUpper() == "X")
                    return null;

                x = Char.IsDigit(positionFrom[0]) ? (int)Char.GetNumericValue(positionFrom[0]) -1 : -1;
                y = Board.GetLetterMap(positionFrom[1]);

                position = new Position(x, y);

                if (!chessboard.ValidatePosition(position) || !possibleMoves.Exists(z => z.Row == x && z.Column == y))
                {
                    Console.WriteLine("Not a valid position. Please enter again.");
                }
                else if (chessboard.GetFigureFromPosition(position)?.Color == game.CurrentPlayer.Color)
                {
                    Console.WriteLine("Can't move a {0} piece over a {1} peice. Please enter again.", chessboard.GetFigureFromPosition(position).Color, game.CurrentPlayer);
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

        public static Figure GetPawnPromotion(params object[] constructorArgs)
        {
            while (true)
            {
                Console.Write("Transform Pawn to:");
                string figureType = Console.ReadLine() ?? "";

                if (figureType.ToUpper() == "K")
                {
                    Console.WriteLine("Can not transform into King.");
                    continue;   
                }

                // Use reflection to construct a base class Figure based on derived class Attributes
                IEnumerable <Figure> exporters = typeof(Figure)
                    .Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Figure)) && !t.IsAbstract && t.GetCustomAttributesData()[0].ConstructorArguments[1].Value.ToString().Contains(figureType.ToUpper()))
                    .Select(t => (Figure)Activator.CreateInstance(t, constructorArgs));

                if (exporters.FirstOrDefault() != null)
                    return exporters.FirstOrDefault();
            }
        }
    } 
}

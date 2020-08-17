using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess
{
    class Print
    {      

        public static void PrintBoard(Board chessBoard)
        {
            for (int x = 0; x < chessBoard.Rows; x++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(chessBoard.Rows - x + "  ");
                Console.ResetColor();

                for (int y = 0; y < chessBoard.Columns; y++)
                {
                    PrintPiece(chessBoard.GetFigureFromPosition(x, y));
                }
                Console.WriteLine();


            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   A B C D E F G H");
            Console.ResetColor();
        }

        public static void PrintPiece(Figure figure)
        {
            if (figure == null)
                Console.Write("+ ");
            else
            {
                if (figure.Color == Figure.ColorList.White)
                {
                    Console.Write(figure + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(figure + " ");
                    Console.ResetColor();
                }
            }
        }

        public static Position GetPositionFrom_User(Board chessboard)
        {
            int x = -1, y = -1;
            Console.WriteLine("Example position: 1a");

            do
            {
                Console.Write("From:");
                var PositionFrom = Console.ReadLine();

                int test = (int)PositionFrom[0];

                x = Char.IsDigit(PositionFrom[0]) ? (int)Char.GetNumericValue(PositionFrom[0])  : -1; 
                y = Board.GetLetterMap(PositionFrom[1]);
            }
            while (!chessboard.ExistFigure(new Position(x, y)));

            return new Position(x, y);
        }

        public static Position GetPositionTo_User(Board chessboard)
        {
            int x = -1, y = -1;
            Console.WriteLine("Example position: 1a");

            do
            {
                Console.WriteLine("To:");
                var PositionFrom = Console.ReadLine();

                x = Char.IsDigit(PositionFrom[0]) ? (int)Char.GetNumericValue(PositionFrom[0]) : -1;
                y = Board.GetLetterMap(PositionFrom[1]);
            }
            while (!chessboard.ValidatePosition(new Position(x, y)));

            return new Position(x, y);
        }
    } 
}

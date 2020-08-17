using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess
{
    class Print
    {

        public static void PrintBoard (Board ChessBoard)
        {
            for(int x = 0; x < ChessBoard.Rows; x++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ChessBoard.Rows - x + "  ");
                Console.ResetColor();

                for (int y = 0; y < ChessBoard.Columns; y++)
                {
                    PrintPiece(ChessBoard.GetFigureFromPosition(x,y));                   
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
    }
}

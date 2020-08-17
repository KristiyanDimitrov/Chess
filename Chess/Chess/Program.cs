using System;
using Chess.Figures;
using Chess.Figures.Properties;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Game CurrentGame = new Game();
            Print.PrintBoard(CurrentGame.Chessboard);




        }
    }
}

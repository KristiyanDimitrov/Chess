using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Figures.Properties;

namespace Chess
{
    class Program
    {
        static void Main()
        {
            Game currentGame = new();

            while (!currentGame.GameEnded.Item1)
            {
                Console.Clear();
                Print.PrintBoard(currentGame.Chessboard);

                //Validates position is valid and it contains a figure that corresponds to the current player color
                Position from = Print.GetPositionFrom_User(currentGame);

                //Get possible moves
                List<Position> possibleMoves = currentGame.GetPossibleMoves(from);
                if (!possibleMoves.Any())
                {
                    Print.DisplayMessageBoxMsg("Figure has no possible moves!");
                    continue;
                }
                             
                Console.Clear();
                Print.PrintBoard(currentGame.Chessboard, possibleMoves);

                //Validates it is a valid position
                Position to = Print.GetPositionTo_User(currentGame, possibleMoves);

                //Play the selected move
                if (to == null)
                    continue;

                currentGame.PlayMove(from, to);
            }

            Console.Clear();
            Print.PrintBoard(currentGame.Chessboard);

            Print.DisplayMessageBoxMsg(currentGame.GameEnded.Item2);
            Console.ReadKey();

        }
    }
}

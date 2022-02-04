using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Figures;
using Chess.Figures.Properties;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Game currentGame = new Game();
            List<Position> possibleMoves = new List<Position>();

            while (!currentGame.GameEnded)
            {
                Console.Clear();
                Print.PrintBoard(currentGame.Chessboard);

                //Validates position is valid and it contains a figure that coresponds to the current player color
                Position @from = Print.GetPositionFrom_User(currentGame);

                //Get possible moves
                possibleMoves = currentGame.GetPossibleMoves(@from);
                if (!possibleMoves.Any())
                    continue;              

                Console.Clear();
                Print.PrintBoard(currentGame.Chessboard, possibleMoves);

                //Validates it is a valid position
                Position to = Print.GetPositionTo_User(currentGame, possibleMoves);

                //Play the selected move
                if (to == null)
                    continue;

                currentGame.PlayMove(@from, to);
            }
        }
    }
}

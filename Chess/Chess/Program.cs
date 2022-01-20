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
            Game CurrentGame = new Game();
            List<Position> PossibleMoves = new List<Position>();

            while (!CurrentGame.GameEnded)
            {
                Console.Clear();
                Print.PrintBoard(CurrentGame.Chessboard);

                //Validates position is valid and it contains a figure that coresponds to the current player color
                Position From = Print.GetPositionFrom_User(CurrentGame);

                //Get possible moves
                PossibleMoves = CurrentGame.getPossibleMoves(From);
                if (!PossibleMoves.Any())
                    continue;              

                Console.Clear();
                Print.PrintBoard(CurrentGame.Chessboard, PossibleMoves);

                //Validates it is a valid position
                Position To = Print.GetPositionTo_User(CurrentGame, PossibleMoves);

                //Play the selected move
                if (To == null)
                    continue;

                CurrentGame.PlayMove(From, To);



            }



        }
    }
}

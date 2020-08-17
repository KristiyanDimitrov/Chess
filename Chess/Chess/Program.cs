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
           

            bool GameEnded = false;
            while(!GameEnded)
            {
                Console.Clear();
                Print.PrintBoard(CurrentGame.Chessboard);

                //Validates position is valid and it contains a figure
                Position From = Print.GetPositionFrom_User(CurrentGame.Chessboard); // ¬¬¬¬ Need to add validaton for current player

                //Validates it is a valid position
                Position To = Print.GetPositionTo_User(CurrentGame.Chessboard); // ¬¬¬¬ Need to validate position does not contain current player figure



            }



        }
    }
}

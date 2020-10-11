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
           
            while(!CurrentGame.GameEnded)
            {
                Console.Clear();
                Print.PrintBoard(CurrentGame.Chessboard);

                //Validates position is valid and it contains a figure that coresponds to the current player color
                Position From = Print.GetPositionFrom_User(CurrentGame); 
                
                //Validates it is a valid position and prevents moving to a position that contains the same color figure.
                Position To = Print.GetPositionTo_User(CurrentGame);

                //Play the selected move
                CurrentGame.PlayMove(From, To);



            }



        }
    }
}

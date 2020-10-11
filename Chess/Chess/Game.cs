using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures;
using Chess.Figures.Properties;

namespace Chess
{
    class Game
    {
        public Board Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Figure.ColorList CurrentPlayer { get; private set; }
        public bool GameEnded { get; private set; }      


        public Game ()
        {
            Chessboard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Figure.ColorList.White;
            GameEnded = false;
            SetBoard();
        }

        public void SetBoard()
        {
            
            Chessboard.MoveFigure(new Rook  (7,0, Figure.ColorList.White));
            Chessboard.MoveFigure(new Knight(7,1, Figure.ColorList.White));
            Chessboard.MoveFigure(new Bishop(7,2, Figure.ColorList.White));
            Chessboard.MoveFigure(new Queen (7,3, Figure.ColorList.White));
            Chessboard.MoveFigure(new King  (7,4, Figure.ColorList.White));
            Chessboard.MoveFigure(new Bishop(7,5, Figure.ColorList.White));
            Chessboard.MoveFigure(new Knight(7,6, Figure.ColorList.White));
            Chessboard.MoveFigure(new Rook  (7,7, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,0, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,1, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,2, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,3, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,4, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,5, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,6, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn (6,7, Figure.ColorList.White));

            Chessboard.MoveFigure(new Rook  (0, 0, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Knight(0, 1, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Bishop(0, 2, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Queen (0, 3, Figure.ColorList.Black));
            Chessboard.MoveFigure(new King  (0, 4, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Bishop(0, 5, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Knight(0, 6, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Rook  (0, 7, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 0, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 1, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 2, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 3, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 4, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 5, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 6, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 7, Figure.ColorList.Black));
        }

        public void PlayMove(Position from, Position to)
        {
            Figure SelectedFigure = Chessboard.ClearPosition(from);
            Chessboard.MoveFigure(SelectedFigure,to);
            CurrentPlayer = CurrentPlayer == Figure.ColorList.White ? Figure.ColorList.Black : Figure.ColorList.White;
        }
    }
}

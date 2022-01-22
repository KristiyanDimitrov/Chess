using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures;
using Chess.Figures.Properties;
using System.Linq;

namespace Chess
{
    class Game
    {
        public Board Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player[] Players = new Player[2];
        public bool GameEnded { get; private set; }


        public Game()
        {
            Chessboard = new Board(8, 8);
            Turn = 1;
            GameEnded = false;
            SetBoard();
            CurrentPlayer = Players.Where(x => x.Color == Figure.ColorList.White).FirstOrDefault();
        }

        public void SetBoard()
        {

            Chessboard.MoveFigure(new Rook(7, 0, Figure.ColorList.White));
            Chessboard.MoveFigure(new Knight(7, 1, Figure.ColorList.White));
            Chessboard.MoveFigure(new Bishop(7, 2, Figure.ColorList.White));
            Chessboard.MoveFigure(new Queen(7, 3, Figure.ColorList.White));
            Chessboard.MoveFigure(new King(7, 4, Figure.ColorList.White));
            Chessboard.MoveFigure(new Bishop(7, 5, Figure.ColorList.White));
            Chessboard.MoveFigure(new Knight(7, 6, Figure.ColorList.White));
            Chessboard.MoveFigure(new Rook(7, 7, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 0, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 1, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 2, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 3, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 4, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 5, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 6, Figure.ColorList.White));
            Chessboard.MoveFigure(new Pawn(6, 7, Figure.ColorList.White));

            Chessboard.MoveFigure(new Rook(0, 0, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Knight(0, 1, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Bishop(0, 2, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Queen(0, 3, Figure.ColorList.Black));
            Chessboard.MoveFigure(new King(0, 4, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Bishop(0, 5, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Knight(0, 6, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Rook(0, 7, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 0, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 1, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 2, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 3, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 4, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 5, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 6, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Pawn(1, 7, Figure.ColorList.Black));

            Players[0] = new Player(Figure.ColorList.White);
            Players[1] = new Player(Figure.ColorList.Black);
        }

        public void PlayMove(Position from, Position to)
        {
            Figure SelectedFigure = Chessboard.ClearPosition(from);
            SelectedFigure.IsFirstMove = false;

            Chessboard.MoveFigure(SelectedFigure, to);
            CurrentPlayer = CurrentPlayer.Color == Figure.ColorList.White ? Players.Where(x => x.Color == Figure.ColorList.Black).FirstOrDefault() : Players.Where(x => x.Color == Figure.ColorList.White).FirstOrDefault();

            KingInCheckUpdate(CurrentPlayer);
        }

        public List<Position> getPossibleMoves(Position from)
        {
            Figure SelectedFigure = Chessboard.GetFigureFromPosition(from);
            List<Position> PossibleMoves = SelectedFigure.PossibleMoves(Chessboard);

            //Remove moves that will put frendly King in Check
            KingCheck(SelectedFigure.Color, SelectedFigure, PossibleMoves);

            return PossibleMoves;
        }

        /*
            This method uses "ShadowMoves" to check the consequences of a move. A ShadowMove consists of making the move in a controlled and reversable way,
        in order to use the existing methods of checking possible moves. 
            This helps to check if the state of the board after a move is valid. For example if it puts a friendly king in danger.           
        */
        private void KingCheck(Figure.ColorList color, Figure selectedFigure, List<Position> moves)
        {
            Position TheKingPos = Chessboard.GetKingFigure(color).GettPosition();
            List<Position> MovesToRemove = new List<Position>();

            foreach (Position pos in moves)
            {
                Chessboard.MoveShadowFigure(selectedFigure, pos);

                for (int x = Chessboard.Rows - 1; x >= 0; x--)
                {
                    for (int y = 0; y <= Chessboard.Columns - 1; y++)
                    {
                        Figure CurFigure = Chessboard.GetFigureFromPosition(x, y);
                        if (CurFigure == null || CurFigure?.Color == color)
                            continue;

                        // If the potental move puts the frendly King in danger remove it from the list of possible moves.
                        List<Position> CurFigurePossibleMoves = CurFigure.PossibleMoves(Chessboard);
                        if (CurFigurePossibleMoves.Exists(move => move.Column == TheKingPos.Column && move.Row == TheKingPos.Row))
                            MovesToRemove.Add(pos);
                    }
                }

                Chessboard.ResetShadowMove(selectedFigure, pos);
            }

            foreach (Position move in MovesToRemove)
                moves.RemoveAll(x => x.Column == move.Column && x.Row == move.Row);
        }

        // Check if the played move puts the oponents King in Check
        private void KingInCheckUpdate(Player CurentPlayer)
        {
            King TheKing = (King)Chessboard.GetKingFigure(CurrentPlayer.Color == Figure.ColorList.Black ? Figure.ColorList.White : Figure.ColorList.Black);

            for (int x = Chessboard.Rows - 1; x >= 0; x--)
            {
                for (int y = 0; y <= Chessboard.Columns - 1; y++)
                {
                    Figure CurFigure = Chessboard.GetFigureFromPosition(x, y);
                    if (CurFigure == null || CurFigure?.Color == TheKing.Color)
                        continue;

                    List<Position> CurFigurePossibleMoves = CurFigure.PossibleMoves(Chessboard);
                    if (CurFigurePossibleMoves.Exists(move => move.Column == TheKing.FigurePosition.Column && move.Row == TheKing.FigurePosition.Row))
                    {
                        TheKing.KingInCheck = true; // Don't like the check being in two places, will be like this for now untill I figure out how to implement it best ¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬
                        CurentPlayer.InCheck = true;
                        return;
                    }
                }
            }
            TheKing.KingInCheck = false;
            CurentPlayer.InCheck = false;
        }
    }
}

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
            //Chessboard.MoveFigure(new Knight(7, 1, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Bishop(7, 2, Figure.ColorList.White));
            Chessboard.MoveFigure(new Queen(7, 3, Figure.ColorList.White));
            Chessboard.MoveFigure(new King(7, 4, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Bishop(7, 5, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Knight(7, 6, Figure.ColorList.White));
            Chessboard.MoveFigure(new Rook(7, 7, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 0, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 1, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 2, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 3, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 4, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 5, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 6, Figure.ColorList.White));
            //Chessboard.MoveFigure(new Pawn(6, 7, Figure.ColorList.White));

            Chessboard.MoveFigure(new Rook(0, 0, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Knight(0, 1, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Bishop(0, 2, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Queen(0, 3, Figure.ColorList.Black));
            Chessboard.MoveFigure(new King(0, 4, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Bishop(0, 5, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Knight(0, 6, Figure.ColorList.Black));
            Chessboard.MoveFigure(new Rook(0, 7, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 0, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 1, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 2, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 3, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 4, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 5, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 6, Figure.ColorList.Black));
            //Chessboard.MoveFigure(new Pawn(1, 7, Figure.ColorList.Black));

            Players[0] = new Player(Figure.ColorList.White);
            Players[1] = new Player(Figure.ColorList.Black);
        }

        public void PlayMove(Position from, Position to)
        {
            Figure SelectedFigure = Chessboard.ClearPosition(from);
            

            //Castling move - Handling the Rook
            if (SelectedFigure is King)
                if (Math.Abs(SelectedFigure.FigurePosition.Column - to.Column) == 2)
                {
                    Rook theRook = ((King) SelectedFigure).GetCastleMoveRook(to);
                    int kingToRookDistance = SelectedFigure.FigurePosition.Column - theRook.GetPosition().Column;
                    int offset;

                    if (kingToRookDistance > 0)
                        offset = -1;
                    else
                        offset = 1;

                    Chessboard.MoveFigure(theRook, new Position(SelectedFigure.FigurePosition.Row, SelectedFigure.FigurePosition.Column + offset));
                }

            Chessboard.MoveFigure(SelectedFigure, to);


            CurrentPlayer = CurrentPlayer.Color == Figure.ColorList.White ? Players.Where(x => x.Color == Figure.ColorList.Black).FirstOrDefault() : Players.Where(x => x.Color == Figure.ColorList.White).FirstOrDefault();
            SelectedFigure.IsFirstMove = false;

            KingInCheckUpdate(CurrentPlayer);
        }

        public List<Position> GetPossibleMoves(Position from)
        {
            Figure SelectedFigure = Chessboard.GetFigureFromPosition(from);
            List<Position> PossibleMoves = SelectedFigure.PossibleMoves(Chessboard);
            
            // Position CastleMove = PossibleMoves.FirstOrDefault(x => Math.Abs(x.Column - SelectedFigure.GettPosition().Column) == 2);

            KingCheck(SelectedFigure.Color, SelectedFigure, PossibleMoves);//Remove moves that will put friendly King in Check

            return PossibleMoves;
        }

        /*
            This method uses "ShadowMoves" to check the consequences of a move. A ShadowMove consists of making the move in a controlled and reversible way,
        in order to use the existing methods of checking possible moves. 
            This helps to check if the state of the board after a move is valid. For example if it puts a friendly king in danger.
        
            This method is also checks if friendly king is in danger during the Castle move.
        */
        private void KingCheck(Figure.ColorList color, Figure selectedFigure, List<Position> moves)
        {
            Position TheKingPos = Chessboard.GetKingFigure(color).GetPosition(); // When the figure to move is the King this breaks the logic ¬¬¬¬¬¬¬
            List<Position> MovesToRemove = new List<Position>();

            // Used for validation of the field the King jumps over.
            Position CastleMove = selectedFigure is not King ? null : moves.FirstOrDefault(x => Math.Abs(x.Column - selectedFigure.GetPosition().Column) == 2);          


            foreach (Position pos in moves)
            {
                TheKingPos = selectedFigure is King ? pos : TheKingPos;
                Chessboard.MoveShadowFigure(selectedFigure, pos);

                for (int x = Chessboard.Rows - 1; x >= 0; x--)
                {
                    for (int y = 0; y <= Chessboard.Columns - 1; y++)
                    {
                        Figure CurFigure = Chessboard.GetFigureFromPosition(x, y);
                        if (CurFigure == null || CurFigure?.Color == color)
                            continue;

                        // If the potential move puts the friendly King in danger remove it from the list of possible moves
                        List<Position> CurFigurePossibleMoves = CurFigure.PossibleMoves(Chessboard);
                        if (CurFigurePossibleMoves.Exists(move => move.Column == TheKingPos.Column && move.Row == TheKingPos.Row))
                            MovesToRemove.Add(pos);
                    }
                }
                // Castle Move Jump: Check if the previous field was not under attack
                if (CastleMove != null && MovesToRemove.Exists(x => x.Column == (CastleMove.Column-1)))
                    MovesToRemove.Add(CastleMove);

                Chessboard.ResetShadowMove(selectedFigure, pos);
            }

            foreach (Position move in MovesToRemove)
                moves.RemoveAll(x => x.Column == move.Column && x.Row == move.Row);
        }

        // Check if the played move puts the opponents King in Check
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
                        TheKing.KingInCheck = true; // Don't like the check being in two places, will be like this for now until I figure out how to implement it best ¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬
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

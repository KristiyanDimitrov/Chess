using System;
using System.Collections.Generic;
using ChessLogic.Figures;
using ChessLogic.Figures.Properties;
using System.Linq;

namespace ChessLogic
{
    public class Game
    {
        public Board Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player[] Players = new Player[2];
        public (bool, string) GameEnded { get; private set; }

        public Game()
        {
            Chessboard = new Board(8, 8);
            Turn = 1;
            GameEnded = (false,"");
            SetBoard();
            CurrentPlayer = Players.FirstOrDefault(x => x.Color == Figure.ColorList.White);
        }

        private void SetBoard()
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
            Figure selectedFigure = Chessboard.ClearPosition(from);

            #region SpecialMoves
            //Castling move - Handling the Rook
            if (selectedFigure is King)
                if (Math.Abs(selectedFigure.figurePosition.Column - to.Column) == 2)
                {
                    Tuple<Rook, Position> rookMove = ((King) selectedFigure).GetCastleMoveRook(to);
                    Chessboard.MoveFigure(rookMove.Item1, rookMove.Item2);
                }

            //// Pawn Special Move - Promotion
            //if (selectedFigure is Pawn && (to.Row == 0 || to.Row == Chessboard.Rows - 1))
            //{
            //    var transformedFigure = Print.GetPawnPromotion(to.Row, to.Column, selectedFigure.color);
            //    selectedFigure = transformedFigure;
            //}

            // Pawn Special Move - En passant
            if (Chessboard.EnPassantEnabledPawn != null && selectedFigure is Pawn && Chessboard.IsEnPassantPossition(to.Row, to.Column))
                Chessboard.ClearFigure(Chessboard.EnPassantEnabledPawn, true);
            #endregion

            Chessboard.MoveFigure(selectedFigure, to);

            #region EnPassantSetUp
            if (selectedFigure is Pawn && ((Pawn)selectedFigure).EnPassant)
            {
                Chessboard.EnPassantEnabledPawn = selectedFigure;
                int enPassantDirection = selectedFigure.color == Figure.ColorList.White ? 1 : -1;
                Chessboard.EnPassantPosition = new Position(selectedFigure.figurePosition.Row + enPassantDirection, selectedFigure.figurePosition.Column);
            }
            else
            {
                Chessboard.EnPassantEnabledPawn = null;
                Chessboard.EnPassantPosition = null;
            }
            #endregion

            CurrentPlayer = CurrentPlayer.Color == Figure.ColorList.White ? Players.FirstOrDefault(x => x.Color == Figure.ColorList.Black) : Players.FirstOrDefault(x => x.Color == Figure.ColorList.White);

            KingInCheckUpdate(CurrentPlayer);
            GameEnded = EndgameUpdate(CurrentPlayer);
        }

        public List<Position> GetPossibleMoves(Position from)
        {
            Figure selectedFigure = Chessboard.GetFigureFromPosition(from);
            List<Position> possibleMoves = selectedFigure.PossibleMoves(Chessboard);

            KingCheck(selectedFigure.color, selectedFigure, possibleMoves);//Remove moves that will put friendly King in Check

            return possibleMoves;
        }

        public void HandlePromotion(Position position, string promotionType)
        {
            switch (promotionType.ToString())
            {
                case "Queen":
                    Chessboard.MoveFigure(new Queen(position.Row, position.Column, CurrentPlayer.OpositeColor));
                    break;
                case "Rook":
                    Chessboard.MoveFigure(new Rook(position.Row, position.Column, CurrentPlayer.OpositeColor));
                    break;
                case "Bishop":
                    Chessboard.MoveFigure(new Bishop(position.Row, position.Column, CurrentPlayer.OpositeColor));
                    break;
                case "Knight":
                    Chessboard.MoveFigure(new Knight(position.Row, position.Column, CurrentPlayer.OpositeColor));
                    break;
            }              
        }

        /*
            This method uses "ShadowMoves" to check the consequences of a move. A ShadowMove consists of making the move in a controlled and reversible way,
        in order to use the existing methods of checking possible moves. 
            This helps to check if the state of the board after a move is valid. For example if it puts a friendly king in danger.
            
            <<ShadowMove>> !!FIGURE POSSITION DOES NOT CHANGE!!
        
            This method is also checks if friendly king is in danger during the Castle move.
        */
        private void KingCheck(Figure.ColorList color, Figure selectedFigure, List<Position> moves)
        {
            Position theKingPos = Chessboard.GetKingFigure(color).GetPosition();
            List<Position> movesToRemove = new();
     

            foreach (Position pos in moves)
            {
                theKingPos = selectedFigure is King ? pos : theKingPos;
                Chessboard.MoveShadowFigure(selectedFigure, pos);

                for (int x = Chessboard.Rows - 1; x >= 0; x--)
                {
                    for (int y = 0; y <= Chessboard.Columns - 1; y++)
                    {
                        Figure curFigure = Chessboard.GetFigureFromPosition(x, y);
                        if (curFigure == null || curFigure.color == color)
                            continue;

                        // If the potential move puts the friendly King in danger remove it from the list of possible moves
                        List<Position> curFigurePossibleMoves = curFigure.PossibleMoves(Chessboard);
                        if (curFigurePossibleMoves.Exists(move => move.Column == theKingPos.Column && move.Row == theKingPos.Row))
                            movesToRemove.Add(pos);
                    }
                }

                // Castle Move Jump: Check if the previous field was not under attack
                if (selectedFigure is King && Math.Abs(Chessboard.GetKingFigure(color).GetPosition().Column - pos.Column) == 2)
                {
                    Tuple<Rook, Position> castleRookMove = ((King)Chessboard.GetKingFigure(color)).GetCastleMoveRook(pos);

                    if (!moves.Exists(x => x.Row == (castleRookMove.Item2.Row) && x.Column == (castleRookMove.Item2.Column)) // Check if the field that is jumped is a valid move
                            || movesToRemove.Exists(s => s.Row == (castleRookMove.Item2.Row) && s.Column == (castleRookMove.Item2.Column)))  // or if it is going to be removed.
                        movesToRemove.Add(pos);
                }

                Chessboard.ResetShadowMove(selectedFigure, pos);
            }

            foreach (Position move in movesToRemove)
                moves.RemoveAll(x => x.Column == move.Column && x.Row == move.Row);
        }

        // Check if the played move puts the opponents King in Check
        private void KingInCheckUpdate(Player currentPlayer, Position KingShadowPosition = null)
        {
            King theKing = (King)Chessboard.GetKingFigure(currentPlayer.Color == Figure.ColorList.Black ? Figure.ColorList.White : Figure.ColorList.Black);
            Position theKingPosition = KingShadowPosition ?? theKing.figurePosition;

            for (int x = Chessboard.Rows - 1; x >= 0; x--)
            {
                for (int y = 0; y <= Chessboard.Columns - 1; y++)
                {
                    Figure curFigure = Chessboard.GetFigureFromPosition(x, y);
                    if (curFigure == null || curFigure.color == theKing.color)
                        continue;

                    List<Position> curFigurePossibleMoves = curFigure.PossibleMoves(Chessboard);
                    if (curFigurePossibleMoves.Exists(move => move.Column == theKingPosition.Column && move.Row == theKingPosition.Row))
                    {
                        theKing.KingInCheck = true;
                        currentPlayer.InCheck = true;
                        return;
                    }
                }
            }

            theKing.KingInCheck = false;
            currentPlayer.InCheck = false;
        }

        private (bool, string) EndgameUpdate(Player currentPlayer)
        {
            if (currentPlayer.InCheck)
                return (false, "");

            King theKing = (King)Chessboard.GetKingFigure(CurrentPlayer.Color);
            var kingPossibleMoves = theKing.PossibleMoves(Chessboard);
            KingCheck(theKing.color, theKing, kingPossibleMoves);

            Player oposingPlayer = Players.FirstOrDefault(x => x.Color != theKing.color); // KingCheck() is for 

            // Check for stalemate
            if (Chessboard.PlayerHasPossibleMoves(currentPlayer))
                return (true, "Current game is in Stalemate! /n Game is a draw.");

            // Check for King moves that can save it
            foreach (Position pos in kingPossibleMoves)
            {
                Chessboard.MoveShadowFigure(theKing, pos);

                KingInCheckUpdate(oposingPlayer);
                if (!theKing.KingInCheck)
                {
                    Chessboard.ResetShadowMove(theKing, pos);
                    return (false, "");
                }

                Chessboard.ResetShadowMove(theKing, pos);
            }

            // Check for other moves that can save the king
            for (int x = Chessboard.Rows - 1; x >= 0; x--)
            {
                for (int y = 0; y <= Chessboard.Columns - 1; y++)
                {
                    Figure curFigure = Chessboard.GetFigureFromPosition(x, y);
                    if (curFigure == null || curFigure.color != theKing.color)
                        continue;

                    List<Position> curFigurePossibleMoves = curFigure.PossibleMoves(Chessboard);
                    foreach (Position pos in curFigurePossibleMoves)
                    {
                        Chessboard.MoveShadowFigure(curFigure, pos);

                        KingInCheckUpdate(oposingPlayer, curFigure is King ? pos : null);
                        if (!theKing.KingInCheck)
                        {
                            Chessboard.ResetShadowMove(curFigure, pos);
                            return (false, "");
                        }                            

                        Chessboard.ResetShadowMove(curFigure, pos);
                    }
                }
            }
            return (true, $"{CurrentPlayer.Color} player is in Checkmate!");
        }
    }
}

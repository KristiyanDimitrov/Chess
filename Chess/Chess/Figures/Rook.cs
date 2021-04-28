using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class Rook : Figure
    {
        public Rook(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> PossiblePositions = new List<Position>();
            Position CurPos = base.FigurePosition;

            int X = CurPos.Row;
            int Y = CurPos.Column;

            bool X_Pos, X_Neg, Y_Pos, Y_Neg; // Indicator if the movement to a given direction is blocked
            X_Pos = X_Neg = Y_Pos = Y_Neg = true;

            for (int i = 1; true; i++)
            {
                X_Pos = X_Pos ? board.BasicMoveValidate(PossiblePositions, this, X + i, Y) : false;
                Y_Neg = Y_Neg ? board.BasicMoveValidate(PossiblePositions, this, X, Y - i) : false;
                X_Neg = X_Neg ? board.BasicMoveValidate(PossiblePositions, this, X - i, Y) : false;
                Y_Pos = Y_Pos ? board.BasicMoveValidate(PossiblePositions, this, X, Y + i) : false;

                if (!X_Pos && !X_Neg && !Y_Pos && !Y_Neg)
                    break;
            }

            return PossiblePositions;
        }

        public override string ToString() => "R";
    }
}

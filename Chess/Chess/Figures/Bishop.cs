using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class Bishop : Figure
    {
        public Bishop(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> PossiblePositions = new List<Position>();
            Position CurPos = base.FigurePosition;
            bool CheckPosition;
            int X = CurPos.Row;
            int Y = CurPos.Column;

            bool XY_Pos, XY_Neg, Y_Pos, Y_Neg; // Indicator if the diagonal is blocked
            XY_Pos = XY_Neg = Y_Pos = Y_Neg = true;

            for (int i = 1; true; i++)
            {
                if(XY_Pos)
                {
                    CheckPosition = board.BasicMoveValidate(PossiblePositions, this, X + i, Y + i);
                    XY_Pos = CheckPosition;
                }

                if (Y_Neg)
                {
                    CheckPosition = board.BasicMoveValidate(PossiblePositions, this, X + i, Y - i);
                    Y_Neg = CheckPosition;
                }

                if (XY_Neg)
                {
                    CheckPosition = board.BasicMoveValidate(PossiblePositions, this, X - i, Y - i);
                    XY_Neg = CheckPosition;
                }

                if (Y_Pos)
                {
                    CheckPosition = board.BasicMoveValidate(PossiblePositions, this, X - i, Y + i);
                    Y_Pos = CheckPosition;
                }

                if (!XY_Pos && !XY_Neg && !Y_Pos && !Y_Neg)
                    break;
            }


            return PossiblePositions;
        }


        public override string ToString() => "B";
    }
}

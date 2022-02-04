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
            List<Position> possiblePositions = new List<Position>();
            Position curPos = base.FigurePosition;
            int x = curPos.Row;
            int y = curPos.Column;

            bool xPos, xNeg, yPos, yNeg; // Indicator if the diagonal is blocked
            xPos = xNeg = yPos = yNeg = true;

            for (int i = 1; true; i++)
            {
                xPos = xPos ? board.BasicMoveValidate(possiblePositions, this, x + i, y + i) : false;
                yNeg = yNeg ? board.BasicMoveValidate(possiblePositions, this, x + i, y - i) : false;
                xNeg = xNeg ? board.BasicMoveValidate(possiblePositions, this, x - i, y - i) : false;
                yPos = yPos ? board.BasicMoveValidate(possiblePositions, this, x - i, y + i) : false;

                if (!xPos && !xNeg && !yPos && !yNeg)
                    break;
            }


            return possiblePositions;
        }


        public override string ToString() => "B";
    }
}

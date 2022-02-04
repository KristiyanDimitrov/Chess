using System.Collections.Generic;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class Queen : Figure
    {
        public Queen(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> possiblePositions = new();
            Position curPos = base.FigurePosition;
            int x = curPos.Row;
            int y = curPos.Column;

            bool xPos, xNeg, yPos, yNeg; // Indicator if the movement to a given direction is blocked
            xPos = xNeg = yPos = yNeg = true;

            // Straing movements
            for (int i = 1;; i++)
            {
                xPos = xPos ? board.BasicMoveValidate(possiblePositions, this, x + i, y) : false;
                yNeg = yNeg ? board.BasicMoveValidate(possiblePositions, this, x, y - i) : false;
                xNeg = xNeg ? board.BasicMoveValidate(possiblePositions, this, x - i, y) : false;
                yPos = yPos ? board.BasicMoveValidate(possiblePositions, this, x, y + i) : false;

                if (!xPos && !xNeg && !yPos && !yNeg)
                    break;
            }

            xPos = xNeg = yPos = yNeg = true;
            // Diagonal movement
            for (int i = 1;; i++)
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

        public override string ToString() => "Q";
    }
}

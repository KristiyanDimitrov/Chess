using System.Collections.Generic;
using ChessLogic.Figures.Properties;

namespace ChessLogic.Figures
{
    [FigureInfo("Bishop", "B")]
    public class Bishop : Figure
    {
        public Bishop(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> possiblePositions = new();
            Position curPos = base.figurePosition;
            int x = curPos.Row;
            int y = curPos.Column;

            bool xPos, xNeg, yPos, yNeg; // Indicator if the diagonal is blocked
            xPos = xNeg = yPos = yNeg = true;

            for (int i = 1; ; i++)
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
        public override int evalValue { get { return 3; }}
    }
}

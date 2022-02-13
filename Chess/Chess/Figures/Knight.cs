using System;
using System.Collections.Generic;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    [FigureInfo("Knight", "N")]
    class Knight : Figure
    {
        public Knight(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> possiblePositions = new();
            Position curPos = base.FigurePosition;
            int curRow = curPos.Row;
            int curCol = curPos.Column;

            for(int x = -2; x <= 2; x++)
                for (int y = -2; y <= 2; y++)
                    if(Math.Abs(x) != Math.Abs(y) & x != 0 & y != 0)
                        board.BasicMoveValidate(possiblePositions, this, curRow + x, curCol + y);

            return possiblePositions;
        }

        public override string ToString() => "N";
    }
}

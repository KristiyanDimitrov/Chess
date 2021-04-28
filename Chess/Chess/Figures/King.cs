using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class King : Figure
    {
        public King(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> PossiblePositions = new List<Position>();
            Position CurPos = base.FigurePosition;
            int X = CurPos.Row;
            int Y = CurPos.Column;


            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                        board.BasicMoveValidate(PossiblePositions, this, X + x, Y + y);

            return PossiblePositions;
        }

        public override string ToString() => "K";
    }
}

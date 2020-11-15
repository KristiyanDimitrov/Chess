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
            return new List<Position>();
        }

        public override string ToString() => "R";
    }
}

﻿using System;
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
            return new List<Position>();
        }

        public override string ToString() => "K";
    }
}

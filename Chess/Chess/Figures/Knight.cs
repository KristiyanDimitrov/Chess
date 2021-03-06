﻿using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class Knight : Figure
    {
        public Knight(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> PossiblePositions = new List<Position>();
            Position CurPos = base.FigurePosition;
            int X = CurPos.Row;
            int Y = CurPos.Column;

            for(int x = -2; x <= 2; x++)
                for (int y = -2; y <= 2; y++)
                    if(Math.Abs(x) != Math.Abs(y) & x != 0 & y != 0)
                        board.BasicMoveValidate(PossiblePositions, this, X + x, Y + y);

            return PossiblePositions;
        }

        public override string ToString() => "N";
    }
}

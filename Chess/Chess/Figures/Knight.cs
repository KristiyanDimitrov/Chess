﻿using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class Knight : Figure
    {
        public Knight(int row, int column, ColorList color) : base(row, column, color) { }

        public override void MoveFigure()
        {
            Console.WriteLine("Move");
        }

        public override string ToString() => "N";
    }
}

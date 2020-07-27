using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    public class Pawn : Figure
    {
        public Pawn(int row, int column, ColorList color) : base(row, column, color) { }

        public override void MoveFigure()
        {
            Console.WriteLine("Move");
        }
    }
}

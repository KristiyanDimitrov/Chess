using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Figures.Properties
{
    public abstract class Figure
    {
        public enum ColorList{White, Black }
        public ColorList Color { get; private set; }
        public Position FigurePosition = null; //Relationship

        public abstract void MoveFigure();
        public abstract override string ToString();

        protected Figure (int row, int column, ColorList color )
        {
            Color = color;
            FigurePosition = new Position(row, column);
        }

        public void SetPosition (int row, int column)
        {
            FigurePosition.SetPosition(row, column);
        }

        public void SetPosition(Position position)
        {
            FigurePosition.SetPosition(position.Row, position.Column);
        }

        public Position GettPosition()
        {
            return FigurePosition;
        }

    }
}

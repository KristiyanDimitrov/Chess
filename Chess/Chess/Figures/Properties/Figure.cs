using System.Collections.Generic;

namespace Chess.Figures.Properties
{
    public abstract class Figure
    {
        public enum ColorList{White, Black}
        public ColorList Color { get; private set; }
        public Position FigurePosition; //Relationship

        public abstract List<Position> PossibleMoves(Board board);
        public abstract override string ToString();

        protected Figure (int row, int column, ColorList color )
        {
            Color = color;
            FigurePosition = new Position(row, column);
        }

        public virtual void SetPosition (int row, int column)
        {
            FigurePosition.SetPosition(row, column);
        }

        public virtual void SetPosition(Position position)
        {
            FigurePosition.SetPosition(position.Row, position.Column);
        }

        public Position GetPosition()
        {
            return FigurePosition;
        }

    }
}

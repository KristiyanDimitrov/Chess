using System.Collections.Generic;

namespace ChessLogic.Figures.Properties
{
    public abstract class Figure
    {
        public enum ColorList { White, Black }
        public ColorList color { get; private set; }
        public Position figurePosition; //Relationship
        public abstract int evalValue { get; }


        public abstract List<Position> PossibleMoves(Board board);
        public abstract override string ToString();

        protected Figure (int row, int column, ColorList color )
        {
            this.color = color;
            figurePosition = new Position(row, column);
        }

        public virtual void SetPosition (int row, int column)
        {
            figurePosition.SetPosition(row, column);
        }

        public virtual void SetPosition(Position position)
        {
            figurePosition.SetPosition(position.Row, position.Column);
        }

        public Position GetPosition()
        {
            return figurePosition;
        }

    }
}

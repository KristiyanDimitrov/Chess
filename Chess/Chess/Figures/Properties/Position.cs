

namespace ChessLogic.Figures.Properties
{
    public class Position
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public Position (int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void SetPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int[] GetPosition()
        {
            return new[] { Row, Column };
        }
    }
}

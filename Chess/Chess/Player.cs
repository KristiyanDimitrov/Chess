using ChessLogic.Figures.Properties;

namespace ChessLogic
{
    public class Player
    {
        public Player(Figure.ColorList color)
        {
            Color = color;
            InCheck = false;
        }

        public Figure.ColorList OppositeColor => Color == Figure.ColorList.White ? Figure.ColorList.Black : Figure.ColorList.White;
        public Figure.ColorList Color { get;  set; }
        public bool InCheck { get; set; }
    }
}

using Chess.Figures.Properties;

namespace Chess
{
    public struct Player
    {
        public Player(Figure.ColorList color)
        {
            Color = color;
            InCheck = false;
        }

        public Figure.ColorList Color { get;  set; }
        public bool InCheck { get; set; }
    }
}

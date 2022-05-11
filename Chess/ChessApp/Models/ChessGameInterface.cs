namespace ChessApp.Models
{
    public class ChessGameInterface
    {
        public int GameID { get; set; }
        public int WhitePlayer { get; set; }
        public int BlackPlayer { get; set; }
        public int WinnerID { get; set; }
    }
}

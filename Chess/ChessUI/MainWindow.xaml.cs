using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;
using Figure = ChessLogic.Figures.Properties.Figure;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] figureImages = new Image[8, 8];
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            Game currentGame = new();
            DrawBoard(currentGame.Chessboard);
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8 ; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    figureImages[r, c] = image;
                    FigureGrid.Children.Add(image);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Figure figure = board.GetFigureFromPosition(r, c);
                    if (figure != null)
                        figureImages[r, c].Source = Images.GetImage(figure);
                }
            }
        }
    }
}

﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using ChessLogic;
using ChessLogic.Figures.Properties;
using Figure = ChessLogic.Figures.Properties.Figure;
using System.Linq;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] figureImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];

        private List<Position> moves = new();

        private Position selectedPos = null;
        private Game currentGame;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            currentGame = new();
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

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);

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
                    else
                        figureImages[r, c].Source = null;
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position lastSelectedPos = ToSquarePosition(point);

            if (selectedPos == null && currentGame.Chessboard.ExistFigure(lastSelectedPos))
                OnFromPositionClick(lastSelectedPos);
            else if (selectedPos != null && moves.Exists(m => m.Row == lastSelectedPos.Row && m.Column == lastSelectedPos.Column))
            {
                currentGame.PlayMove(selectedPos, lastSelectedPos);
                selectedPos = null;
                HideHighlights();
                DrawBoard(currentGame.Chessboard);
            }              
            else if (selectedPos != null && !moves.Exists(m => m.Row == lastSelectedPos.Row && m.Column == lastSelectedPos.Column))
            {
                selectedPos = null;
                HideHighlights();
            }
            
        }

        private Position ToSquarePosition(Point poit)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(poit.Y / squareSize);
            int col = (int)(poit.X / squareSize);

            return new Position(row, col);
        }

        private void OnFromPositionClick(Position pos)
        {
            Figure selectFigure = currentGame.Chessboard.GetFigureFromPosition(pos);

            if (selectFigure.color != currentGame.CurrentPlayer.Color)
                return;

            List<Position> possibleMoves = selectFigure.PossibleMoves(currentGame.Chessboard);

            if (possibleMoves.Any())
            {
                selectedPos = pos;
                this.moves = possibleMoves;
                ShowHightlights();
            }
        }

        private void ShowHightlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach (Position move in moves)
            {
                highlights[move.Row, move.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach (Position move in moves)
            {
                highlights[move.Row, move.Column].Fill = Brushes.Transparent;
            }
        }
    }
}

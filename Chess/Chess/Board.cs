using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures;
using Chess.Figures.Properties;

namespace Chess
{
    public class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Figure [,] Figures { get; set; }
        public List<Figure> TakenFigures{ get; private set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Figures = new Figure[rows, columns];
            TakenFigures = new List<Figure>();
        }

        public Figure GetFigureFromPosition(int row, int column)
        {
            return Figures[row, column];
        }

        public Figure GetFigureFromPosition(Position position)
        {
            return Figures[position.Row, position.Column];
        }

        public Figure GetFigurePosition(Figure figure)
        {
            Position Position = figure.GettPosition();
            return Figures[Position.Row, Position.Column];
        }

        public bool ExistFigure(Position position)
        {
            if (ValidatePosition(position))
                return GetFigureFromPosition(position) != null;
            else
                return false;
        }

        public bool ValidatePosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
                return false;
            return true;
        }

        public void MoveFigure(Figure figure, Position position)
        {
            if (!ValidatePosition(figure.FigurePosition))
                throw new Exception("Position Invalid");

            // ¬¬¬ Need to check if the figure can move there.
            Figure taken = GetFigureFromPosition(position);
            if (taken?.Color == figure.Color)
                throw new Exception("You alredy have figure in that position");
            else if (taken != null && taken.Color != figure.Color)
                ClearFigure(figure);
            
            figure.SetPosition(position);
            Figures[position.Row, position.Column] = figure;
        }



        public void MoveFigure(Figure figure) // For intialising the board
        {
            Position Position = figure.GettPosition();

            Figures[Position.Row, Position.Column] = figure;
           ;
        }

        private void SetPosition(Figure figure, Position position)
        {
            figure.SetPosition(position.Row, position.Column);
            Figures[position.Row, position.Column] = figure;
        }
        private void SetPosition(Figure figure, int x, int y)
        {
            figure.SetPosition(x, y);
            Figures[x, y] = figure;
        }

        private void ClearFigure(Figure figure)
        {
            Position Position = figure.GettPosition();
            Figures[Position.Row, Position.Column] = null;
            TakenFigures.Add(figure);          
        }

        public Figure ClearPosition(Position position)
        {
            if (!ExistFigure(position))
                return null;

            Figure figure = GetFigureFromPosition(position);
            Figures[position.Row, position.Column] = null;
            return figure;
        }

        public static int GetLetterMap(char letter)
        {
            var CharPosition = new Dictionary<char, int>(){
            {'A', 0},
            {'B', 1},
            {'C', 2},
            {'D', 3},
            {'E', 4},
            {'F', 5},
            {'G', 6},
            {'H', 7}};

            if (CharPosition.ContainsKey(Char.ToUpper(letter)))
                return CharPosition[Char.ToUpper(letter)];
            else
                return -1;
        }
    }
}

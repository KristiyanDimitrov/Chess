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
        private Figure [,] Figures;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Figures = new Figure[rows, columns];
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
            {
                return false;
            }
            return true;
        }

        public void MoveFigure(Figure figure, Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
                throw new Exception("Position Invalid");
            else if (Figures[position.Row, position.Column] != null)
                throw new Exception("Position is already taken");

            Figures[position.Row, position.Column] = figure;
            figure.SetPosition(position.Row, position.Column);
        }



        public void MoveFigure(Figure figure) // For intialising the board
        {
            Position Position = figure.GettPosition();

            Figures[Position.Row, Position.Column] = figure;
            figure.SetPosition(Position.Row, Position.Column);
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

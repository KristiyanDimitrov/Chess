using System;
using System.Collections.Generic;
using System.Linq;
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
        private Figure FigureShadowBuffer = null;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Figures = new Figure[rows, columns];
            TakenFigures = new List<Figure>();
        }

        // Return false if the figure can not advance further
        public bool BasicMoveValidate (List<Position> possiblePositions,  Figure figure, int x, int y)
        {
            if (!ValidatePosition(new Position(x,y)))
                return false;

            if (ExistFigure(x, y) && GetFigureFromPosition(x, y).Color != figure.Color)
            {
                possiblePositions.Add(new Position(x, y));
                return false;
            }
            else if (GetFigureFromPosition(x, y)?.Color == figure.Color)
            {
                return false;
            }                
            else
            {
                possiblePositions.Add(new Position(x, y));
                return true;
            }
                
        }

        public Figure GetFigureFromPosition(int row, int column)
        {
            
            if (ValidatePosition(new Position(row,column)))
                return Figures[row, column];
            else
                return null;
        }

        public Figure GetFigureFromPosition(Position position)
        {
            return Figures[position.Row, position.Column];
        }

        public bool ExistFigure(Position position)
        {
            if (ValidatePosition(position))
                return GetFigureFromPosition(position) != null;
            else
                return false;
        }
        public bool ExistFigure(int x, int y)
        {
            Position position = new Position(x, y);
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
            ClearFigure(figure, true);
            
            figure.SetPosition(position);
            Figures[position.Row, position.Column] = figure;
        }

        public void MoveFigure(Figure figure) // For intialising the board
        {
            Position Position = figure.GetPosition();

            Figures[Position.Row, Position.Column] = figure;
        }

        // Used to validate moves
        public void MoveShadowFigure(Figure figure, Position position)
        {
            ClearFigure(figure, true);

            // If there is a figure in the field of the shadowmove, put it in the buffer
            if (Figures[position.Row, position.Column] != null)
                FigureShadowBuffer = Figures[position.Row, position.Column];
            else
                Figures[position.Row, position.Column] = figure;
        }
        public void ResetShadowMove(Figure figure, Position position) 
        {
            Figures[figure.FigurePosition.Row, figure.FigurePosition.Column] = figure;

            if (FigureShadowBuffer != null)
            {
                Figures[FigureShadowBuffer.FigurePosition.Row, FigureShadowBuffer.FigurePosition.Column] = FigureShadowBuffer;
                FigureShadowBuffer = null;
            } 
            else
                Figures[position.Row, position.Column] = null;
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

        private void ClearFigure(Figure figure, bool isMoved = false)
        {
            Position Position = figure.GetPosition();
            Figures[Position.Row, Position.Column] = null;
            if (!isMoved)
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

        public Figure GetKingFigure(Figure.ColorList color) => Figures.Cast<Figure>().ToArray().FirstOrDefault(x => x?.Color == color && x.GetType().Name == "King");

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

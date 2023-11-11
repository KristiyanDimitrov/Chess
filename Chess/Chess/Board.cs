using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessLogic.Figures;
using ChessLogic.Figures.Properties;

namespace ChessLogic
{
    public class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Figure [,] Figures { get; set; }
        public List<Figure> TakenFigures{ get; private set; }
        private Figure _figureShadowBuffer;

        public Figure EnPassantEnabledPawn;
        public Position EnPassantPosition;

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

            if (ExistFigure(x, y) && GetFigureFromPosition(x, y).color != figure.color)
            {
                possiblePositions.Add(new Position(x, y));
                return false;
            }
            else if (GetFigureFromPosition(x, y)?.color == figure.color)
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

        public Type GetFigureType(Position position)
        {
            var tst1 = GetFigureFromPosition(position);
            var tst2 = tst1.GetType();

            return GetFigureFromPosition(position).GetType();
        }

        public Figure.ColorList GetFigureColor(Position position)
        {
            return GetFigureFromPosition(position).color;
        }

        public bool ExistFigure(int x, int y)
        {
            Position position = new(x, y);
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
            ClearFigure(figure);

            Figure figureFromPossition = GetFigureFromPosition(position);
            if (figureFromPossition != null)
                ClearFigure(figureFromPossition,true);
            
            figure.SetPosition(position);
            Figures[position.Row, position.Column] = figure;
        }

        public void MoveFigure(Figure figure) // For intialising the board
        {
            Position position = figure.GetPosition();

            Figures[position.Row, position.Column] = figure;
        }

        // Used to validate moves <<ShadowMove>> !!FIGURE POSSITION DOES NOT CHANGE!!
        public void MoveShadowFigure(Figure figure, Position position)
        {
            ClearFigure(figure);

            // If there is a figure in the field of the shadow move, put it in the buffer
            if (Figures[position.Row, position.Column] != null)
                _figureShadowBuffer = Figures[position.Row, position.Column];

            Figures[position.Row, position.Column] = figure;
        }

        public void ResetShadowMove(Figure figure, Position position) 
        {
            Figures[figure.figurePosition.Row, figure.figurePosition.Column] = figure;

            if (_figureShadowBuffer != null)
            {
                Figures[_figureShadowBuffer.figurePosition.Row, _figureShadowBuffer.figurePosition.Column] = _figureShadowBuffer;
                _figureShadowBuffer = null;
            } 
            else
                Figures[position.Row, position.Column] = null;
        }

        public bool IsEnPassantPossition(int row, int col)
        {
            if (row == EnPassantPosition?.Row && col == EnPassantPosition?.Column)
                return true;
            return false;
        }

        public void ClearFigure(Figure figure, bool isRemoved = false)
        {
            Position position = figure.GetPosition();
            Figures[position.Row, position.Column] = null;
            if (isRemoved)
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

        public Figure GetKingFigure(Figure.ColorList color) => Figures.Cast<Figure>().ToArray().FirstOrDefault(x => x?.color == color && x.GetType().Name == "King");

        public static int GetLetterMap(char letter)
        {
            var charPosition = new Dictionary<char, int>(){
            {'A', 0},
            {'B', 1},
            {'C', 2},
            {'D', 3},
            {'E', 4},
            {'F', 5},
            {'G', 6},
            {'H', 7}};

            if (charPosition.ContainsKey(Char.ToUpper(letter)))
                return charPosition[Char.ToUpper(letter)];
            else
                return -1;
        }

        public bool PlayerHasPossibleMoves(Player player)
        {
            foreach (Figure figure in Figures.Cast<Figure>().ToArray().Where(x => x?.color == player.Color)) // For each figure that the player has
            {
                if (figure.PossibleMoves(this).Any())
                {
                    return true;
                }
            }

            return false;
        }
    }
}

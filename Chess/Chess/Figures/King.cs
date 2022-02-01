﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class King : Figure
    {
        public King(int row, int column, ColorList color) : base(row, column, color) { }
        public bool KingInCheck { get; set; } = false;
        private Dictionary<Position, Rook> _castleMoveRook = new();
        private bool FirstMove = true;
        
        public override List<Position> PossibleMoves(Board board)
        {
            _castleMoveRook = new();
            List<Position> PossiblePositions = new List<Position>();
            Position CurPos = base.FigurePosition;
            int X = CurPos.Row;
            int Y = CurPos.Column;

            // Normal moves
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    board.BasicMoveValidate(PossiblePositions, this, X + x, Y + y);

            
            // Castle move. The validation for fields under attack is done outside of this class in Game.GetPossibleMoves().
            if (FirstMove && !KingInCheck)
            {
                for (int y = Y; y >= 0; y--)
                {
                    Figure figure = board.GetFigureFromPosition(X, y);
                    if (figure is null)
                        continue;
                    if (figure != null && figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(PossiblePositions, this, X, Y - 2))
                            _castleMoveRook.Add(new Position(X, Y - 2), figure as Rook); // The castle move of the king, the Rook figure or possition
                }

                for (int y = Y; y <= 7; y++)
                {
                    Figure figure = board.GetFigureFromPosition(X, y);
                    if (figure is null)
                        continue;
                    if (figure != null && figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(PossiblePositions, this, X, Y + 2))
                            _castleMoveRook.Add(new Position(X, Y + 2), figure as Rook);
                }
            }

            return PossiblePositions;
        }
        public override void SetPosition(int row, int column)
        {
            FirstMove = false;
            base.SetPosition(row, column);
        }

        public override void SetPosition(Position position)
        {
            FirstMove = false;
            base.SetPosition(position);
        }

        public override string ToString() => "K";

        public Rook GetCastleMoveRook(Position pos)
        {
            Position key = _castleMoveRook.Keys.FirstOrDefault(x => x.Column == pos.Column && x.Row == pos.Row);

            return _castleMoveRook[key];
        }
    }
}

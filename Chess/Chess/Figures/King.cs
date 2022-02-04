using System;
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
        private bool _isFiirstMove = true;
        
        public override List<Position> PossibleMoves(Board board)
        {
            _castleMoveRook = new();
            List<Position> possiblePositions = new List<Position>();
            Position curPos = base.FigurePosition;
            int X = curPos.Row;
            int Y = curPos.Column;

            // Normal moves
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    board.BasicMoveValidate(possiblePositions, this, X + x, Y + y);

            
            // Castle move. The validation for fields under attack is done outside of this class in Game.GetPossibleMoves().
            if (_isFiirstMove && !KingInCheck)
            {
                for (int y = Y; y >= 0; y--)
                {
                    Figure figure = board.GetFigureFromPosition(X, y);
                    if (figure is null)
                        continue;
                    if (figure != null && figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(possiblePositions, this, X, Y - 2))
                            _castleMoveRook.Add(new Position(X, Y - 2), figure as Rook); // The castle move of the king, the Rook figure or possition
                }

                for (int y = Y; y <= 7; y++)
                {
                    Figure figure = board.GetFigureFromPosition(X, y);
                    if (figure is null)
                        continue;
                    if (figure != null && figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(possiblePositions, this, X, Y + 2))
                            _castleMoveRook.Add(new Position(X, Y + 2), figure as Rook);
                }
            }

            return possiblePositions;
        }
        public override void SetPosition(int row, int column)
        {
            _isFiirstMove = false;
            base.SetPosition(row, column);
        }

        public override void SetPosition(Position position)
        {
            _isFiirstMove = false;
            base.SetPosition(position);
        }

        public override string ToString() => "K";

        public Tuple<Rook,Position> GetCastleMoveRook(Position kingMoveTo)
        {
            Position key = _castleMoveRook.Keys.FirstOrDefault(x => x.Column == kingMoveTo.Column && x.Row == kingMoveTo.Row);

            if (Math.Abs(FigurePosition.Column - kingMoveTo.Column) == 2)
            {
                Rook theRook = _castleMoveRook[key];

                int kingToRookDistance = FigurePosition.Column - theRook.GetPosition().Column;
                int offset = kingToRookDistance > 0 ? -1 : 1;

                return Tuple.Create(theRook, new Position(FigurePosition.Row, FigurePosition.Column + offset));
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Figures.Properties;
using Newtonsoft.Json;

namespace Chess.Figures
{
    [FigureInfo("King","K")]
    class King : Figure
    {
        public King(int row, int column, ColorList color) : base(row, column, color) { }
        [JsonIgnore]
        public bool KingInCheck { get; set; } = false;
        private Dictionary<Position, Rook> _castleMoveRook = new();
        private bool _isFirstMove = true;
        public override string FigureName => this.ToString();

        public override List<Position> PossibleMoves(Board board)
        {
            _castleMoveRook = new Dictionary<Position, Rook>();
            List<Position> possiblePositions = new();
            Position curPos = base.FigurePosition;
            int curRow = curPos.Row;
            int curCol = curPos.Column;

            // Normal moves
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    board.BasicMoveValidate(possiblePositions, this, curRow + x, curCol + y);

            
            // Castle move. The validation for fields under attack is done outside of this class in Game.GetPossibleMoves().
            if (_isFirstMove && !KingInCheck)
            {
                for (int y = curCol; y >= 0; y--)
                {
                    Figure figure = board.GetFigureFromPosition(curRow, y);
                    if (figure is null)
                        continue;
                    if (figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(possiblePositions, this, curRow, curCol - 2))
                            _castleMoveRook.Add(new Position(curRow, curCol - 2), figure as Rook); // The castle move of the king, the Rook figure or possition
                }

                for (int y = curCol; y <= 7; y++)
                {
                    Figure figure = board.GetFigureFromPosition(curRow, y);
                    if (figure is null)
                        continue;
                    if (figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(possiblePositions, this, curRow, curCol + 2))
                            _castleMoveRook.Add(new Position(curRow, curCol + 2), figure as Rook);
                }
            }

            return possiblePositions;
        }
        public override void SetPosition(int row, int column)
        {
            _isFirstMove = false;
            base.SetPosition(row, column);
        }

        public override void SetPosition(Position position)
        {
            _isFirstMove = false;
            base.SetPosition(position);
        }

        public override string ToString() => "K";

        public Tuple<Rook,Position> GetCastleMoveRook(Position kingMoveTo)
        {
            Position key = _castleMoveRook.Keys.FirstOrDefault(x => x.Column == kingMoveTo.Column && x.Row == kingMoveTo.Row);

            if (key != null && Math.Abs(FigurePosition.Column - kingMoveTo.Column) == 2)
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

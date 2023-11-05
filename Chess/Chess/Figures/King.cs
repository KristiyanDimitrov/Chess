using System;
using System.Collections.Generic;
using System.Linq;
using ChessLogic.Figures.Properties;

namespace ChessLogic.Figures
{
    [FigureInfo("King","K")]
    public class King : Figure
    {
        public King(int row, int column, ColorList color) : base(row, column, color) { }
        public bool KingInCheck { get; set; } = false;
        private Dictionary<Position, Rook> _castleMoveRook = new();
        private bool _isFirstMove = true;
        
        public override List<Position> PossibleMoves(Board board)
        {
            _castleMoveRook = new Dictionary<Position, Rook>();
            List<Position> possiblePositions = new();
            int curRow = figurePosition.Row;
            int curCol = figurePosition.Column;

            // Normal moves
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    board.BasicMoveValidate(possiblePositions, this, curRow + x, curCol + y);

            
            // Castle move. The validation for fields under attack is done outside of this class in Game.GetPossibleMoves().
            if (_isFirstMove && !KingInCheck)
            {
                for (int y = curCol - 1; y >= 0; y--)
                {
                    Figure figure = board.GetFigureFromPosition(curRow, y);
                    if (figure is null)
                        continue;
                    if (figure is not Rook)
                        break;
                    if (figure is Rook && ((Rook)figure).IsFirstMove)
                        if (board.BasicMoveValidate(possiblePositions, this, curRow, curCol - 2))
                            _castleMoveRook.Add(new Position(curRow, curCol - 2), figure as Rook); // The castle move of the king, the Rook figure or possition
                }

                for (int y = curCol + 1; y <= 7; y++)
                {
                    Figure figure = board.GetFigureFromPosition(curRow, y);
                    if (figure is null)
                        continue;
                    if (figure is not Rook)
                        break;
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

        public Tuple<Rook,Position> GetCastleMoveRook(Position kingMoveTo)
        {
            Position key = _castleMoveRook.Keys.FirstOrDefault(x => x.Column == kingMoveTo.Column && x.Row == kingMoveTo.Row);

            if (key != null && Math.Abs(figurePosition.Column - kingMoveTo.Column) == 2)
            {
                Rook theRook = _castleMoveRook[key];

                int kingToRookDistance = figurePosition.Column - theRook.GetPosition().Column;
                int offset = kingToRookDistance > 0 ? -1 : 1;

                return Tuple.Create(theRook, new Position(figurePosition.Row, figurePosition.Column + offset));
            }

            return null;
        }

        public override string ToString() => "K";
        public override int evalValue { get { return 100; } }
    }
}

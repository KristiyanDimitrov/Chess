using System;
using System.Collections.Generic;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    class King : Figure
    {
        public King(int row, int column, ColorList color) : base(row, column, color) { }
        public bool KingInCheck { get; set; } = false;
        private bool _castleMovePossible = false;
        public bool CastleMovePossible { get { return _castleMovePossible; } set { _castleMovePossible = false; } }
        public override List<Position> PossibleMoves(Board board)
        {
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
                        if (board.BasicMoveValidate(PossiblePositions, this, X, Y + 2))
                            _castleMovePossible = true;
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
        private bool FirstMove = true;
        private bool _CastleMovePossible;
        private bool castleMovePossible = false;
    }
}

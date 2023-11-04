using System;
using System.Collections.Generic;
using ChessLogic.Figures.Properties;

namespace ChessLogic.Figures
{
    [FigureInfo("Pawn", "P")]
    public class Pawn : Figure
    {
        public bool EnPassant { get; private set; }
        public Pawn(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> possiblePositions = new();
            Position curPos= base.figurePosition;

            if (base.color == ColorList.Black)
            {
                for (int x = curPos.Row + 1; x <= curPos.Row + 1 + Convert.ToInt32(_firstMove); x++)
                {
                    if (!board.ExistFigure(x, curPos.Column) && (x == curPos.Row + 1 || possiblePositions.Exists(z => z.Row == curPos.Row + 1 && z.Column == curPos.Column)))
                        possiblePositions.Add(new Position(x, curPos.Column));

                    for (int y = curPos.Column - 1; y <= curPos.Column + 1; y+=2)
                    {
                        if (Math.Abs(x % curPos.Row) == 1 && ((board.ExistFigure(x, y) || board.IsEnPassantPossition(x, y)) && board.GetFigureFromPosition(x, y)?.color != base.color))
                            possiblePositions.Add(new Position(x, y));
                    }
                }
            }
            else
            {
                for (int x = curPos.Row - 1; x >= curPos.Row - 1 - Convert.ToInt32(_firstMove); x--)
                {
                    if (!board.ExistFigure(x, curPos.Column) && (x == curPos.Row - 1 || possiblePositions.Exists( z => z.Row == curPos.Row - 1 && z.Column == curPos.Column)))
                        possiblePositions.Add(new Position(x, curPos.Column));

                    for (int y = curPos.Column - 1; y <= curPos.Column + 1; y+=2)
                    {
                        if ( Math.Abs(curPos.Row % x) == 1 && ((board.ExistFigure(x, y) || board.IsEnPassantPossition(x, y)) && board.GetFigureFromPosition(x, y)?.color != base.color))
                            possiblePositions.Add(new Position(x, y));
                    }
                }
            }
            
            return possiblePositions;
        }

        public override void SetPosition(int row, int column)
        {
            _firstMove = false;
            EnPassant = Math.Abs(row - base.GetPosition().Row) == 2;

            base.SetPosition(row, column);
        }

        public override void SetPosition(Position position)
        {
            _firstMove = false;
            EnPassant = Math.Abs(position.Row - base.GetPosition().Row) == 2;
            base.SetPosition(position);
        }

        public override string ToString() => "P";
        private bool _firstMove = true;
        public override int evalValue { get { return 1; } }
    }
}

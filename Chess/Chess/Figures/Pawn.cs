using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Figures.Properties;

namespace Chess.Figures
{
    public class Pawn : Figure
    {
        public Pawn(int row, int column, ColorList color) : base(row, column, color) { }

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> PossiblePositions = new List<Position>();
            Position CurPos= base.FigurePosition;

            if (base.Color == ColorList.Black)
            {
                for (int x = CurPos.Row + 1; x <= CurPos.Row + 2; x++)
                {
                    if (!board.ExistFigure(x, CurPos.Column) && (x == CurPos.Row + 1 || PossiblePositions.Exists(z => z.Row == CurPos.Row + 1 && z.Column == CurPos.Column)))
                        PossiblePositions.Add(new Position(x, CurPos.Column));

                    for (int y = CurPos.Column - 1; y <= CurPos.Column + 1; y+=2)
                    {
                        Figure test = board.GetFigureFromPosition(x, y);
                        if (Math.Abs(CurPos.Row % x) == 1 && (board.ExistFigure(x, y) && board.GetFigureFromPosition(x, y)?.Color != base.Color))
                            PossiblePositions.Add(new Position(x, y));
                    }
                }
            }
            else
            {
                for (int x = CurPos.Row - 1; x >= CurPos.Row - 2; x--)
                {
                    if (!board.ExistFigure(x, CurPos.Column) && (x == CurPos.Row - 1 || PossiblePositions.Exists( z => z.Row == CurPos.Row - 1 && z.Column == CurPos.Column)))
                        PossiblePositions.Add(new Position(x, CurPos.Column));

                    for (int y = CurPos.Column - 1; y <= CurPos.Column + 1; y+=2)
                    {
                        Figure test = board.GetFigureFromPosition(x, y);
                        if ( Math.Abs(CurPos.Row % x) == 1 && (board.ExistFigure(x,y) && board.GetFigureFromPosition(x, y)?.Color != base.Color))
                            PossiblePositions.Add(new Position(x, y));
                    }
                }
            }
            

            return PossiblePositions;
        }

        public override void SetPosition(int row, int column)
        {
            FistMove = FistMove == true ? false : FistMove;
            base.SetPosition(row, column);
        }

        public override void SetPosition(Position position)
        {
            FistMove = FistMove == true ? false : FistMove;
            base.SetPosition(position);
        }

        public override string ToString() => "P";
        private bool FistMove = true;
    }
}

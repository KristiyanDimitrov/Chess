﻿using System.Collections.Generic;
using ChessLogic.Figures.Properties;

namespace ChessLogic.Figures
{
    [FigureInfo("Rook", "R")]
    public class Rook : Figure
    {
        public Rook(int row, int column, ColorList color) : base(row, column, color) { }

        public bool IsFirstMove { get; private set; } = true;

        public override List<Position> PossibleMoves(Board board)
        {
            List<Position> possiblePositions = new();
            Position curPos = base.figurePosition;

            int curRow = curPos.Row;
            int curCol = curPos.Column;

            bool xPos, xNeg, yPos, yNeg; // Indicator if the movement to a given direction is blocked
            xPos = xNeg = yPos = yNeg = true;

            for (int i = 1;; i++)
            {
                xPos = xPos ? board.BasicMoveValidate(possiblePositions, this, curRow + i, curCol) : false;
                yNeg = yNeg ? board.BasicMoveValidate(possiblePositions, this, curRow, curCol - i) : false;
                xNeg = xNeg ? board.BasicMoveValidate(possiblePositions, this, curRow - i, curCol) : false;
                yPos = yPos ? board.BasicMoveValidate(possiblePositions, this, curRow, curCol + i) : false;

                if (!xPos && !xNeg && !yPos && !yNeg)
                    break;
            }

            return possiblePositions;
        }

        public override void SetPosition(int row, int column)
        {
            IsFirstMove = false;
            base.SetPosition(row, column);
        }

        public override void SetPosition(Position position)
        {
            IsFirstMove = false;
            base.SetPosition(position);
        }
        public override string ToString() => "R";
        public override int evalValue { get { return 5; } }
    }
}

using System;

namespace GameLogic
{
    public static class BoardUtil
    {
        public static BoardPosition GetNewBoardPosition(BoardPosition currentPosition, MoveDirection intendedMove)
        {
            switch (intendedMove)
            {
                case MoveDirection.Up:
                    return new BoardPosition(currentPosition.X,currentPosition.Y + 1);

                case MoveDirection.Down:
                    return new BoardPosition(currentPosition.X, currentPosition.Y - 1);
                    
                case MoveDirection.Left:
                    return new BoardPosition(currentPosition.X - 1, currentPosition.Y);

                case MoveDirection.Right:
                    return new BoardPosition(currentPosition.X + 1, currentPosition.Y);

                default:
                    throw new ArgumentOutOfRangeException(nameof(intendedMove), intendedMove, "Move position is not defined.");
            }
        }

        public static string GetChessNotation(this BoardPosition position)
        {
            return position.X < 26 ? $"{Convert.ToChar(65+position.X)}{position.Y + 1}" : $"{position.X + 1} - {position.Y + 1}";
        }
    }
}

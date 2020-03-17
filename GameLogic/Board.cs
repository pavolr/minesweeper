using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class Board
    {
        private readonly Field[,] _board;
        private readonly int _numberOfRows;
        private readonly int _numberOfColumns;

        public Board(int numberOfColumns, int numberOfRows, List<Tuple<int, int>> mineMap)
        {
            _numberOfRows = numberOfRows;
            _numberOfColumns = numberOfColumns;
            _board = new BoardBuilder(_numberOfColumns, _numberOfRows, mineMap).Build();
        }

        private bool isPositionOnTheBoard(BoardPosition position)
        {
            return (position.X >= 0 && position.X < _numberOfColumns) && (position.Y >= 0 && position.Y < _numberOfRows);
        }

        public bool IsMoveAvailable(BoardPosition currentPosition, MoveDirection intendedMove)
        {
            return isPositionOnTheBoard(BoardUtil.GetNewBoardPosition(currentPosition, intendedMove));
        }

        public bool IsGoalReached(BoardPosition currentPosition)
        {
            return currentPosition.Y == _numberOfRows - 1;
        }

        public bool IsMinePresent(BoardPosition currentPosition)
        {
            return _board[currentPosition.X, currentPosition.Y].IsMine;
        }
        
        public List<string> GetTextualRepresentation(BoardPosition position)
        {
            var result = new List<string>();

            for (int y = 0; y < _numberOfRows; y++)
            {
                char[] line = new char[_numberOfColumns];
                for (int x = 0; x < _numberOfColumns; x++)
                {
                    line[x] = position.X == x && position.Y == y ? _board[x, y].IsMine ? 'M' : 'P' : _board[x, y].IsExploded ? '*' : '-';
                }
                result.Add(new string(line));
            }

            return result;
        }

        public void ExplodeMine(BoardPosition currentPosition)
        {
            _board[currentPosition.X, currentPosition.Y].Explode();
        }
    }
}

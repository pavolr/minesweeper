using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class Game
    {
        private readonly Board _board;
        private BoardPosition _currentPosition;
        private int _lives;
        private int _moves; 

        public event EventHandler<GameArguments> GameGoalReached;
        public event EventHandler<GameArguments> MineFound;
        public event EventHandler<GameArguments> MoveNotAvailable;
        public event EventHandler<GameArguments> MoveReported;
        public event EventHandler<GameArguments> LivesRunOut;
        
        public Game(int cols, int rows , int lives, List<Tuple<int,int>> mineMap , BoardPosition initialPosition)
        {
            _lives = lives;
            _board = new Board(cols,rows, mineMap);
            _currentPosition = initialPosition; 
        }

        public void AdvanceGame(MoveDirection moveDirection)
        {
            if (_board.IsMoveAvailable(_currentPosition, moveDirection))
            {
                _currentPosition = BoardUtil.GetNewBoardPosition(_currentPosition, moveDirection);
                _moves++;

                var messageAlreadyGiven = false;

                if (_board.IsMinePresent(_currentPosition))
                {
                    _lives--;
                    _board.ExplodeMine(_currentPosition);
                    MineFound?.Invoke(this, GetGameStatusArguments());
                    messageAlreadyGiven = true;

                    if (_lives < 1)
                    {
                        LivesRunOut?.Invoke(this, GetGameStatusArguments());
                        return;
                    }
                }

                if (_board.IsGoalReached(_currentPosition))
                {
                    GameGoalReached?.Invoke(this, GetGameStatusArguments());
                    messageAlreadyGiven = true;
                }

                if (!messageAlreadyGiven)
                {
                    MoveReported?.Invoke(this, GetGameStatusArguments());
                }

            }
            else
            {
                MoveNotAvailable?.Invoke(this, GetGameStatusArguments());
            }
        }

        public  GameArguments GetGameStatusArguments()
        {
            return new GameArguments(_lives, _board.IsGoalReached(_currentPosition), _currentPosition, _moves, GetTextualRepresentation());
        }

        public List<string> GetTextualRepresentation()
        {
            return _board.GetTextualRepresentation(_currentPosition);
        }
    }
}

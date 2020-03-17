using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class GameArguments : EventArgs
    {
        public GameArguments(int livesLeft, bool gameGoalReached, BoardPosition lastKnownPosition, int movesElapsed, List<string> textRepresentation)
        {
            LivesLeft = livesLeft;
            GameGoalReached = gameGoalReached;
            LastKnownBoardPosition = lastKnownPosition;
            MovesElapsed = movesElapsed;
            TextRepresentation = textRepresentation;
        }

        public int LivesLeft { get; }       
        public bool GameGoalReached { get; }
        public BoardPosition  LastKnownBoardPosition { get; }
        public int MovesElapsed { get; }
        public List<string> TextRepresentation { get; }
    }
}
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Threading;
using GameLogic;

namespace Minesweeper
{
    class Program
    {
        private const int Cols = 8;
        private const int Rows = 8; 

        static void Main(string[] args)
        {
            var prg = new Program();
            prg.RunGame();
            Console.ReadLine();
        }

        private MoveDirection _characterDirection;
        private bool _gameIsOn;

        void RunGame()
        {
            var rnd = new Random();

            var game = new Game(Cols, Rows, 5, new RandomMapBuilder(Cols, Rows).Build(),
                new BoardPosition(rnd.Next(Cols), 0));
            game.GameGoalReached += GameOnGameGoalReached;
            game.MoveNotAvailable += GameOnMoveNotAvailable;
            game.MineFound += GameOnMineFound;
            game.MoveReported += GameOnMoveReported;
            game.LivesRunOut += GameOnLivesRunOut;
            _gameIsOn = true;
            
            Report(game.GetGameStatusArguments(),"Begin game");
            Console.WriteLine("User arrow keys to navigate, Esc to exit the game.");

            while (_gameIsOn)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            _characterDirection = MoveDirection.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            _characterDirection = MoveDirection.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            _characterDirection = MoveDirection.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            _characterDirection = MoveDirection.Down;
                            break;
                        case ConsoleKey.Escape:
                            _gameIsOn = false;
                            break;
                        default:
                            break;
                    }
                    game.AdvanceGame(_characterDirection);

                }
                Thread.Sleep(100);
            }
        }

        private void GameOnLivesRunOut(object sender, GameArguments e)
        {
            Report(e, "Lives run  out");
            _gameIsOn = false;
        }

        private void GameOnMoveReported(object sender, GameArguments e)
        {
            Report(e, "Move");
        }

        private void GameOnMineFound(object sender, GameArguments e)
        {
            ReportTextRepresentation(e.TextRepresentation);
            Console.WriteLine($"Mine Found {e.LastKnownBoardPosition.GetChessNotation()}");
        }

        private void GameOnMoveNotAvailable(object sender, GameArguments e)
        {
            ReportTextRepresentation(e.TextRepresentation);
            Console.WriteLine("Move not available.");
        }

        private void GameOnGameGoalReached(object sender, GameArguments e)
        {
            _gameIsOn = false;
            Report(e, "Game goal reached");
            Console.WriteLine($"Final score {e.MovesElapsed}");
        }

        private static void Report(GameArguments gameArguments, string status)
        {
            ReportTextRepresentation(gameArguments.TextRepresentation);
            Console.WriteLine(
                $"{status} - lives left {gameArguments.LivesLeft} , moves elapsed {gameArguments.MovesElapsed}, last position {gameArguments.LastKnownBoardPosition.GetChessNotation()} ");
        }

        private static void ReportTextRepresentation(List<string> textRepresentation)
        {
            Console.Clear();
            for (int i = textRepresentation.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(textRepresentation[i]);
            }
        }
    }
}

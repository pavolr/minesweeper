using System;
using System.Collections.Generic;
using GameLogic;
using NUnit.Framework;

namespace UnitTest
{
    public class GameTest
    {

        [Test]
        public void test_that_three_row_board_finishes_in_two_moves()
        {
            var game = new Game(3, 3, 3, new List<Tuple<int, int>>(), new BoardPosition(0, 0));
            game.AdvanceGame(MoveDirection.Up);
            game.AdvanceGame(MoveDirection.Up);
            var arguments = game.GetGameStatusArguments();
            Assert.AreEqual(2, arguments.MovesElapsed);
            Assert.GreaterOrEqual(arguments.LivesLeft, 1);
            Assert.IsTrue(arguments.GameGoalReached);
        }

        [Test]
        public void test_that_three_row_board_fully_mined_triggers_event_two_times()
        {
            var game = new Game(3, 3, 3, new AllMinesMapBuilder(3, 3).Build(), new BoardPosition(0, 0));
            var mineCount = 0;
            game.MineFound += (sender, arguments) => { mineCount++; };
            game.AdvanceGame(MoveDirection.Up);
            game.AdvanceGame(MoveDirection.Up);
            Assert.AreEqual(2, mineCount);
        }

        [Test]
        public void test_that_textual_representation_contains_current_position()
        {
            var game = new Game(3, 3, 3, new List<Tuple<int, int>>(), new BoardPosition(0, 0));
            var text = game.GetTextualRepresentation();
            Assert.AreEqual("P--", text[0]);
        }

        [Test]
        public void test_that_textual_representation_contains_current_position_after_one_move()
        {
            var game = new Game(3, 3, 3, new List<Tuple<int, int>>(), new BoardPosition(0, 0));
            game.AdvanceGame(MoveDirection.Right);
            var text = game.GetTextualRepresentation();
            Assert.AreEqual("-P-", text[0]);
        }

        [Test]
        public void test_that_textual_representation_contains_current_position_after_two_moves()
        {
            var game = new Game(3, 3, 3, new List<Tuple<int, int>>(), new BoardPosition(0, 0));
            game.AdvanceGame(MoveDirection.Right);
            game.AdvanceGame(MoveDirection.Up);
            var actual = game.GetTextualRepresentation();
            Assert.AreEqual("---", actual[0]);
            Assert.AreEqual("-P-", actual[1]);
            Assert.AreEqual("---", actual[2]);
        }

        [Test]
        public void test_that_textual_representation_contains_current_position_after_three_moves()
        {
            var game = new Game(3, 3, 3, new OneMineMapBuilder(3, 3).Build(), new BoardPosition(0, 0));
            bool gameGoalMet = false;
            int mineCount = 0;
            game.GameGoalReached += (sender, arguments) => { gameGoalMet = arguments.GameGoalReached; };
            game.MineFound += (sender, arguments) => { mineCount++; };
            game.AdvanceGame(MoveDirection.Right);
            game.AdvanceGame(MoveDirection.Up);
            game.AdvanceGame(MoveDirection.Up);

            var text = game.GetTextualRepresentation();
            Assert.AreEqual("---", text[0]);
            Assert.AreEqual("-*-", text[1]);
            Assert.AreEqual("-P-", text[2]);
            Assert.IsTrue(gameGoalMet);
            Assert.AreEqual(1, mineCount);

        }

        [Test]
        public void test_that_when_move_to_mined_field_it_explodes_displays_M()
        {
            var game = new Game(3, 3, 3, new OneMineMapBuilder(3, 3).Build(), new BoardPosition(1, 0));
            game.AdvanceGame(MoveDirection.Up);
            var expected = new List<string>() {"---",
                                               "-M-",
                                               "---"};
            expected.Reverse();
            Assert.AreEqual(expected, game.GetTextualRepresentation());
        }
        [Test]
        public void test_that_when_move_to_over_mine_field_it_displays_P_and_mine_is_exploded()
        {
            var game = new Game(3, 3, 3, new OneMineMapBuilder(3, 3).Build(), new BoardPosition(1, 0));
            game.AdvanceGame(MoveDirection.Up);
            game.AdvanceGame(MoveDirection.Up);
            var expected = new List<string>() {"-P-",
                                               "-*-",
                                               "---"};
            expected.Reverse();
            Assert.AreEqual(expected, game.GetTextualRepresentation());
        }

        [Test]
        public void test_that_game_can_run_out_of_lives()
        {
            var game = new Game(3, 3, 1, new OneMineMapBuilder(3, 3).Build(), new BoardPosition(1, 0));
            var livesRunOut = false;
            game.LivesRunOut += (sender, arguments) => livesRunOut = true;
            game.AdvanceGame(MoveDirection.Up);

            var expected = new List<string>() {"---",
                "-M-",
                "---"};
            expected.Reverse();
            Assert.AreEqual(expected, game.GetTextualRepresentation());
            Assert.IsTrue(livesRunOut);
        }
    }
}

using System;
using System.Collections.Generic;
using GameLogic;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class GameBoardTest
    {
        public const int rows = 8;
        public const int cols = 8; 

        [Test]
        [TestCase(4, 4, MoveDirection.Down)]
        [TestCase(4, 4, MoveDirection.Up)]
        [TestCase(4, 4, MoveDirection.Right)]
        [TestCase(4, 4, MoveDirection.Left)]
        [TestCase(0, 1, MoveDirection.Down)]
        [TestCase(0, 0, MoveDirection.Up)]
        [TestCase(0, 0, MoveDirection.Right)]
        [TestCase(0, 6, MoveDirection.Up)]
        [TestCase(7, 7, MoveDirection.Left)]
        [TestCase(7, 7, MoveDirection.Down)]
        [TestCase(0, 7, MoveDirection.Down)]
        [TestCase(0, 7, MoveDirection.Right)]
        [TestCase(7, 0, MoveDirection.Up)]
        [TestCase(7, 0, MoveDirection.Left)]
        [TestCase(7, 7, MoveDirection.Down)]
        [TestCase(7, 7, MoveDirection.Left)]
        
        public void test_that_position_in_the_Middle_of_Chessboard_Is_available_for_all_moves(int column, int row, MoveDirection moveDirection)
        {
            var board = new Board(cols, rows, new List<Tuple<int, int>>());
            var actual = board.IsMoveAvailable(new BoardPosition(column, row), moveDirection);
            Assert.IsTrue(actual);
        }


        [Test]
        [TestCase(0, 0, MoveDirection.Down)]
        [TestCase(0, 0, MoveDirection.Left)]
        [TestCase(7, 7, MoveDirection.Right)]
        [TestCase(7, 7, MoveDirection.Up)]
        [TestCase(0, 7, MoveDirection.Up)]
        [TestCase(0, 7, MoveDirection.Left)]
        [TestCase(7, 0, MoveDirection.Down)]
        [TestCase(7, 0, MoveDirection.Right)]
        [TestCase(7, 7, MoveDirection.Up)]
        [TestCase(7, 7, MoveDirection.Right)]


        public void test_that_intended_move_is_not_available(int column, int row, MoveDirection moveDirection)
        {

            var board = new Board(rows, cols, new List<Tuple<int, int>>());
            var actual = board.IsMoveAvailable(new BoardPosition(column, row), moveDirection);
            Assert.False(actual);
        }

        [Test]
        public void test_position_after_move_up()
        {
            var expected = new BoardPosition(0,0);
            var actual = BoardUtil.GetNewBoardPosition(new BoardPosition(0, 0), MoveDirection.Up);
            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y + 1, actual.Y); 
        }
        
        [Test]
        public void test_position_after_move_down()
        {
            var expected = new BoardPosition(0, 0);
            var actual = BoardUtil.GetNewBoardPosition(new BoardPosition(0, 0), MoveDirection.Down);
            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y - 1, actual.Y);
        }

        [Test]
        public void test_position_after_move_left()
        {
            var expected = new BoardPosition(0, 0);
            var actual = BoardUtil.GetNewBoardPosition(new BoardPosition(0, 0), MoveDirection.Left);
            Assert.AreEqual(expected.X - 1, actual.X);
            Assert.AreEqual(expected.Y , actual.Y);
        }

        [Test]
        public void test_position_after_move_right()
        {
            var expected = new BoardPosition(0, 0);
            var actual = BoardUtil.GetNewBoardPosition(new BoardPosition(0, 0), MoveDirection.Right);
            Assert.AreEqual(expected.X + 1, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
        }


        [Test]
        [TestCase(0,7)]
        [TestCase(5, 7)]
        [TestCase(7, 7)]
        public void test_that_goal_is_reached(int column, int row)
        {
            var board = new Board(cols, rows, new List<Tuple<int, int>>());
            var actual = board.IsGoalReached(new BoardPosition(column, row));
            Assert.IsTrue(actual);
        }

        [Test]
        [TestCase(0, 0, "A1")]
        [TestCase(5, 7,"F8")]
        [TestCase(7, 7, "H8")]
        [TestCase(50, 7, "51 - 8")]

        public void test_chess_notation(int column, int row, string expected)
        {
            var actual = BoardUtil.GetChessNotation(new BoardPosition(column, row));
            Assert.AreEqual(expected, actual);
        }
    }
}

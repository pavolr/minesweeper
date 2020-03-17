using System;
using System.Collections.Generic;
using GameLogic;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class BoardBuilderTest
    {
        [Test]
        public void test_board_builder()
        {
            var board  = new BoardBuilder(3,3, new AllMinesMapBuilder(3,3).Build()).Build();
            
            Assert.AreEqual(3, board.GetLength(0));
            Assert.AreEqual(3, board.GetLength(1));
        }
    }
}
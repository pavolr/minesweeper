using GameLogic;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class MapBuilderTest
    {
        [Test]
        public void test_that_random_map_builder_produces_some_data()
        {
            var map = new RandomMapBuilder(8,8).Build();
            Assert.LessOrEqual(1,map.Count); 
        }

        [Test]
        public void test_that_all_map_builder_produces_all_data()
        {
            var map = new AllMinesMapBuilder(8, 8).Build();
            Assert.AreEqual(64, map.Count);
        }
    }
}
using System;
using System.Collections.Generic;

namespace GameLogic
{
   public class OneMineMapBuilder : IMineMapBuilder
    {
        public OneMineMapBuilder(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
        }

        public int Rows { get; }
        public int Columns { get; }

        public List<Tuple<int, int>> Build()
        {
            return new List<Tuple<int, int>> {new Tuple<int, int>(1, 1)};
        }
    }
}

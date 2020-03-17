using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class AllMinesMapBuilder : IMineMapBuilder
    {
        public AllMinesMapBuilder(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
        }

        public int Rows { get; }
        public int Columns { get; }

        public List<Tuple<int, int>> Build()
        {
            var result = new List<Tuple<int,int>>();
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)

                {
                    result.Add(new Tuple<int, int>(x, y));
                }
            }
            return result;
        }
    }
}

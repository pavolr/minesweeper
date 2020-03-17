using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class RandomMapBuilder:IMineMapBuilder
    {
        public RandomMapBuilder(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;
        }

        public int Rows { get; }
        public int Columns { get;}
        
        public List<Tuple<int, int>> Build()
        {
            var result = new List<Tuple<int, int>>();
            var rnd = new Random();
            var maxMinePercentage = 50;

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (rnd.Next(100) < maxMinePercentage)
                    {
                        result.Add(new Tuple<int, int>(x,y));
                    }
                }
            }
            return result; 
        }
    }
}

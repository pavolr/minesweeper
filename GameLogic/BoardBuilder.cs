using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class BoardBuilder
    {
        private int _numberOfRows;
        private int _numberOfColumns;
        private List<Tuple<int, int>> _mineMap;

        public BoardBuilder(int columns, int rows, List<Tuple<int,int>> mapMines)
        {
            _numberOfColumns = columns;
            _numberOfRows = rows;
            _mineMap = mapMines; 
        }

        public Field[,] Build()
        {
            return MapMines(_mineMap, ConstructBoard());
        }

        private Field[,] ConstructBoard()
        {
            var board = new Field[_numberOfColumns, _numberOfRows];

            for (int x = 0; x < _numberOfColumns; x++)
            {
                for (int y = 0; y < _numberOfRows; y++)
                {
                    board[x, y] = new Field(false);
                }
            }

            return board; 
        }

        private static Field[,] MapMines(List<Tuple<int,int>> mineMap, Field[,] board )
        {
            foreach (var coordinate in mineMap)
            {
                board[coordinate.Item1, coordinate.Item2].IsMine = true;
            }

            return board;
        }
    }
}

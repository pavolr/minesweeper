namespace GameLogic
{
    public class BoardPosition
    {
        public int X { get; }        
        public int Y { get; }

        public BoardPosition(int column, int row)
        {
            X = column;
            Y = row;
        }
    }
}
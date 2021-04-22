namespace Сhessmen
{
    public class Variant
    {
        public int Row { get; }
        public int Column { get; }

        public Variant(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
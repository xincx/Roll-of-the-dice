using System;

namespace Tomino
{
    public struct Position
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(Position other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override string ToString()
        {
            return "(" + Row.ToString() + ", " + Column.ToString() + ")";
        }

        public bool IsNeighborOf(Position other)
        {
            int dx = Math.Abs(Row - other.Row);
            int dy = Math.Abs(Column - other.Column);
            return (dx == 0 && dy == 1) || (dy == 0 && dx == 1);
        }
    }
}

namespace main;

public struct Position
{
    public int Column { get; }
    public int Row { get; }

    public Position(int column, int row)
    {
        Column = column;
        Row = row;
    }
}

namespace main;

public class Maze
{
    private readonly char[,] _maze;
    private Position _mousePosition;
    private Position _cheesePosition;

    public Maze(char[,] maze)
    {
        _maze = maze;
    }

    public void FindCheese()
    {
        FindMouseAndCheese();
        MoveUp(_mousePosition);
        MoveRight(_mousePosition);
        MoveDown(_mousePosition);
        MoveLeft(_mousePosition);
    }

    private void MoveUp(Position currentPosition)
    {
        int indexUpRow = currentPosition.Line - 1;
        char valueUpRow = _maze[indexUpRow, currentPosition.Column];

        bool canMoveUp = true;
        if (indexUpRow < 0) canMoveUp = false;
        else if (valueUpRow is Constants.Wall or Constants.Mouse) canMoveUp = false;

        Position nextPosition = new Position(currentPosition.Column, currentPosition.Line - 1);

        if (canMoveUp)
        {
            Console.WriteLine($"Moved to up - from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");

            CheckCheese(nextPosition);

            MoveUp(nextPosition);
            MoveRight(nextPosition);
            MoveLeft(nextPosition);
        }
        else
        {
            Console.WriteLine($"* Impossible move to up from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");
        }
    }

    private void MoveRight(Position currentPosition)
    {
        int indexRightColumn = currentPosition.Column + 1;
        char valueRightColumn = _maze[currentPosition.Line, indexRightColumn];

        bool canMoveRight = true;
        if (indexRightColumn >= _maze.GetLength(Dimension.Column)) canMoveRight = false;
        else if (valueRightColumn is Constants.Wall or Constants.Mouse) canMoveRight = false;

        Position nextPosition = new Position(currentPosition.Column + 1, currentPosition.Line);

        if (canMoveRight)
        {
            Console.WriteLine($"Moved to right - from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");

            CheckCheese(nextPosition);

            MoveUp(nextPosition);
            MoveRight(nextPosition);
            MoveDown(nextPosition);
        }
        else
        {
            Console.WriteLine($"* Impossible move to right from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");
        }
    }

    private void MoveDown(Position currentPosition)
    {
        int indexDownRow = currentPosition.Line + 1;
        char valueDownRow = _maze[indexDownRow, currentPosition.Column];

        bool canMoveDown = true;
        if (indexDownRow >= _maze.GetLength(Dimension.Row)) canMoveDown = false;
        else if (valueDownRow is Constants.Wall or Constants.Mouse) canMoveDown = false;

        Position nextPosition = new Position(currentPosition.Column, currentPosition.Line + 1);

        if (canMoveDown)
        {
            Console.WriteLine($"Moved to down - from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");

            CheckCheese(nextPosition);

            MoveRight(nextPosition);
            MoveDown(nextPosition);
            MoveLeft(nextPosition);
        }
        else
        {
            Console.WriteLine($"* Impossible move to down from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");
        }
    }

    private void MoveLeft(Position currentPosition)
    {
        int indexLeftColumn = currentPosition.Column - 1;
        char valueLeftColumn = _maze[currentPosition.Line, indexLeftColumn];

        bool canMoveLeft = true;
        if (indexLeftColumn < 0) canMoveLeft = false;
        else if (valueLeftColumn is Constants.Wall or Constants.Mouse) canMoveLeft = false;

        Position nextPosition = new Position(currentPosition.Column - 1, currentPosition.Line);

        if (canMoveLeft)
        {
            Console.WriteLine($"Moved to left - from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");

            CheckCheese(nextPosition);

            MoveUp(nextPosition);
            MoveDown(nextPosition);
            MoveLeft(nextPosition);
        }
        else
        {
            Console.WriteLine($"* Impossible move to left from [{currentPosition.Column},{currentPosition.Line}] to [{nextPosition.Column},{nextPosition.Line}]");
        }
    }

    private void CheckCheese(Position currentPosition)
    {
        char valueCurrentPosition = _maze[currentPosition.Line, currentPosition.Column];
        if (valueCurrentPosition is Constants.Cheese)
        {
            Console.WriteLine($"\t-> Cheese Found... [{currentPosition.Column},{currentPosition.Line}]");
        }
    }

    private void FindMouseAndCheese()
    {
        for (int l = 0; l < _maze.GetLength(0); l++)
        {
            for (int c = 0; c < _maze.GetLength(1); c++)
            {
                char character = _maze[l, c];

                switch (character)
                {
                    case Constants.Mouse:
                        _mousePosition = new Position(c, l);
                        break;
                    case Constants.Cheese:
                        _cheesePosition = new Position(c, l);
                        break;
                }
            }
        }
    }

    public void Print()
    {
        Console.WriteLine();
        Console.WriteLine();

        for (int l = 0; l < _maze.GetLength(0); l++)
        {
            Console.Write("\t\t");
            for (int c = 0; c < _maze.GetLength(1) -1; c++)
            {
                Console.Write($"{_maze[l, c]}, ");
            }
            Console.Write($"{_maze[l, _maze.GetLength(Dimension.Column) - 1]}");
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine($"\t\tMouse: [{_mousePosition.Column},{_mousePosition.Line}]");
        Console.WriteLine($"\t\tCheese: [{_cheesePosition.Column},{_mousePosition.Line}]");
        Console.WriteLine();
        Console.WriteLine();
    }
}

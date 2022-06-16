namespace main;

public class Maze
{
    private readonly char[,] _maze;
    private Pile _path;
    private List _pathHappy;
    private Position _mousePosition;
    private Position _cheesePosition;
    private bool _cheeseFound;

    public Maze(char[,] maze)
    {
        _maze = maze;
        _path = new Pile();
        _pathHappy = new List();
    }

    public void FindCheese()
    {
        FindMouseAndCheese();
        _path.Add(_mousePosition);

        Console.WriteLine("Starting search for cheese...");
        if (_cheeseFound == false) MoveUp(_mousePosition);
        if (_cheeseFound == false) MoveRight(_mousePosition);
        if (_cheeseFound == false) MoveDown(_mousePosition);
        if (_cheeseFound == false) MoveLeft(_mousePosition);

        if(_cheeseFound == false)
        {
            Console.WriteLine();
            Console.WriteLine("\tNo cheese found!!!");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tCheese found!!!");
            Console.WriteLine("\tPath to cheese (happy): ");
            _pathHappy.Show();
            Console.WriteLine();
        }
    }

    private void MoveUp(Position currentPosition)
    {
        int indexUpRow = currentPosition.Row - 1;
        char valueUpRow = _maze[indexUpRow, currentPosition.Column];

        bool canMoveUp = true;
        if (indexUpRow < 0) canMoveUp = false;
        else if (valueUpRow is Constants.Wall or Constants.Mouse) canMoveUp = false;

        Position nextPosition = new Position(currentPosition.Column, currentPosition.Row - 1);
        if (_path.Isset(nextPosition)) canMoveUp = false;
        _path.Add(nextPosition);


        if (canMoveUp)
        {
            Console.WriteLine($"→ Moved to up \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");

            CheckCheese(nextPosition);

            if (_cheeseFound == false) MoveUp(nextPosition);
            if (_cheeseFound == false) MoveRight(nextPosition);
            if (_cheeseFound == false) MoveLeft(nextPosition);
        }
        else
        {
            // Console.WriteLine($"* Impossible move to up from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");
            _path.Remove();
        }
    }

    private void MoveRight(Position currentPosition)
    {
        bool canMoveRight = true;

        int indexRightColumn = currentPosition.Column + 1;
        if (indexRightColumn >= _maze.GetLength(Dimension.Column)) canMoveRight = false;

        char valueRightColumn = _maze[currentPosition.Row, indexRightColumn];
        if (valueRightColumn is Constants.Wall or Constants.Mouse) canMoveRight = false;

        Position nextPosition = new Position(currentPosition.Column + 1, currentPosition.Row);
        if (_path.Isset(nextPosition)) canMoveRight = false;
        _path.Add(nextPosition);


        if (canMoveRight)
        {
            Console.WriteLine($"→ Moved to right \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");

            CheckCheese(nextPosition);

            if (_cheeseFound == false) MoveUp(nextPosition);
            if (_cheeseFound == false) MoveRight(nextPosition);
            if (_cheeseFound == false) MoveDown(nextPosition);
        }
        else
        {
            // Console.WriteLine($"* Impossible move to right from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");
            _path.Remove();
        }
    }

    private void MoveDown(Position currentPosition)
    {
        int indexDownRow = currentPosition.Row + 1;
        char valueDownRow = _maze[indexDownRow, currentPosition.Column];

        bool canMoveDown = true;
        if (indexDownRow >= _maze.GetLength(Dimension.Row)) canMoveDown = false;
        else if (valueDownRow is Constants.Wall or Constants.Mouse) canMoveDown = false;

        Position nextPosition = new Position(currentPosition.Column, currentPosition.Row + 1);
        if (_path.Isset(nextPosition)) canMoveDown = false;
        _path.Add(nextPosition);


        if (canMoveDown)
        {
            Console.WriteLine($"→ Moved to down \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");

            CheckCheese(nextPosition);

            if (_cheeseFound == false) MoveRight(nextPosition);
            if (_cheeseFound == false) MoveDown(nextPosition);
            if (_cheeseFound == false) MoveLeft(nextPosition);
        }
        else
        {
            // Console.WriteLine($"* Impossible move to down from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");
            _path.Remove();
        }
    }

    private void MoveLeft(Position currentPosition)
    {
        int indexLeftColumn = currentPosition.Column - 1;
        char valueLeftColumn = _maze[currentPosition.Row, indexLeftColumn];

        bool canMoveLeft = true;
        if (indexLeftColumn < 0) canMoveLeft = false;
        else if (valueLeftColumn is Constants.Wall or Constants.Mouse) canMoveLeft = false;

        Position nextPosition = new Position(currentPosition.Column - 1, currentPosition.Row);
        if (_path.Isset(nextPosition)) canMoveLeft = false;
        _path.Add(nextPosition);


        if (canMoveLeft)
        {
            Console.WriteLine($"→ Moved to left: \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");

            CheckCheese(nextPosition);

            if (_cheeseFound == false) MoveUp(nextPosition);
            if (_cheeseFound == false) MoveDown(nextPosition);
            if (_cheeseFound == false) MoveLeft(nextPosition);
        }
        else
        {
            // Console.WriteLine($"* Impossible move to left from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");
            _path.Remove();
        }
    }

    private void CheckCheese(Position currentPosition)
    {
        char valueCurrentPosition = _maze[currentPosition.Row, currentPosition.Column];
        if (valueCurrentPosition is not Constants.Cheese) return;
        CheeseFound();
    }

    private void CheeseFound()
    {
        _cheeseFound = true;

        Position lastRemovedPosition = default;
        while (lastRemovedPosition.Column != _mousePosition.Column || lastRemovedPosition.Row != _mousePosition.Row)
        {
            lastRemovedPosition = _path.Remove();
            _pathHappy.Prepend(lastRemovedPosition);
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

        FindMouseAndCheese();

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
        Console.WriteLine($"\t\tMouse:  [{_mousePosition.Column},{_mousePosition.Row}]");
        Console.WriteLine($"\t\tCheese: [{_cheesePosition.Column},{_mousePosition.Row}]");
        Console.WriteLine();
        Console.WriteLine();
    }
}

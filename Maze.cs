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

    private bool MoveUp(Position currentPosition)
    {
        bool canMoveUp = true;
        bool canMoveRight = true;
        bool canMoveLeft = true;
        Position nextPosition = new Position(currentPosition.Column, currentPosition.Row - 1);

        int indexUpRow = currentPosition.Row - 1;
        char valueUpRow = _maze[indexUpRow, currentPosition.Column];

        if (valueUpRow is Constants.Wall or Constants.Mouse) return false;
        if (_path.Isset(nextPosition)) return false;
        _path.Add(nextPosition);
        Console.WriteLine($"→ Moved to up \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");

        CheckCheese(nextPosition);
        if (_cheeseFound == false) canMoveUp = MoveUp(nextPosition);
        if (_cheeseFound == false) canMoveRight = MoveRight(nextPosition);
        if (_cheeseFound == false) canMoveLeft = MoveLeft(nextPosition);

        if (canMoveUp == false && canMoveRight == false && canMoveLeft == false)
        {
            _path.Remove();
            return false;
        }

        return canMoveUp || canMoveRight || canMoveLeft;
    }

    private bool MoveRight(Position currentPosition)
    {
        bool canMoveUp = true;
        bool canMoveRight = true;
        bool canMoveDown = true;
        Position nextPosition = new Position(currentPosition.Column + 1, currentPosition.Row);

        int indexRightColumn = currentPosition.Column + 1;
        char valueRightColumn = _maze[currentPosition.Row, indexRightColumn];

        if (valueRightColumn is Constants.Wall or Constants.Mouse) return false;
        if (_path.Isset(nextPosition)) return false;
        _path.Add(nextPosition);
        Console.WriteLine($"→ Moved to right \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");

        CheckCheese(nextPosition);
        if (_cheeseFound == false) canMoveUp = MoveUp(nextPosition);
        if (_cheeseFound == false) canMoveRight = MoveRight(nextPosition);
        if (_cheeseFound == false) canMoveDown = MoveDown(nextPosition);

        if (canMoveUp == false && canMoveRight == false && canMoveDown == false)
        {
            _path.Remove();
            return false;
        }

        return canMoveUp || canMoveRight || canMoveDown;
    }

    private bool MoveDown(Position currentPosition)
    {
        bool canMoveRight = true;
        bool canMoveDown = true;
        bool canMoveLeft = true;
        Position nextPosition = new Position(currentPosition.Column, currentPosition.Row + 1);

        int indexDownRow = currentPosition.Row + 1;
        char valueDownRow = _maze[indexDownRow, currentPosition.Column];

        if (valueDownRow is Constants.Wall or Constants.Mouse) return false;
        if (_path.Isset(nextPosition)) return false;
        _path.Add(nextPosition);
        Console.WriteLine($"→ Moved to down \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");


        CheckCheese(nextPosition);
        if (_cheeseFound == false) canMoveRight = MoveRight(nextPosition);
        if (_cheeseFound == false) canMoveDown = MoveDown(nextPosition);
        if (_cheeseFound == false) canMoveLeft = MoveLeft(nextPosition);

        if (canMoveRight == false && canMoveDown == false && canMoveLeft == false)
        {
            _path.Remove();
            return false;
        }

        return canMoveRight || canMoveDown || canMoveLeft;
    }

    private bool MoveLeft(Position currentPosition)
    {
        bool canMoveUp = true;
        bool canMoveDown = true;
        bool canMoveLeft = true;
        Position nextPosition = new Position(currentPosition.Column - 1, currentPosition.Row);

        int indexLeftColumn = currentPosition.Column - 1;
        char valueLeftColumn = _maze[currentPosition.Row, indexLeftColumn];

        if (valueLeftColumn is Constants.Wall or Constants.Mouse) return false;
        if (_path.Isset(nextPosition)) return false;
        _path.Add(nextPosition);
        Console.WriteLine($"→ Moved to left: \t from [{currentPosition.Column},{currentPosition.Row}] to [{nextPosition.Column},{nextPosition.Row}]");


        CheckCheese(nextPosition);
        if (_cheeseFound == false) canMoveUp = MoveUp(nextPosition);
        if (_cheeseFound == false) canMoveDown = MoveDown(nextPosition);
        if (_cheeseFound == false) canMoveLeft = MoveLeft(nextPosition);

        if (canMoveUp == false && canMoveDown == false && canMoveLeft == false)
        {
            _path.Remove();
            return false;
        }

        return canMoveUp || canMoveDown || canMoveLeft;
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

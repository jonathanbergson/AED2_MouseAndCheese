namespace main;

public static class Program
{
    private static void Main()
    {
        char[,] mazeStatic = Generator.MazeStatic();

        Maze maze = new Maze(mazeStatic);
        maze.Print();
        maze.FindCheese();
    }
}

namespace main;

public static class Program
{
    private static void Main()
    {
        char[,] mazeGenerated = Generator.MazeWrongPathStatic();

        Maze maze = new Maze(mazeGenerated);
        maze.Print();
        maze.FindCheese();
    }
}

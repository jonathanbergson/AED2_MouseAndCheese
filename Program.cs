namespace main
{
    public static class Program
    {
        private static void Main()
        {
            Generator.MazeMakeFile();
            char[,] generated = Generator.MazeReadFile();
            Maze maze = new Maze(generated);
            maze.Print();
            maze.FindCheese();
        }
    }
}

using System;
using System.IO;

namespace main
{
    internal static class Generator
    {
        public static char[,] MazeStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '0', '1', '0', '1'},
                {'1', '0', '0', '0', '1', '1'},
                {'1', '0', '1', '0', '0', '1'},
                {'1', '0', '1', '0', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeUpStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'Q', '1', '1', '1', '1'},
                {'1', '0', '1', '1', '1', '1'},
                {'1', '0', '1', '1', '1', '1'},
                {'1', 'R', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeRightStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '0', '0', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeDownStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '1', '1', '1', '1'},
                {'1', '0', '1', '1', '1', '1'},
                {'1', '0', '1', '1', '1', '1'},
                {'1', 'Q', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeLeftStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'Q', '0', '0', 'R', '1'},
                {'1', '1', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeRightDownStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '0', '1', '1', '1'},
                {'1', '1', '0', '0', '1', '1'},
                {'1', '1', '1', '0', '0', '1'},
                {'1', '1', '1', '1', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeLoopStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '0', '0', '1', '1'},
                {'1', '1', '0', '0', '1', '1'},
                {'1', '1', '1', '0', '0', '1'},
                {'1', '1', '1', '1', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeTwoPathStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '0', '0', '0', '1'},
                {'1', '1', '0', '1', '0', '1'},
                {'1', '1', '0', '1', '0', '1'},
                {'1', '1', '0', '0', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeWrongPathStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '1', '1', '1', '1'},
                {'1', '0', '0', '0', '0', '1'},
                {'1', '1', '0', '1', '1', '1'},
                {'1', '1', '0', '0', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static char[,] MazeNotFoundStatic()
        {
            return new[,]
            {
                {'1', '1', '1', '1', '1', '1'},
                {'1', 'R', '0', '0', '0', '1'},
                {'1', '1', '0', '1', '0', '1'},
                {'1', '1', '0', '1', '1', '1'},
                {'1', '1', '0', '1', 'Q', '1'},
                {'1', '1', '1', '1', '1', '1'},
            };
        }

        public static void MazeMakeFile(string fileName = "maze.txt", int mazeColumns = 8, int mazeRows = 8)
        {
            try
            {
                Console.WriteLine("TRABALHO PR??TICO - Rato no Labirinto - GERADOR DE ARQUIVO");

                String arquivoEscrita;
                Console.Write("Digite nome do arquivo de entrada >> ");
                arquivoEscrita = fileName; //Console.ReadLine();

                int numeroLinhas, numeroColunas;
                Console.Write("Digite numero de linhas do labirinto >> ");
                numeroLinhas = mazeRows; // Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite numero de colunas do labirinto >> ");
                numeroColunas = mazeColumns; //Convert.ToInt32(Console.ReadLine());


                if (numeroLinhas <= 0 || numeroColunas <= 0)
                {
                    Console.WriteLine("Entrada inv??lida! O programa vai adotar valores padrao!");
                    numeroLinhas = 10;
                    numeroColunas = 10;
                }

                Random gerador = new Random();
                int ratoX = (Math.Abs(gerador.Next()) % (numeroColunas - 2)) + 1;
                int ratoY = (Math.Abs(gerador.Next()) % (numeroLinhas - 2)) + 1;
                int queijoX = (Math.Abs(gerador.Next()) % (numeroColunas - 2)) + 1;
                int queijoY = (Math.Abs(gerador.Next()) % (numeroLinhas - 2)) + 1;

                using (StreamWriter escritor = new StreamWriter(arquivoEscrita))
                {
                    escritor.WriteLine(numeroLinhas + " " + numeroColunas);
                    escritor.WriteLine(ratoX + " " + ratoY);
                    escritor.WriteLine(queijoX + " " + queijoY);
                    escritor.Flush();

                    for (int i = 0; i < numeroLinhas; i++)
                    {
                        for (int j = 0; j < numeroColunas; j++)
                        {
                            if (i == 0 || j == 0 || i == (numeroLinhas - 1) || j == (numeroColunas - 1))
                            {
                                escritor.Write("1");
                            }
                            else if ((j == ratoX && i == ratoY) || (j == queijoX && i == queijoY))
                            {
                                escritor.Write("*");
                            }
                            else
                            {
                                int simbolo = Math.Abs(gerador.Next()) % 2; // 0 ou 1
                                escritor.Write(simbolo + "");
                            }
                        }

                        escritor.WriteLine();
                        escritor.Flush();
                    }
                }

            }
            catch (Exception excecao)
            {
                Console.WriteLine(excecao.Message);
            }
        }

        public static char[,] MazeReadFile(string fileName = "maze.txt")
        {
            char[,] maze = { };
            int[] mousePosition = Array.Empty<int>();
            int[] cheesePosition = Array.Empty<int>();

            try
            {
                using (StreamReader stream = new StreamReader(fileName))
                {
                    int lineNumber = 0;
                    int mazeLine = 0;

                    while (stream.ReadLine() is { } line)
                    {
                        switch (lineNumber)
                        {
                            case 0:
                            {
                                string[] mazeSize = line.Split(' ');
                                int column = Convert.ToInt32(mazeSize[Dimension.Column]);
                                int row = Convert.ToInt32(mazeSize[Dimension.Row]);
                                maze = new char[column, row];
                                break;
                            }
                            case 1:
                            {
                                string[] position = line.Split(' ');
                                int column = Convert.ToInt32(position[Dimension.Column]);
                                int row = Convert.ToInt32(position[Dimension.Row]);

                                mousePosition = new[] {column, row};
                                break;
                            }
                            case 2:
                            {
                                string[] position = line.Split(' ');
                                int column = Convert.ToInt32(position[Dimension.Column]);
                                int row = Convert.ToInt32(position[Dimension.Row]);

                                cheesePosition = new[] {column, row};
                                break;
                            }
                            default:
                            {
                                for (int i = 0; i < line.Length; i++)
                                {
                                    maze[mazeLine, i] = line[i];
                                }

                                mazeLine++;
                                break;
                            }
                        }

                        lineNumber++;
                    }
                }

                maze[mousePosition[0], mousePosition[1]] = Constants.Mouse;
                maze[cheesePosition[0], cheesePosition[1]] = Constants.Cheese;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return maze;
        }
    }
}

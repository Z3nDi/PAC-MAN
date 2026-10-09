using System;
using System.Threading;
using System.Text;

namespace SimplePacMan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

        char[,] map = {
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                { '#', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '#' },
                { '#', '.', '#', '.', '#', '.', '#', '#', '#', '#', '.', '#' },
                { '#', '.', '#', '.', '.', '.', '.', '.', '.', '#', '.', '#' },
                { '#', '.', '.', '.', '#', '#', '#', '#', '.', '#', '.', '#' },
                { '#', '.', '#', '.', '.', '.', '.', '#', '.', '.', '.', '#' },
                { '#', '.', '#', '#', '#', '#', '.', '#', '#', '#', '.', '#' },
                { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
     
        };

            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;

            int maxScore = 0;
            foreach (char c in map) if (c == '.') maxScore += 10;

            int ghostX = 10;
            int ghostY = 7;
            Random random = new Random();

            bool isGameOver = false;

            while (!isGameOver)
            {
                Console.SetCursorPosition(0, 0);

                // 1. КАРТА ТА ПЕРСОНАЖІ
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    for (int x = 0; x < map.GetLength(1); x++)
                    {
                        if (x == pacmanX && y == pacmanY)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write('C');
                        }
                        else if (x == ghostX && y == ghostY)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('G');
                        }
                        else
                        {
                            switch (map[y, x])
                            {
                                case '#':
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case '.':
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                            }
                            Console.Write(map[y, x]);
                        }
                    }
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nРахунок: {score} / {maxScore}");

                // 2.  ПЕРЕМОГА ТА ПРОГРАШ
                if (pacmanX == ghostX && pacmanY == ghostY)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nГРА ЗАКІНЧЕНА!");
                    isGameOver = true;
                    break;
                }

                if (score == maxScore)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nВІТАЄМО! Ви перемогли!");
                    isGameOver = true;
                    break;
                }

                // 3. ОБРОБКА КРОКІВ ПЕКМАНА
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);

                    int nextPacmanX = pacmanX;
                    int nextPacmanY = pacmanY;

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            nextPacmanY--;
                            break;
                        case ConsoleKey.DownArrow:
                            nextPacmanY++;
                            break;
                        case ConsoleKey.LeftArrow:
                            nextPacmanX--;
                            break;
                        case ConsoleKey.RightArrow:
                            nextPacmanX++;
                            break;
                    }

                    if (map[nextPacmanY, nextPacmanX] != '#')
                    {
                        pacmanX = nextPacmanX;
                        pacmanY = nextPacmanY;

                        if (map[pacmanY, pacmanX] == '.')
                        {
                            score += 10;
                            map[pacmanY, pacmanX] = ' ';
                        }
                    }
                }

                // 4. РУХ ПРИВИДА
                int nextGhostX = ghostX;
                int nextGhostY = ghostY;
                int direction = random.Next(4);
                switch (direction)
                {
                    case 0: nextGhostY--; break;
                    case 1: nextGhostY++; break;
                    case 2: nextGhostX--; break;
                    case 3: nextGhostX++; break;
                }

                if (map[nextGhostY, nextGhostX] != '#')
                {
                    ghostX = nextGhostX;
                    ghostY = nextGhostY;
                }

                // 5. ШВИДКІСТЬ ГРИ
                Thread.Sleep(150);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}

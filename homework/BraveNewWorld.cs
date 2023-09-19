using System;

namespace homework
{
    internal static class BraveNewWorld
    {
        public static void Main1(string[] args)
        {
            int columns = 20;
            int rows = 5;

            char wall = '#';
            char empty = ' ';

            int playerPositionX = 1;
            int playerPositionY = 1;

            char player = '@';

            int finishPositionX = 1;
            int finishPositionY = 1;

            char finish = '$';

            Random random = new Random();

            Console.CursorVisible = false;

            char[,] map = CreateMap(columns, rows, wall, empty);

            while (finishPositionX == playerPositionX || finishPositionY == playerPositionX)
            {
                finishPositionX = random.Next(1, columns - 1);
                finishPositionY = random.Next(1, rows - 1);
            }

            ChangeElementInMap(map, finishPositionX, finishPositionY, finish);

            DrawFrame(map, playerPositionX, playerPositionY, player, finish);

            while (playerPositionX != finishPositionX || playerPositionY != finishPositionY)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                MovePlayer(pressedKey, map, wall, ref playerPositionX, ref playerPositionY);

                DrawFrame(map, playerPositionX, playerPositionY, player, finish);
            }

            Console.WriteLine();

            Console.WriteLine("Вы дошли до финиша!");
        }

        private static char[,] CreateMap(int sizeX, int sizeY, char wall, char empty)
        {
            char[,] map = new char[sizeY, sizeX];

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (i == 0 || i == sizeY - 1 || j == 0 || j == sizeX - 1)
                    {
                        map[i, j] = wall;
                    }
                    else
                    {
                        map[i, j] = empty;
                    }
                }
            }

            return map;
        }

        private static void DirectPlayer(ConsoleKeyInfo pressedKey, out int deltaPositionX,
            out int deltaPositionY)
        {
            const char CommandKeyCharUp = 'w';
            const char CommandKeyCharLeft = 'a';
            const char CommandKeyCharDown = 's';
            const char CommandKeyCharRight = 'd';

            deltaPositionX = 0;
            deltaPositionY = 0;

            switch (pressedKey.KeyChar)
            {
                case CommandKeyCharUp:
                    deltaPositionY = -1;
                    break;

                case CommandKeyCharLeft:
                    deltaPositionX = -1;
                    break;

                case CommandKeyCharDown:
                    deltaPositionY = 1;
                    break;

                case CommandKeyCharRight:
                    deltaPositionX = 1;
                    break;
            }
        }

        private static void MovePlayer(ConsoleKeyInfo pressedKey, char[,] map, char wall, ref int playerPositionX,
            ref int playerPositionY)
        {
            DirectPlayer(pressedKey, out int deltaPositionX, out int deltaPositionY);

            if (map[playerPositionY + deltaPositionY, playerPositionX + deltaPositionX] != wall)
            {
                playerPositionX += deltaPositionX;
                playerPositionY += deltaPositionY;
            }
        }

        private static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static void ChangeElementInMap(char[,] map, int positionX, int positionY, char symbol)
        {
            if (positionX >= 0 && positionY >= 0 && positionX < map.GetLength(1) - 1 &&
                positionY < map.GetLength(0) - 1)
            {
                map[positionY, positionX] = symbol;
            }
        }

        private static void DrawElement(int x, int y, char symbol, ConsoleColor color = ConsoleColor.Cyan)
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            Console.SetCursorPosition(x, y);
            Console.Write(symbol);

            Console.ForegroundColor = defaultColor;

            Console.SetCursorPosition(cursorX, cursorY);
        }

        private static void DrawFrame(char[,] map, int playerPositionX, int playerPositionY, char player, char finish)
        {
            Console.Clear();

            DrawMap(map);
            DrawElement(playerPositionX, playerPositionY, player);

            Console.WriteLine("Вы находитесь в ячейке ({0}, {1})", playerPositionX, playerPositionY);
            Console.WriteLine($"Финиш обозначен символом {finish}");
        }
    }
}
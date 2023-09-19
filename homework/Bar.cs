using System;

namespace homework
{
    internal static class Bar
    {
        public static void Main1(string[] args)
        {
            int manaPercent = 34;
            int maxMana = 15;

            DrawBar(manaPercent, maxMana, ConsoleColor.Blue, symbol: '_');

            Console.WriteLine();
        }

        private static void DrawBar(float percent, int barSize, ConsoleColor color = ConsoleColor.Red,
            int positionX = 0, int positionY = 0, char symbol = ' ')
        {
            ConsoleColor defaultColor = Console.BackgroundColor;

            string openBar = "[";
            string closeBar = "]";

            int fillPart = Convert.ToInt32(Math.Round(percent / 100f * barSize, 0));

            string bar = FillBar(fillPart, symbol);

            Console.SetCursorPosition(positionX, positionY);
            Console.Write(openBar);

            Console.BackgroundColor = color;
            Console.Write(bar);

            Console.BackgroundColor = defaultColor;

            bar = FillBar(barSize - fillPart, symbol);

            Console.Write(bar + closeBar);
        }

        private static string FillBar(int border, char symbol)
        {
            string bar = string.Empty;

            for (int i = 0; i < border; i++)
            {
                bar += symbol;
            }

            return bar;
        }
    }
}
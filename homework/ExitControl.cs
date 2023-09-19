using System;

namespace homework
{
    internal static class ExitControl
    {
        public static void Main1(string[] args)
        {
            string stopMessage = "exit";
            string message = "";
            
            while (message != null && message.ToLower() != stopMessage)
            {
                message = Console.ReadLine();
            }
        }
    }
}
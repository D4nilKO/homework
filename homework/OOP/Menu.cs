// using System;
// using System.Collections.Generic;
// //Это шаблон консольного меню для удобства копирования
// namespace homework.OOP
// {
//     {
//     internal static class Program
//     {
//         public static void Main(string[] args)
//         {
//             const string Command = "1";
//             const string Command = "2";
//             const string Command = "3";
//             const string Command = "4";
//             const string Command = "5";
//             const string CommandExit = "Exit";
//
//             Dictionary<string, string> actionsByCommand = new()
//             {
//                 { Command, "" },
//                 { Command, "" },
//                 { Command, "" },
//                 { Command, "" },
//                 { Command, "" },
//                 { CommandExit, "Выйти из программы" }
//             };
//
//             bool isContinue = true;
//
//             while (isContinue)
//             {
//                 Console.Clear();
//                 Console.WriteLine();
//                 Console.WriteLine("\nМеню:");
//
//                 foreach (KeyValuePair<string, string> option in actionsByCommand)
//                 {
//                     Console.WriteLine($"{option.Key} - {option.Value}");
//                 }
//
//                 Console.Write("\nВыберете необходимую операцию: ");
//                 string desiredOperation = Console.ReadLine();
//                 Console.WriteLine();
//
//                 switch (desiredOperation)
//                 {
//                     case Command:
//
//                         break;
//
//                     case Command:
//
//                         break;
//
//                     case Command:
//
//                         break;
//
//                     case Command:
//
//                         break;
//
//                     case Command:
//
//                         break;
//
//
//                     case CommandExit:
//                         isContinue = false;
//                         Console.WriteLine("Выход...");
//                         break;
//
//                     default:
//                         Console.WriteLine("Неизвестная команда. Повторите ввод.");
//                         break;
//                 }
//
//                 Console.WriteLine("Нажмите любую клавишу для продолжения... ");
//                 Console.ReadKey();
//             }
//         }
//     }
//     }
// }
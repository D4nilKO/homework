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

class Client
{
    void Operation()
    {
        Prototype prototype = new ConcretePrototype1(1);
        Prototype clone = prototype.Clone();
        prototype = new ConcretePrototype2(2);
        clone = prototype.Clone();
    }
}

abstract class Prototype
{
    public int Id { get; private set; }

    public Prototype(int id)
    {
        this.Id = id;
    }

    public abstract Prototype Clone();
}

class ConcretePrototype1 : Prototype
{
    public ConcretePrototype1(int id) : base(id)
    {
    }

    public override Prototype Clone()
    {
        return new ConcretePrototype1(Id);
    }
}

class ConcretePrototype2 : Prototype
{
    public ConcretePrototype2(int id)
        : base(id)
    {
    }

    public override Prototype Clone()
    {
        return new ConcretePrototype2(Id);
    }
}
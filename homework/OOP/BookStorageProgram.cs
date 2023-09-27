using System;
using System.Collections.Generic;

namespace homework.OOP;

internal static class BookStorageProgram
{
    public static void Main1(string[] args)
    {
        const string CommandAddBook = "Add";
        const string CommandRemoveBook = "Remove";
        const string CommandFindBook = "Find";
        const string CommandExit = "Exit";

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandAddBook, "Добавить книгу" },
            { CommandRemoveBook, "Удалить книгу" },
            { CommandFindBook, "Найти книгу" },
            { CommandExit, "Выйти" }
        };

        BookStorage bookStorage = new();

        bool isContinue = true;

        while (isContinue)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\nМеню:");

            foreach (KeyValuePair<string, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write("Выберете необходимую операцию: ");
            string desiredOperation = Console.ReadLine();
            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandAddBook:
                    bookStorage.AddBook();
                    break;

                case CommandRemoveBook:
                    bookStorage.RemoveBook();
                    break;

                case CommandFindBook:
                    bookStorage.ShowBooksByParameter();
                    break;

                case CommandExit:
                    isContinue = false;
                    break;
            }
        }

        Console.WriteLine("Выход...");
    }
}

class BookStorage
{
    private List<Book> _storage = new();

    public void AddBook()
    {
        Console.Write("Ведите название книги: ");
        string title = Console.ReadLine();
        Console.WriteLine();

        Console.Write("Введите автора: ");
        string author = Console.ReadLine();
        Console.WriteLine();

        bool isContinue = true;

        while (isContinue)
        {
            Console.Write("Введите год выпуска: ");
            string releaseYearInput = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(releaseYearInput, out int releaseYear))
            {
                _storage.Add(new Book(title, author, releaseYear));

                Console.WriteLine("Книга успешно добавлена!");
                isContinue = false;
            }
            else
            {
                Console.Write("Ожидалось число, повторите ввод: ");
                Console.WriteLine();
            }
        }
    }

    public void RemoveBook()
    {
        bool isContinue = true;

        while (isContinue)
        {
            Console.Write("Введите номер книги, которую нужно удалить: ");
            string indexInput = Console.ReadLine();

            if (int.TryParse(indexInput, out int index))
            {
                _storage.RemoveAt(index);

                Console.WriteLine("Книга успешно добавлена!");
                isContinue = false;
            }
            else
            {
                Console.Write("Ожидалось число, повторите ввод: ");
                Console.WriteLine();
            }
        }
    }

    public void ViewAllBooks()
    {
        for (int i = 0; i < _storage.Count; i++)
        {
            var book = _storage[i];

            Console.Write($"(Книга№{i+1}) - ");
            book.View();
        }
    }

    public void ShowBooksByParameter()
    {
        const string CommandTitle = "Название";
        const string CommandAuthor = "Автор";
        const string CommandReleaseYear = "Год выпуска";
        const string CommandExit = "Выход";

        bool isContinue = true;

        while (isContinue)
        {
            Console.Write("Введите по какому параметру нужно найти книгу: ");
            string parameterInput = Console.ReadLine();

            switch (parameterInput)
            {
                case CommandTitle:

                    break;
                case CommandAuthor:

                    break;
                case CommandReleaseYear:

                    break;
                case CommandExit:
                    isContinue = false;
                    break;
            }
        }
    }

    private void FindBookByTitle()
    {
        string input = Console.ReadLine();
        
        if (TryGetBookByTitle(input, out Book book))
        {
            Console.WriteLine();
        }
    }

    private bool TryGetBookByTitle(string title, out Book book)
    {
        book = default;

        foreach (var element in _storage)
        {
            if (element.Title.Contains(title))
            {
                book = element;
                
                
                return true;
            }
        }

        Console.WriteLine("Книг с таким названием не найдено.");

        return false;
    }

    private bool TryGetBookByAuthor(string author, out Book book)
    {
        book = default;

        foreach (var element in _storage)
        {
            if (element.Author.Contains(author))
            {
                book = element;
                return true;
            }
        }

        Console.WriteLine("Книг с таким Автором не найдено.");

        return false;
    }

    private bool TryGetBookByReleaseYear(string releaseYear, out Book book)
    {
        book = default;

        foreach (var element in _storage)
        {
            if (element.ReleaseYear.ToString().Contains(releaseYear))
            {
                book = element;
                return true;
            }
        }

        Console.WriteLine("Книг с таким годом выпуска не найдено.");

        return false;
    }
}

class Book
{
    public Book(string title, string author, int releaseYear)
    {
        Title = title;
        Author = author;
        ReleaseYear = releaseYear;
    }

    public string Title { get; private set; }
    public string Author { get; private set; }
    public int ReleaseYear { get; private set; }

    public void View()
    {
        Console.Write($"{Title} | Автор - {Author} | Год выпуска - {ReleaseYear}\n");
    }
}
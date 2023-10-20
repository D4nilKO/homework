using System;
using System.Collections.Generic;

namespace homework.OOP.BookStorage
{
    internal static class Program
    {
        public static void Main1(string[] args)
        {
            const string CommandAddBook = "Add";
            const string CommandRemoveBook = "Remove";
            const string CommandFindBook = "Find";
            const string CommandViewAll = "View all";
            const string CommandExit = "Exit";

            Dictionary<string, string> actionsByCommand = new()
            {
                { CommandAddBook, "Добавить книгу" },
                { CommandRemoveBook, "Удалить книгу" },
                { CommandFindBook, "Найти книгу" },
                { CommandViewAll, "Показать все книги" },
                { CommandExit, "Выйти" }
            };

            BookStorage bookStorage = new();

            bool isContinue = true;

            while (isContinue)
            {
                Console.Clear();

                Console.WriteLine("Меню:");

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

                    case CommandViewAll:
                        bookStorage.ViewAllBooks();
                        break;

                    case CommandExit:
                        isContinue = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда, повторите ввод.");
                        break;
                }

                if (isContinue)
                {
                    Console.WriteLine("Для продолжения нажмите любую клавишу.");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Выход...");
        }
    }

    class BookStorage
    {
        private List<Book> _storage = new()
        {
            new Book("1", "1", 1),
            new Book("2", "1", 1),
            new Book("1", "2", 1),
            new Book("1", "1", 2),
            new Book("2", "2", 2),
            new Book("11", "11", 11),
            new Book("22", "22", 22),
        };

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

            ViewAllBooks();

            while (isContinue)
            {
                Console.Write("Введите ID книги, которую нужно удалить: ");
                string identifierInput = Console.ReadLine();

                if (TryGetBookByIdentifier(identifierInput, out Book book))
                {
                    _storage.Remove(book);

                    Console.WriteLine("Книга успешно удалена!");
                    isContinue = false;
                }
                else
                {
                    Console.Write("Книги с таким номером нет, повторите ввод: ");
                }

                Console.WriteLine();
            }
        }

        public void ShowBooksByParameter()
        {
            const string CommandTitle = "Title";
            const string CommandAuthor = "Author";
            const string CommandReleaseYear = "Release year";
            const string CommandExit = "Exit";

            Dictionary<string, string> actionsByCommand = new()
            {
                { CommandTitle, "Найти по названию" },
                { CommandAuthor, "Найти по автору" },
                { CommandReleaseYear, "Найти по году выпуска" },
                { CommandExit, "Выйти" }
            };

            foreach (KeyValuePair<string, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            bool isContinue = true;

            while (isContinue)
            {
                Console.Write("Введите по какому параметру нужно найти книгу: ");
                string parameterInput = Console.ReadLine();

                switch (parameterInput)
                {
                    case CommandTitle:
                        FindBookByTitle();
                        break;

                    case CommandAuthor:
                        FindBookByAuthor();
                        break;

                    case CommandReleaseYear:
                        FindBookByReleaseYear();
                        break;

                    case CommandExit:
                        isContinue = false;
                        break;
                }
            }
        }

        public void ViewAllBooks()
        {
            for (int i = 0; i < _storage.Count; i++)
            {
                Book book = _storage[i];
                Console.Write($"(Книга №{i + 1}) - ");
                book.View();
            }
        }

        private void ViewAllBooks(List<Book> books)
        {
            foreach (Book book in books)
            {
                book.View();
            }

            Console.WriteLine();
        }

        private bool TryGetBookByIdentifier(string identifier, out Book book)
        {
            bool isFound = false;

            book = null;

            foreach (Book element in _storage)
            {
                if (element.Identifier.ToString() == identifier)
                {
                    book = element;
                    isFound = true;
                }
            }

            return isFound;
        }

        private void FindBookByTitle()
        {
            string input = Console.ReadLine();

            if (TryGetBookByTitle(input, out List<Book> books))
            {
                Console.WriteLine("Найденные книги:");

                ViewAllBooks(books);
            }
        }

        private void FindBookByAuthor()
        {
            string input = Console.ReadLine();

            if (TryGetBookByAuthor(input, out List<Book> books))
            {
                Console.WriteLine("Найденные книги:");

                ViewAllBooks(books);
            }
        }

        private void FindBookByReleaseYear()
        {
            string input = Console.ReadLine();

            if (TryGetBookByReleaseYear(input, out List<Book> books))
            {
                Console.WriteLine("Найденные книги:");

                ViewAllBooks(books);
            }
        }

        private bool TryGetBookByTitle(string title, out List<Book> books)
        {
            books = new List<Book>();

            bool isFound = false;

            foreach (Book book in _storage)
            {
                if (book.Title.Contains(title))
                {
                    books.Add(book);

                    isFound = true;
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("Книг с таким названием не найдено.");
            }

            return isFound;
        }

        private bool TryGetBookByAuthor(string author, out List<Book> books)
        {
            books = new List<Book>();

            bool isFound = false;

            foreach (Book book in _storage)
            {
                if (book.Author.Contains(author))
                {
                    books.Add(book);
                    isFound = true;
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("Книг с таким названием не найдено.");
            }

            return isFound;
        }

        private bool TryGetBookByReleaseYear(string releaseYear, out List<Book> books)
        {
            books = new List<Book>();

            bool isFound = false;

            foreach (Book book in _storage)
            {
                if (book.ReleaseYear.ToString().Contains(releaseYear))
                {
                    books.Add(book);
                    isFound = true;
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("Книг с таким названием не найдено.");
            }

            return isFound;
        }
    }

    class Book
    {
        public Book(string title, string author, int releaseYear)
        {
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
            Identifier = Guid.NewGuid();
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public int ReleaseYear { get; private set; }
        public Guid Identifier { get; private set; }

        public void View()
        {
            Console.Write($"{Title} | Автор - {Author} | Год выпуска - {ReleaseYear} | ID: {Identifier}\n");
        }
    }
}
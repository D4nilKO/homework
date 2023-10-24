using System;
using System.Collections.Generic;

namespace homework.OOP.AutoService;

internal static class Program
{
    public static void Main(string[] args)
    {
        new AutoService().Work();
    }
}

class AutoService
{
    private int _serviceMoney = 1200;
    private int _punishment = 350;
    private int _clientsNumber = 1;

    private Queue<Client> _clients = new();
    private List<Detail> _detailsStorage = new();
    private List<Detail> _allDetails = new();

    public AutoService()
    {
        _allDetails.Add(new Detail("Блок управления ABS", 300));
        _allDetails.Add(new Detail("Генератор", 250));
        _allDetails.Add(new Detail("Тормозные диски", 235));
        _allDetails.Add(new Detail("Стартер", 275));
        _allDetails.Add(new Detail("Сцепление", 200));
        _allDetails.Add(new Detail("Глушитель", 100));
        _allDetails.Add(new Detail("Помпа", 150));

        FillStorage(_allDetails);
        CreateClients(_allDetails);
    }

    public void Work()
    {
        const string CommandTryToFixDetailCar = "1";
        const string CommandRefuseClient = "2";
        const string CommandExit = "3";

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandTryToFixDetailCar, "Заменить деталь машины." },
            { CommandRefuseClient, "Отказать клиенту в услуге." },
            { CommandExit, "Выйти из программы." }
        };

        bool isWork = true;

        while (isWork)
        {
            if (_serviceMoney < 0)
            {
                Console.WriteLine("Вы влезли в долги, прощайте...");
                return;
            }

            if (_clients.Count <= 0)
            {
                Console.WriteLine("Клиентов больше нет!");
                return;
            }

            Console.Clear();

            Client currentClient = _clients.Peek();
            Detail brokenDetail = currentClient.GetBrokenDetail();

            ShowInfo();

            brokenDetail.ShowInfo();

            DetailAvailableInStorage(brokenDetail.Name);

            foreach (KeyValuePair<string, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write("\nВыберете необходимую операцию: ");
            string desiredOperation = Console.ReadLine();
            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandTryToFixDetailCar:
                    FixCar(currentClient);
                    break;

                case CommandRefuseClient:
                    RefuseClient();
                    break;

                case CommandExit:
                    isWork = false;
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Неправильный ввод.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения... ");
            Console.ReadKey();
        }
    }

    private void FillStorage(List<Detail> details)
    {
        int maxCountForTypeOfDetail = 3;

        foreach (Detail detail in details)
        {
            int count = UserUtils.GetRandomNumber(1, maxCountForTypeOfDetail + 1);

            for (int i = 0; i < count; i++)
            {
                _detailsStorage.Add(detail.Clone());
            }
        }
    }

    private void AddMoneyForFixCar(int money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException(nameof(money));

        _serviceMoney += money;
    }

    private void FineForIncorrectRepairs()
    {
        if (_punishment < 0)
            throw new ArgumentOutOfRangeException(nameof(_punishment));

        _serviceMoney -= _punishment;
    }

    private void FixCar(Client client)
    {
        int priceForRepairDetail = 325;

        Detail brokenDetail = client.GetBrokenDetail();
        brokenDetail.ShowInfo();

        ShowStorage();

        Console.WriteLine("Выберите деталь, которую нужно заменить в машине: ");

        int detailNumber = UserUtils.GetNumberFromRange(1, _detailsStorage.Count);
        Detail detailFromStorage = _detailsStorage[detailNumber - 1];

        client.ReplaceDetail(brokenDetail, detailFromStorage);
        DeleteDetailFromStorage(detailFromStorage);

        if (brokenDetail.Name == detailFromStorage.Name)
        {
            AddMoneyForFixCar(brokenDetail.Cost + priceForRepairDetail);

            Console.WriteLine(
                $"Вы починили деталь: <{brokenDetail.Name}> за {brokenDetail.Cost + priceForRepairDetail} руб.\n");

            Console.WriteLine("До свидания, приезжайте еще!");
        }
        else
        {
            FineForIncorrectRepairs();
            Console.WriteLine($"Вы заменили не ту деталь. Вы получаете штраф в размере: {_punishment} руб.");
        }

        MoveToNextClient();
        _clientsNumber++;
    }

    private void RefuseClient()
    {
        Console.WriteLine($"Вы отказали в услуге. Вам придется заплатить штраф: {_punishment} руб.");

        FineForIncorrectRepairs();
        MoveToNextClient();
        _clientsNumber++;
    }

    private void ShowInfo()
    {
        Console.WriteLine($"\nКлиент на подходе. Кол-во клентов - {_clients.Count}.\n");
        Console.WriteLine($"Клиент №{_clientsNumber}:");
        Console.WriteLine("_______________________________________\n");
        Console.WriteLine($"На балансе автосервиса: {_serviceMoney} руб.");
    }

    private void CreateClients(List<Detail> details)
    {
        int minClients = 5;
        int maxClients = 12;

        int clientsCount = UserUtils.GetRandomNumber(minClients, maxClients);

        for (int i = 0; i < clientsCount; i++)
        {
            _clients.Enqueue(new Client(details));
        }
    }

    private void ShowStorage()
    {
        Console.WriteLine("Доступные детали на складе: ");

        for (int i = 0; i < _detailsStorage.Count; i++)
        {
            Detail detail = _detailsStorage[i];
            Console.WriteLine($"{i + 1}) {detail.Name}. Цена : {detail.Cost} руб.\n");
        }
    }

    private void MoveToNextClient()
    {
        if (_clients.Count > 0)
            _clients.Dequeue();
    }

    private void DeleteDetailFromStorage(Detail detail)
    {
        _detailsStorage.Remove(detail);
    }

    private void DetailAvailableInStorage(string nameOfDetail)
    {
        bool isFound = false;

        foreach (Detail detail in _detailsStorage)
        {
            if (detail.Name == nameOfDetail)
            {
                isFound = true;
            }
        }

        string status = (isFound
            ? $"Есть"
            : $"Нет");

        Console.WriteLine($"{status} на складе");
    }
}

class Client
{
    private List<Detail> _details = new();

    public Client(List<Detail> allDetails)
    {
        foreach (var detail in allDetails)
        {
            _details.Add(detail.Clone());
        }

        BreakRandomDetail();
    }

    public void ReplaceDetail(Detail brokenDetail, Detail newDetail)
    {
        _details.Remove(brokenDetail);
        _details.Add(newDetail);
    }

    public Detail GetBrokenDetail()
    {
        foreach (Detail detail in _details)
        {
            if (detail.IsBroken)
            {
                return detail;
            }
        }

        throw new Exception("У машины нет сломаной детали.");
    }

    private void BreakRandomDetail()
    {
        int randomDetail = UserUtils.GetRandomNumber(_details.Count);

        _details[randomDetail].Break();
    }
}

class Detail
{
    public Detail(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

    public string Name { get; private set; }
    public int Cost { get; private set; }
    public bool IsBroken { get; private set; }

    public Detail Clone()
    {
        return new Detail(Name, Cost);
    }

    public void ShowInfo()
    {
        string status = (IsBroken
            ? $"Cломана"
            : $"В рабочем состоянии");

        Console.WriteLine($"Деталь \"{Name.ToUpper()}\" - *** {status} ***");
    }

    public void Break()
    {
        IsBroken = true;
    }
}

internal static class UserUtils
{
    private static Random s_random = new();

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static int GetRandomNumber(int max)
    {
        return s_random.Next(max);
    }

    public static int GetNumberFromRange(int min, int max)
    {
        if (min > max)
        {
            Console.WriteLine($"min > max | {min} > {max}");
        }

        bool isLookingResult = true;
        int result = 0;

        Console.WriteLine($"Введите число от {min} до {max} включительно.");

        while (isLookingResult)
        {
            if (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.WriteLine("Некорректный ввод!");
                continue;
            }

            if (result < min || result > max)
            {
                Console.WriteLine("Введенное число не входит в диапазон!");
                continue;
            }

            isLookingResult = false;
        }

        return result;
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace homework.OOP.TrainConfigurator
{
    internal static class Program
    {
        public static void Main1(string[] args)
        {
            new TrainConfigurator().Work();
        }
    }

    class TrainConfigurator
    {
        private Train _currentTrain;
        private Direction _currentDirection;
        private uint _passengersCount;

        public void Work()
        {
            const string CommandCreateDirection = "1";
            const string CommandSellTickets = "2";
            const string CommandFormTrain = "3";
            const string CommandTrainDispatch = "4";
            const string CommandCreatePlan = "5";
            const string CommandExit = "Exit";

            Dictionary<string, string> actionsByCommand = new()
            {
                { CommandCreateDirection, "Создает направление для поезда" },
                { CommandSellTickets, "Вы получаете кол-во пассажиров, которые купили билеты на это направление" },
                { CommandFormTrain, "Создает поезд, и заполняет его вагоны пассажирами" },
                { CommandTrainDispatch, "Отправляет поезд, после чего можете снова создать направление" },
                { CommandCreatePlan, "(для ленивых) вы создаете полностью план поезда" },
                { CommandExit, "Выйти из программы" }
            };

            bool isWorking = true;

            while (isWorking)
            {
                Console.Clear();

                ShowInfo();

                Console.WriteLine();
                Console.WriteLine("\nМеню:");

                foreach (KeyValuePair<string, string> option in actionsByCommand)
                {
                    Console.WriteLine($"{option.Key} - {option.Value}");
                }

                Console.Write("\nВыберете необходимую операцию: ");
                string desiredOperation = Console.ReadLine();
                Console.WriteLine();

                switch (desiredOperation)
                {
                    case CommandCreateDirection:
                        CreateDirection();
                        break;

                    case CommandSellTickets:
                        SellTickets();
                        break;

                    case CommandFormTrain:
                        FormTrain();
                        break;

                    case CommandTrainDispatch:
                        DispatchTrain();
                        break;

                    case CommandCreatePlan:
                        CreatePlan();
                        break;

                    case CommandExit:
                        isWorking = false;
                        Console.WriteLine("Выход...");
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда. Повторите ввод.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения... ");
                Console.ReadKey();
            }
        }

        private void CreatePlan()
        {
            CreateDirection();
            SellTickets();
            FormTrain();
        }

        private void ShowInfo()
        {
            StringBuilder status = new("\nСостояние: ");
            string separator = " | ";

            if (_currentDirection != null)
            {
                status.Append($"{_currentDirection.GetInfo()}");
            }
            else
            {
                status.Append("Направление не задано");
            }

            status.Append(separator);

            if (_passengersCount > 0)
            {
                status.Append($"Количество купленых билетов: {_passengersCount}");
            }
            else
            {
                status.Append("Билеты не проданы");
            }

            status.Append(separator);

            if (_currentTrain != null)
            {
                status.Append($"{_currentTrain.GetInfo(separator)}");
            }
            else
            {
                status.Append("Поезд не создан");
            }

            Console.WriteLine(status.ToString());
        }

        private void CreateDirection()
        {
            if (_currentDirection == null)
            {
                Console.WriteLine("Введите откуда отправляется поезд: ");
                string departure = Console.ReadLine();

                Console.WriteLine("Введите куда приезжает поезд: ");
                string arrival = Console.ReadLine();

                _currentDirection = new Direction(departure, arrival);
            }
            else
            {
                Console.WriteLine("Направление уже задано.");
            }
        }

        private void SellTickets()
        {
            if (_currentDirection == null)
            {
                Console.WriteLine("Направление не задано, как вы продавать билеты можете?");
                return;
            }

            if (_passengersCount != 0)
            {
                Console.WriteLine("Билеты уже проданы.");
                return;
            }

            if (_currentTrain != null)
            {
                Console.WriteLine("Поезд уже сформирован");
                return;
            }

            int minRandomTicketCount = 10;
            int maxRandomTicketCount = 30;

            Random random = new();

            _passengersCount = (uint)random.Next(minRandomTicketCount, maxRandomTicketCount);

            Console.WriteLine($"Билеты купили {_passengersCount} человек.");
        }
        
        private void FormTrain()
        {
            if (_passengersCount <= 0)
            {
                Console.WriteLine("Билеты не проданы, невозможно создать поезд.");
                return;
            }

            if (_currentDirection == null)
            {
                Console.WriteLine("Направление не задано.");
                return;
            }

            if (_currentTrain != null)
            {
                Console.WriteLine("Поезд уже сформирован!");
                return;
            }

            _currentTrain = new Train(_currentDirection);

            _currentTrain.Form(_passengersCount);

            Console.WriteLine("\nПоезд успешно сформирован.");

            Console.WriteLine($"Каждый вагон вмещает {Train.SeatsPerWagon} человек");
            Console.WriteLine($"Количество пассажиров: {_passengersCount}");
            Console.WriteLine($"Количество вагонов: {_currentTrain.Wagons.Count}");
            Console.WriteLine($"Количество свободных мест: {_currentTrain.GetAllFreeSeats()}");
            Console.WriteLine();

            _currentTrain.DisplayAllSeats();
        }

        private void DispatchTrain()
        {
            if (_currentTrain != null)
            {
                Console.WriteLine($"Поезд {_currentTrain.Direction.GetInfo()} отбыл в путь!");
                Console.WriteLine("Теперь вы можете создать новый поезд.");

                _currentDirection = default;
                _currentTrain = default;
                _passengersCount = default;
            }
            else
            {
                Console.WriteLine("Поезд для отправки отсутсвует.");
            }
        }
    }

    class Train
    {
        public const uint SeatsPerWagon = 10;

        private bool _isFormed;

        public Train(Direction direction)
        {
            Direction = direction;
            _isFormed = false;
        }

        public List<Wagon> Wagons { get; private set; } = new();
        public Direction Direction { get; private set; }

        public string GetInfo(string separator)
        {
            StringBuilder info = new();

            info.Append($"Кол-во вагонов {Wagons.Count}{separator}");
            info.Append($"Кол-во свободных мест в каждом вагоне = {SeatsPerWagon}{separator}");
            info.Append($"Сформирован: {_isFormed}");

            return info.ToString();
        }

        public void DisplayAllSeats()
        {
            for (int i = 0; i < Wagons.Count; i++)
            {
                Wagon wagon = Wagons[i];
                for (int j = 0; j < wagon.GetSeats().Length; j++)
                {
                    Seat seat = wagon.GetSeats()[j];
                    Console.WriteLine($"Вагон: {i + 1} | Место: {j + 1} - {seat.IsOccupied}");
                }

                Console.WriteLine();
            }
        }

        public uint GetAllFreeSeats()
        {
            uint freeSeats = 0;

            foreach (Wagon wagon in Wagons)
            {
                freeSeats += wagon.GetCountOfFreeSeats();
            }

            return freeSeats;
        }

        public void Form(uint passengersCount)
        {
            uint wagonsCount = (uint)Math.Ceiling((double)passengersCount / SeatsPerWagon);

            AddWagons(wagonsCount, SeatsPerWagon);
            FillWagons(passengersCount);

            _isFormed = true;
        }

        private void AddWagons(uint count, uint allSeatsCount)
        {
            for (int i = 0; i < count; i++)
            {
                Wagons.Add(new Wagon(allSeatsCount));
            }
        }

        private bool TryGetFreeSeat(out Seat freeSeat)
        {
            freeSeat = null;

            foreach (Wagon wagon in Wagons)
            {
                foreach (Seat seat in wagon.GetSeats())
                {
                    if (seat.IsOccupied == false)
                    {
                        freeSeat = seat;
                        return true;
                    }
                }
            }

            return false;
        }

        private void FillWagons(uint passengersCount)
        {
            for (int i = 0; i < passengersCount; i++)
            {
                if (TryGetFreeSeat(out Seat seat))
                {
                    seat.TakeSeat();
                }
            }
        }
    }

    class Direction
    {
        private string _departure;
        private string _arrival;

        public Direction(string departure, string arrival)
        {
            _departure = departure;
            _arrival = arrival;
        }

        public string GetInfo()
        {
            return ($"{_departure} - {_arrival}");
        }
    }

    class Wagon
    {
        private Seat[] _seats;

        public Wagon(uint allSeatsCount)
        {
            _seats = new Seat[allSeatsCount];

            for (int index = 0; index < _seats.Length; index++)
            {
                _seats[index] = new Seat();
            }
        }

        public Seat[] GetSeats()
        {
            return _seats;
        }

        public uint GetCountOfFreeSeats()
        {
            uint countOfFreeSeats = 0;

            foreach (Seat seat in _seats)
            {
                if (seat.IsOccupied == false)
                {
                    countOfFreeSeats++;
                }
            }

            return countOfFreeSeats;
        }
    }

    class Seat
    {
        public Seat()
        {
            IsOccupied = false;
        }

        public bool IsOccupied { get; private set; }

        public void TakeSeat()
        {
            IsOccupied = true;
        }
    }
}
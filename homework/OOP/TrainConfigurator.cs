using System;
using System.Collections.Generic;

namespace homework.OOP.TrainConfigurator
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            TrainConfigurator trainConfigurator = new();

            trainConfigurator.MakeTrainPlan();
        }
    }

    class TrainConfigurator
    {
        public Train CurrentTrain;
        public Direction CurrentDirection;
        public uint PassengersCount;

        public void MakeTrainPlan()
        {
            CreateDirection();
            SellTickets();
            FormTrain();
        }

        public void CreateDirection()
        {
            Console.WriteLine("Введите откуда отправляется поезд: ");
            string departure = Console.ReadLine();

            Console.WriteLine("Введите куда приезжает поезд: ");
            string arrival = Console.ReadLine();

            CurrentDirection = new Direction(departure, arrival);
        }

        public void SellTickets()
        {
            int minRandomTicketCount = 10;
            int maxRandomTicketCount = 30;

            Random random = new();

            PassengersCount = (uint)random.Next(minRandomTicketCount, maxRandomTicketCount);
        }

        public void FormTrain()
        {
            CurrentTrain = new Train(CurrentDirection);

            if (CurrentTrain.TryForm(PassengersCount))
            {
                Console.WriteLine("\nПоезд успешно сформирован.");
                Console.WriteLine($"Вагон вмещает : {CurrentTrain.Wagons[0].AllSeatsCount}");
                Console.WriteLine($"Количество пассажиров: {PassengersCount}");
                Console.WriteLine($"Количество вагонов: {CurrentTrain.Wagons.Count}");
                Console.WriteLine($"Количество свободных мест: {CurrentTrain.GetAllFreeSeats()}");
            }
            else
            {
                Console.WriteLine("Ошибка формирования поезда.");
            }
        }

        public void TrainDispatch()
        {
            CurrentDirection = default;
            CurrentTrain = default;
            PassengersCount = default;
        }
    }

    class Train
    {
        public Train(Direction direction)
        {
            Direction = direction;
            AddWagons(StartWagonsCount);
        }

        public List<Wagon> Wagons { get; private set; } = new();
        public Direction Direction { get; private set; }
        private uint StartWagonsCount { get; set; } = 1;

        public uint GetAllFreeSeats()
        {
            uint freeSeats = 0;

            foreach (var wagon in Wagons)
            {
                freeSeats += wagon.GetCountOfFreeSeats();
            }

            return freeSeats;
        }

        public void AddWagons(uint count)
        {
            Console.WriteLine($"Вагонов добавилось {count}");
            
            for (int i = 0; i < count; i++)
            {
                Wagons.Add(new Wagon());
            }
        }

        public bool TryGetIncompleteWagon(out Wagon wagon)
        {
            bool isFound = false;
            wagon = null;

            foreach (var element in Wagons)
            {
                if (element.TryGetFreeSeat(out Seat seat))
                {
                    isFound = true;
                    wagon = element;
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("Незаполненый вагон не найден!");
            }

            return isFound;
        }

        public void FillWagons(uint passengersCount)
        {
            while (passengersCount > 0)
            {
                if (TryGetIncompleteWagon(out Wagon wagon))
                {
                    if (wagon.TryGetFreeSeat(out Seat seat))
                    {
                        if (seat.TryTakeSeat())
                        {
                            passengersCount--;
                        }
                        else
                        {
                            Console.WriteLine("ошибка 3");
                            Console.WriteLine($"{passengersCount}");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ошибка 2");
                        Console.WriteLine($"{passengersCount}");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("ошибка 1");
                    Console.WriteLine($"{passengersCount}");
                    break;
                }
            }
        }

        public bool TryForm(uint passengersCount)
        {
            bool isFormed = false;

            if (TryGetIncompleteWagon(out Wagon firstWagon))
            {
                uint seatsPerWagon = firstWagon.AllSeatsCount;
                uint wagonsCount = (uint)Math.Ceiling((double)passengersCount / seatsPerWagon);

                AddWagons((uint)(wagonsCount - Wagons.Count));
                FillWagons(passengersCount);

                isFormed = true;
            }
            else
            {
                Console.WriteLine("Ошибка: У поезда нет стартового вагона");
            }

            return isFormed;
        }
    }

    struct Direction
    {
        public Direction(string departure, string arrival)
        {
            Departure = departure;
            Arrival = arrival;
        }

        public string Departure { get; private set; }
        public string Arrival { get; private set; }
    }

    class Wagon
    {
        public Wagon()
        {
            Seats = new Seat[AllSeatsCount];

            for (var index = 0; index < Seats.Length; index++)
            {
                Seats[index] = new Seat();
            }
        }

        public uint GetCountOfFreeSeats()
        {
            uint countOfFreeSeats = 0;

            foreach (var seat in Seats)
            {
                if (seat.IsOccupied == true)
                {
                    countOfFreeSeats++;
                }
            }

            return countOfFreeSeats;
        }

        public uint AllSeatsCount { get; private set; } = 10;
        public Seat[] Seats { get; private set; }

        public bool TryGetFreeSeat(out Seat seat)
        {
            seat = null;

            foreach (var element in Seats)
            {
                if (element.TryTakeSeat())
                {
                    seat = element;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Вагон уже заполнен");
                }
            }

            return false;
        }
    }

    class Seat
    {
        public Seat()
        {
            IsOccupied = false;
        }

        public bool IsOccupied { get; private set; }

        public bool TryTakeSeat()
        {
            if (IsOccupied == false)
            {
                IsOccupied = true;
                return true;
            }
            else
            {
                Console.WriteLine("Место уже занято");
                return false;
            }
        }
    }
}
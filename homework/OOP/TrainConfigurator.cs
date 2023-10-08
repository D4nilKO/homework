using System;
using System.Collections.Generic;

namespace homework.OOP.TrainConfigurator
{
    internal static class Program
    {
        public static void Main1(string[] args)
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

            CurrentTrain.Form(PassengersCount);

            Console.WriteLine("\nПоезд успешно сформирован.");
            
            Console.WriteLine($"Каждый вагон вмещает {Wagon.AllSeatsCount} человек");
            Console.WriteLine($"Количество пассажиров: {PassengersCount}");
            Console.WriteLine($"Количество вагонов: {CurrentTrain.Wagons.Count}");
            Console.WriteLine($"Количество свободных мест: {CurrentTrain.GetAllFreeSeats()}");
            Console.WriteLine();
            
            CurrentTrain.DisplayAllSeats();
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

        public void DisplayAllSeats()
        {
            for (var i = 0; i < Wagons.Count; i++)
            {
                var wagon = Wagons[i];
                for (var j = 0; j < wagon.Seats.Length; j++)
                {
                    var seat = wagon.Seats[j];
                    Console.WriteLine($"Вагон: {i + 1} | Место: {j + 1} - {seat.IsOccupied}");
                }

                Console.WriteLine();
            }
        }

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
            for (int i = 0; i < count; i++)
            {
                Wagons.Add(new Wagon());
            }
        }

        public bool TryGetFreeSeat(out Seat freeSeat)
        {
            freeSeat = null;

            foreach (var wagon in Wagons)
            {
                foreach (var seat in wagon.Seats)
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

        public void FillWagons(uint passengersCount)
        {
            for (int i = 0; i < passengersCount; i++)
            {
                if (TryGetFreeSeat(out Seat seat))
                {
                    seat.TakeSeat();
                }
            }
        }

        public void Form(uint passengersCount)
        {
            uint seatsPerWagon = Wagon.AllSeatsCount;
            uint wagonsCount = (uint)Math.Ceiling((double)passengersCount / seatsPerWagon);

            AddWagons((uint)(wagonsCount - Wagons.Count));
            FillWagons(passengersCount);
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
                if (seat.IsOccupied == false)
                {
                    countOfFreeSeats++;
                }
            }

            return countOfFreeSeats;
        }

        public static readonly uint AllSeatsCount = 10;
        public Seat[] Seats { get; private set; }
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
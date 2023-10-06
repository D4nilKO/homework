using System;
using System.Collections.Generic;

namespace homework.OOP.TrainConfigurator
{
    internal static class Program
    {
        public static void Main1(string[] args)
        {
            TrainConfigurator trainConfigurator = new();
        }
    }

    class TrainConfigurator
    {
        public Train CurrentTrain;
        public Direction CurrentDirection;
        public uint TicketsCount;

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
            int minRandomTicketCount = 100;
            int maxRandomTicketCount = 1000;

            Random random = new();

            TicketsCount = (uint)random.Next(minRandomTicketCount, maxRandomTicketCount);
        }

        public void CreateTrain()
        {
        }
    }

    class Train
    {
        public Train(Direction direction)
        {
            Direction = direction;
        }

        public List<Wagon> Wagons { get; private set; } = new();
        public Direction Direction { get; private set; }

        // public 
    }

    class Direction
    {
        public Direction(string departure, string arrival)
        {
            Departure = departure;
            Arrival = arrival;
        }

        public string Departure { get; private set; }
        public string Arrival { get; private set; }

        // public void View()
        // {
        //     Console.WriteLine($"Поезд: {Departure} - {Arrival}");
        // }
    }

    class Wagon
    {
        public Wagon()
        {
            Seats = new Seat[SeatsCount];
        }

        public uint SeatsCount { get; private set; } = 50;
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
            }

            return false;
        }
    }

    class Seat
    {
        public bool IsOccupied { get; private set; }

        public bool TryTakeSeat()
        {
            if (IsOccupied == false)
            {
                IsOccupied = true;
                return true;
            }

            Console.WriteLine("Место уже занято");
            return false;
        }
    }
}
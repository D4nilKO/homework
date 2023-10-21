using System;
using System.Collections.Generic;

namespace homework.OOP.Deck;

internal static class Program
{
    public static void Main1(string[] args)
    {
        const string CommandDrawCard = "Draw";
        const string CommandExit = "Exit";

        Deck deck = new();
        Player player = new();

        deck.ShowAllCards();

        Console.WriteLine();

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandDrawCard, "Взять карту" },
            { CommandExit, "Выйти из программы" }
        };

        bool isContinue = true;

        while (isContinue)
        {
            foreach (KeyValuePair<string, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write("Выберете необходимую операцию: ");
            string desiredOperation = Console.ReadLine();

            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandDrawCard:
                    player.DrawCard(deck.GiveCard());
                    break;

                case CommandExit:
                    isContinue = false;
                    break;
            }
        }

        player.ShowAllCard();
        Console.WriteLine("\nВы закончили брать карты и вышли.");
    }
}

class Deck
{
    private Random _random = new();

    private List<Card> _сards = new();

    public Deck()
    {
        Fill();
        Shuffle();
        ShowAllCards();
    }

    public Card GiveCard()
    {
        if (_сards.Count > 0)
        {
            Card card = _сards[0];
            _сards.Remove(card);

            return card;
        }

        return null;
    }

    public void ShowAllCards()
    {
        foreach (Card card in _сards)
        {
            card.Show();
        }

        Console.WriteLine($"\nВ колоде осталось {_сards.Count} карт.");
    }

    private void Shuffle()
    {
        for (int i = _сards.Count - 1; i > 0; i--)
        {
            int secondNumber = _random.Next(i + 1);

            SwapCards(i, secondNumber);
        }
    }

    private void SwapCards(int firstCard, int secondCard)
    {
        Card temporaryCard = _сards[firstCard];

        _сards[firstCard] = _сards[secondCard];
        _сards[secondCard] = temporaryCard;
    }

    private void Fill()
    {
        int minRank = 6;
        int maxRank = 14;

        string[] suits =
        {
            "Spades",
            "Hearts",
            "Diamonds",
            "Clubs"
        };

        for (int i = minRank; i < maxRank + 1; i++)
        {
            for (int j = 0; j < suits.Length; j++)
            {
                _сards.Add(new Card(i, suits[j]));
            }
        }
    }
}

class Card
{
    public Card(int rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public int Rank { get; private set; }
    public string Suit { get; private set; }

    public void Show()
    {
        Console.WriteLine($"{Rank} - {Suit}");
    }
}

class Player
{
    private List<Card> _hand = new();

    public void DrawCard(Card card)
    {
        if (card != null)
        {
            _hand.Add(card);

            Console.Write($"Вы взяли карту ");
            card.Show();
        }
    }

    public void ShowAllCard()
    {
        Console.WriteLine("Ваши карты:");

        foreach (Card card in _hand)
        {
            card.Show();
        }
    }
}
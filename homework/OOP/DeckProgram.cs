using System;
using System.Collections.Generic;

namespace homework.OOP;

internal static class DeckProgram
{
    public static void Main(string[] args)
    {
        const string CommandDrawCard = "Draw";
        const string CommandExit = "Exit";

        Deck deck = new(6, 14);
        Player player = new();

        deck.ShowAllCards();

        Console.WriteLine();

        bool isContinue = true;

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandDrawCard, "Взять карту" },
            { CommandExit, "Выйти из программы" }
        };

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
                    player.DrawCard(deck);
                    break;

                case CommandExit:
                    player.ShowAllCard();

                    Console.WriteLine("\nВы закончили брать карты и вышли.");
                    isContinue = false;
                    break;
            }
        }
    }
}

class Deck
{
    private Random _random = new();

    private List<Card> _deck = new();

    private string[] _suits =
    {
        "Spades",
        "Hearts",
        "Diamonds",
        "Clubs"
    };

    public Deck(uint minRank, uint maxRank)
    {
        _minRank = minRank;
        _maxRank = maxRank;

        FillDeck();
    }

    public uint _minRank { get; private set; }
    public uint _maxRank { get; private set; }

    public Card GetNextCard()
    {
        Card card = _deck[0];
        _deck.RemoveAt(0);
        return card;
    }

    public void ShowAllCards()
    {
        foreach (Card card in _deck)
        {
            card.ShowCard();
        }

        Console.WriteLine($"\nВ колоде осталось {_deck.Count} карт.");
    }

    private void Shuffle()
    {
        for (int i = _deck.Count - 1; i > 0; i--)
        {
            int secondNumber = _random.Next(i + 1);

            Swap(_deck, i, secondNumber);
        }
    }

    private void Swap(List<Card> list, int firstCard, int secondCard)
    {
        Card temporaryCard = list[firstCard];

        list[firstCard] = list[secondCard];
        list[secondCard] = temporaryCard;
    }

    private void FillDeck()
    {
        _deck.Clear();
        _deck.Clear();

        int suitCount = _deck.

        for (uint i = _minRank; i < _maxRank + 1; i++)
        {
            for (int j = 0; j < suitCount; j++)
            {
                _deck.Add(new Card(i, (Suit)j));
            }
        }

        Shuffle();

        foreach (var card in _deck)
        {
            _deck.Enqueue(card);
        }
    }
}

class Card
{
    public Card(uint rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public uint Rank { get; private set; }
    public Suit Suit { get; private set; }

    public void ShowCard()
    {
        Console.WriteLine($"{Rank} - {Suit}");
    }
}

class Player
{
    private List<Card> _hand = new();

    public void DrawCard(Deck deck)
    {
        _hand.Add(deck.GetNextCard());

        Console.Write($"Вы взяли карту ");
        _hand[_hand.Count - 1].ShowCard();
    }

    public void ShowAllCard()
    {
        Console.WriteLine("Ваши карты:");

        foreach (var card in _hand)
        {
            card.ShowCard();
        }
    }
}
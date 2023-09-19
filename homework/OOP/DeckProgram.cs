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

    private List<Card> _temporaryDeck = new();
    private Queue<Card> _deck = new();

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
        return _deck.Dequeue();
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
        for (int firstNumber = _temporaryDeck.Count - 1; firstNumber > 0; firstNumber--)
        {
            int secondNumber = _random.Next(firstNumber + 1);

            Swap(_temporaryDeck, firstNumber, secondNumber);
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
        _temporaryDeck.Clear();
        _deck.Clear();

        int suitCount = Enum.GetNames(typeof(Suit)).Length;

        for (uint i = _minRank; i < _maxRank + 1; i++)
        {
            for (int j = 0; j < suitCount; j++)
            {
                _temporaryDeck.Add(new Card(i, (Suit)j));
            }
        }

        Shuffle();

        foreach (var card in _temporaryDeck)
        {
            _deck.Enqueue(card);
        }
    }
}

enum Suit
{
    Spades,
    Hearts,
    Diamonds,
    Clubs
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
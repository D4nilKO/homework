using System;
using System.Collections.Generic;

namespace homework.OOP;

internal static class DeckProgram
{
    public static void Main(string[] args)
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
                    player.DrawCard(deck);
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
    private const int MinRank = 6;
    private const int MaxRank = 14;

    private Random _random = new();

    private List<Card> _deck = new();

    private string[] _suits =
    {
        "Spades",
        "Hearts",
        "Diamonds",
        "Clubs"
    };

    public Deck()
    {
        Fill();
        Shuffle();
        ShowAllCards();
    }

    public Card GetCard()
    {
        Card card = _deck[0];
        _deck.RemoveAt(0);
        return card;
    }

    public void DrawCard(List<Card> hand)
    {
        hand.Add(GetCard());
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

            SwapCards(_deck, i, secondNumber);
        }
    }

    private void SwapCards(List<Card> list, int firstCard, int secondCard)
    {
        Card temporaryCard = list[firstCard];

        list[firstCard] = list[secondCard];
        list[secondCard] = temporaryCard;
    }

    private void Fill()
    {
        _deck.Clear();
        _deck.Clear();

        int suitCount = _suits.Length;

        for (uint i = MinRank; i < MaxRank + 1; i++)
        {
            for (int j = 0; j < suitCount; j++)
            {
                _deck.Add(new Card(i, _suits[j]));
            }
        }
    }
}

class Card
{
    public Card(uint rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public uint Rank { get; private set; }
    public string Suit { get; private set; }

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
        _hand.Add(deck.GetCard());

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
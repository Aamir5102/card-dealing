using System;
using System.Collections.Generic;
// giving the suits of the cards
enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}
// giving the numbers of the cards here ace - king 
enum Rank
{
    Ace = 1,
    Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
    Jack, Queen, King
}

class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

class Pack
{
    private List<Card> cards;
    // makes the pack of cards
    public Pack()
    {
        cards = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }
// where input is required on which shuffle needs to be done 
    public bool Shuffle(int typeOfShuffle)
    {
        switch (typeOfShuffle)
        {
            case 1:
                return FisherYatesShuffle();
            case 2:
                return RiffleShuffle();
            case 3:
                return true;
            default:
                return false;
        }
    }
    //cards are being dealt
    public Card Deal()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("No cards left in the pack.");
        }
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public List<Card> Deal(int amount)
    {
        List<Card> dealtCards = new List<Card>();
        for (int i = 0; i < amount; i++)
        {
            dealtCards.Add(Deal());
        }
        return dealtCards;
    }
//making the types of shuffle
    private bool FisherYatesShuffle()
    {
        Random random = new Random();
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
        return true;
    }

    private bool RiffleShuffle()
    {
        List<Card> shuffledCards = new List<Card>();
        int halfIndex = cards.Count / 2;
        for (int i = 0; i < halfIndex; i++)
        {
            shuffledCards.Add(cards[i]);
            shuffledCards.Add(cards[halfIndex + i]);
        }
        if (cards.Count % 2 != 0)
        {
            shuffledCards.Add(cards[cards.Count - 1]);
        }
        cards = shuffledCards;
        return true;
    }

//the information that was going to be printed in the terminal for the input 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Card shuffler!");
            Console.WriteLine("Select the type of shuffle youd like to do:");
            Console.WriteLine("1 - Fisher-Yates shuffle");
            Console.WriteLine("2 - Riffle shuffle");
            

            string input = Console.ReadLine();
            int shuffleType;
            if (int.TryParse(input, out shuffleType))
            {
                Pack pack = new Pack();
                bool shuffleResult = pack.Shuffle(shuffleType);
                if (shuffleResult)
                {
                    Console.WriteLine("Shuffle successful!");
                    Card dealtCard = pack.Deal();
                    Console.WriteLine($"Dealt card: {dealtCard}");
                    List<Card> dealtCards = pack.Deal(5);
                    Console.WriteLine("Dealt cards:");
                    foreach (Card card in dealtCards)
                    {
                        Console.WriteLine(card);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid shuffle type selected.");
                    Main(args);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1, 2, or any other key to exit.");
                Main(args); // call Main again to ask for another input
            }
        }
    }
}


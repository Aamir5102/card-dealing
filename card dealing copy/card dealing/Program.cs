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
    
    // create a new Card with a suit and number 
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
    //returns a string version of the card 
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}
// class pakc reprsents the desk of cards
class Pack
{
    private List<Card> cards;
    // makes the pack of cards
    public Pack()
    {
        cards = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))//iterates aover all of the suits 
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))//iterates over all of the numbers
            {
                cards.Add(new Card(suit, rank));// adds a new card 
            }
        }
    }
// shuffles the cards on what is selected 
    public bool Shuffle(int typeOfShuffle)
    {
        switch (typeOfShuffle)
        {
            case 1:
                return FisherYatesShuffle();//does fisher yates shuffle
            case 2:
                return RiffleShuffle();// does a riffle shuffle 
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
        Card card = cards[0];//gets card from top of pack
        cards.RemoveAt(0); //removes card from pack
        return card;//returns card that was dealt
    }

    public List<Card> Deal(int amount)
    {
        List<Card> dealtCards = new List<Card>();
        for (int i = 0; i < amount; i++)
        {
            dealtCards.Add(Deal());//calls deal and adds it to delat cards
        }
        return dealtCards;//returns the list of cards that were dealt
    }
//shuffles he cards with fisheryates shuffle 
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
    //shuffles the cards with a riffle shuffle
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
            Console.WriteLine("3 - No shuffle");
            

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
                    List<Card> dealtCards = pack.Deal(5);// 5 cards being dealt
                    Console.WriteLine("Dealt cards:");
                    foreach (Card card in dealtCards)
                    {
                        Console.WriteLine(card);//prints cards in the console 
                    }
                }
                else
                {
                    Console.WriteLine("Invalid shuffle type selected.");
                    Main(args);// call Main again to ask for another input if value not 1 or 2
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                Main(args); // call Main again to ask for another inputif value not 1 or 2
            }
        }
    }
}


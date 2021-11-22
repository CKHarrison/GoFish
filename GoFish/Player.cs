using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public class Player
    {
        public static Random Random = new Random();

        private List<Card> hand = new List<Card>();
        private List<Values> books = new List<Values>();
        /// <summary>
        /// Card's in a players hand
        /// </summary>
        public IEnumerable<Card> Hand => hand;
        /// <summary>
        /// The books a player pulled out when found 4 matches
        /// </summary>
        public IEnumerable<Values> Books => books;

        public readonly string Name;
        /// <summary>
        /// Pluralizes the amount of cards if more than 1
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string S(int s) => s == 1 ? "" : "s";

        /// <summary>
        /// Displays the information about the player
        /// </summary>
        public string Status => $"{Name} has {Hand.Count()} card{S(Hand.Count())} and {Books.Count()} book{S(Books.Count())}";

        /// <summary>
        /// Overladed Constructor to create a player
        /// </summary>
        /// <param name="name"></param>
        public Player(string name) => Name = name;

        /// <summary>
        /// Overloaded constructor to create a player, used for unit testing
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cards"></param>
        public Player(string name, IEnumerable<Card> cards)
        {
            Name = name;
            hand.AddRange(cards);
        }

        /// <summary>
        /// Take the next five cards and add them to the hand
        /// </summary>
        /// <param name="stock"></param>
        public void GetNextHand(Deck stock)
        {
            while(stock.Count() > 0 && hand.Count() < 5)
            {
                hand.Add(stock.Deal(0));
            }
        }
        /// <summary>
        /// Ask if a player has any specific values, if hand is 0, draw from deck
        /// </summary>
        /// <param name="values"></param>
        /// <param name="deck"></param>
        /// <returns></returns>
        public IEnumerable<Card> DoYouHaveAny(Values values, Deck deck)
        {
            IEnumerable<Card> matchedCards = hand.Where(card => card.Value == values)
                .OrderBy(Card => Card.Suit);
            hand = hand.Where(card => card.Value != values).ToList();
            if (hand.Count == 0)
            {
                GetNextHand(deck);
            }
            return matchedCards;
        }

        public void AddCardsAndPullOutBooks(IEnumerable<Card> cards)
        {
            hand.AddRange(cards);
            var fourCards = hand.GroupBy(card => card.Value)
                .Where(group => group.Count() == 4)
                .Select(group => group.Key);

            books.AddRange(fourCards);
            books.Sort();

            hand = hand.Where(card => !books.Contains(card.Value)).ToList();
        }

        public void DrawCard(Deck stock)
        {
            hand.Add(stock.Deal(0));
        }

        public Values RandomValuesFromHand()
        {
            var randomValue = hand.OrderBy(card => card.Value)
                .Select(card => card.Value)
                .Skip(Random.Next(1, 4)).First();
            return randomValue;
        }

        public override string ToString() => Name;

    }
}

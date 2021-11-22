using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GoFish
{
    class Deck : ObservableCollection<Card>
    {
        private static Random random = new Random();
        
        public Deck()
        {
            Reset();
        }

        public void Reset()
        {
            Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    Add(new Card((Values)j, (Suits)i));
                }
            }
        }

        public Card Deal(int index)
        {
            Card cardToDeal = base[index];
            RemoveAt(index);
            return cardToDeal;
        }

        public Deck Shuffle()
        {
            List<Card> copy = new List<Card>(this);
            Clear();
            while (copy.Count > 0)
            {
                int index = random.Next(copy.Count);
                Card card = copy[index];
                copy.RemoveAt(index);
                Add(card);               
            }
            return this;
        }
        public void Sort()
        {
            List<Card> sortedCards = new List<Card>(this);
            sortedCards.Sort(new CardComparerByValue());
            Clear();
            foreach (Card card in sortedCards)
            {
                Add(card);
            }
        }
    }
}

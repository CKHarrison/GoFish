using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public class Card : IComparable<Card>
    {
        public Suits Suit { get; private set; }
        public Values Value { get; private set; }
        public string Name { get { return $"{Value} of {Suit}"; } }

        public Card(Values value, Suits suits)
        {

            this.Value = value;
            this.Suit = suits;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public int CompareTo(Card other)
        {
            return new CardComparerByValue().Compare(this, other);
        }
    }
}

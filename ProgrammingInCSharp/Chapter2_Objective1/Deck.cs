namespace Chapter2_Objective1
{
    using System.Collections.Generic;
    using System.Linq;

    public class Deck
    {
        private int maxNumberOfCards;

        public Deck() : this(5)
        {
        }

        public Deck(int maxNumberOfCards)
        {
            this.maxNumberOfCards = maxNumberOfCards;
            Cards = new List<Card>();
        }

        public List<Card> Cards { get; private set; }

        public Card this[int index]
        {
            get
            {
                return Cards?.ElementAt(index);
            }

            set
            {
                Cards[index] = value;
            }
        }
    }
}

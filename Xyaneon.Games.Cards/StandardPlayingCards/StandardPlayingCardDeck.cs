using System;
using System.Collections.Generic;

namespace Xyaneon.Games.Cards.StandardPlayingCards
{
    /// <summary>
    /// A deck containing standard playing cards.
    /// </summary>
    /// <seealso cref="DrawPile{TCard}"/>
    /// <seealso cref="StandardPlayingCard"/>
    public class StandardPlayingCardDeck : DrawPile<StandardPlayingCard>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StandardPlayingCardDeck"/> class, containing all 52
        /// standard playing cards.
        /// </summary>
        /// <param name="isFaceUp">
        /// A value indicating whether this
        /// <see cref="StandardPlayingCardDeck"/> is supposed to be face-up
        /// (<see langword="true"/>) or face-down (<see langword="false"/>).
        /// </param>
        /// <remarks>
        /// This constructor creates a deck which is pre-populated with all of
        /// the cards typically found in a standard 52-card deck. If you
        /// instead need a deck which can hold
        /// <see cref="StandardPlayingCard"/> objects but is initialized to
        /// contain something else or nothing at all, then consider using the
        /// <see cref="DrawPile{TCard}"/> parent class instead.
        /// </remarks>
        public StandardPlayingCardDeck(bool isFaceUp = false) : base(Create52StandardPlayingCards(), isFaceUp) { }

        #endregion // End constructors region.

        #region Methods

        #region Private methods

        /// <summary>
        /// Creates and returns the 52 cards found in a standard playing card
        /// deck as an enumerable collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing all of the cards
        /// typically found in a standard playing card deck.
        /// </returns>
        private static IEnumerable<StandardPlayingCard> Create52StandardPlayingCards()
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    yield return new StandardPlayingCard(rank, suit);
                }
            }
        }

        #endregion // End private methods region.

        #endregion // End methods region.
    }
}

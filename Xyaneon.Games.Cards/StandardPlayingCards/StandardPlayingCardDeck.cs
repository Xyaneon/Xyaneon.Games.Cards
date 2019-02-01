using System;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.Games.Cards.StandardPlayingCards
{
    /// <summary>
    /// A deck containing standard playing cards.
    /// </summary>
    /// <seealso cref="DrawPile{TCard}"/>
    /// <seealso cref="StandardPlayingCard"/>
    public class StandardPlayingCardDeck : DrawPile<StandardPlayingCard>
    {
        #region Constants

        private const string NumberOfJokersLessThanZeroErrorMessage = "The number of jokers to include in the deck cannot be less than zero.";

        #endregion // End constants region.

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StandardPlayingCardDeck"/> class, containing all 52
        /// standard playing cards and the specified number of jokers.
        /// </summary>
        /// <param name="isFaceUp">
        /// A value indicating whether this
        /// <see cref="StandardPlayingCardDeck"/> is supposed to be face-up
        /// (<see langword="true"/>) or face-down (<see langword="false"/>).
        /// </param>
        /// <param name="numberOfJokers">
        /// The number of <see cref="Joker"/> cards this
        /// <see cref="StandardPlayingCardDeck"/> should also contain. This
        /// parameter is zero by default.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfJokers"/> is less than zero.
        /// </exception>
        /// <remarks>
        /// This constructor creates a deck which is pre-populated with all of
        /// the cards typically found in a standard 52-card deck, plus any
        /// specified <see cref="Joker"/> cards. If you instead need a deck
        /// which can hold <see cref="StandardPlayingCard"/> objects but is
        /// initialized to contain something else or nothing at all, then
        /// consider using the <see cref="DrawPile{TCard}"/> base class
        /// instead.
        /// </remarks>
        public StandardPlayingCardDeck(bool isFaceUp = false, int numberOfJokers = 0) : base(Create52StandardPlayingCards(numberOfJokers), isFaceUp) { }

        #endregion // End constructors region.

        #region Methods

        #region Private methods

        /// <summary>
        /// Creates and returns the 52 cards found in a standard playing card
        /// deck as an enumerable collection.
        /// </summary>
        /// <param name="numberOfJokers">
        /// The number of <see cref="Joker"/> cards the enumerable collection
        /// should also contain. This parameter is zero by default.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> containing all of the cards
        /// typically found in a standard playing card deck.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfJokers"/> is less than zero.
        /// </exception>
        private static IEnumerable<StandardPlayingCard> Create52StandardPlayingCards(int numberOfJokers = 0)
        {
            if (numberOfJokers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfJokers), numberOfJokers, NumberOfJokersLessThanZeroErrorMessage);
            }

            foreach (Rank rank in Enum.GetValues(typeof(Rank)).Cast<Rank>().Where(x => x != Rank.None))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)).Cast<Suit>().Where(x => x != Suit.None))
                {
                    yield return new StandardPlayingCard(rank, suit);
                }
            }

            int i = 0;
            while (i < numberOfJokers)
            {
                i++;
                yield return new Joker();
            }
        }

        #endregion // End private methods region.

        #endregion // End methods region.
    }
}

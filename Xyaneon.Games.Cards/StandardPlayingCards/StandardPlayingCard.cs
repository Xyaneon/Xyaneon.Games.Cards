namespace Xyaneon.Games.Cards.StandardPlayingCards
{
    /// <summary>
    /// A card from a standard 52-card deck.
    /// </summary>
    /// <remarks>
    /// More information about a standard 52-card deck can be found here:
    /// https://en.wikipedia.org/wiki/Standard_52-card_deck
    /// </remarks>
    /// <seealso cref="StandardPlayingCards.Rank"/>
    /// <seealso cref="StandardPlayingCards.Suit"/>
    /// <seealso cref="StandardPlayingCardDeck"/>
    public class StandardPlayingCard : Card
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StandardPlayingCard"/> class using the provided rank
        /// and suit.
        /// </summary>
        /// <param name="rank">
        /// The <see cref="StandardPlayingCards.Rank"/> of the card.
        /// </param>
        /// <param name="suit">
        /// The <see cref="StandardPlayingCards.Suit"/> of the card.
        /// </param>
        public StandardPlayingCard(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        #endregion // End constructors region.

        #region Properties

        /// <summary>
        /// Gets the <see cref="StandardPlayingCards.Rank"/> of this card.
        /// </summary>
        public Rank Rank { get; }

        /// <summary>
        /// Gets the <see cref="StandardPlayingCards.Suit"/> of this card.
        /// </summary>
        public Suit Suit { get; }

        #endregion // End properties region.
    }
}

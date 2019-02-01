namespace Xyaneon.Games.Cards.StandardPlayingCards
{
    /// <summary>
    /// A joker card from a standard 52-card deck.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In this implementation, the joker is considered to have no suit and no
    /// rank.
    /// </para>
    /// <para>
    /// More information about the joker can be found here:
    /// https://en.wikipedia.org/wiki/Joker_(playing_card)
    /// </para>
    /// </remarks>
    /// <seealso cref="StandardPlayingCard"/>
    /// <seealso cref="StandardPlayingCardDeck"/>
    public class Joker : StandardPlayingCard
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Joker"/> class.
        /// </summary>
        public Joker() : base(Rank.None, Suit.None) { }

        #endregion // End constructors region.
    }
}

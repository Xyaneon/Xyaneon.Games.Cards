using System;

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
    public class StandardPlayingCard : Card, IEquatable<StandardPlayingCard>
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

        #region IEquatable<StandardPlayingCard> implementation

        /// <summary>
        /// Indicates whether the current <see cref="StandardPlayingCard"/>
        /// is equal to another <see cref="StandardPlayingCard"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="StandardPlayingCard"/> to compare with this
        /// object.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the other
        /// <see cref="StandardPlayingCard"/> is equal to this
        /// <see cref="StandardPlayingCard"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <seealso cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(StandardPlayingCard other)
        {
            if (other == null)
            {
                return false;
            }

            return Rank.Equals(other.Rank) && Suit.Equals(other.Suit);
        }

        #endregion // End IEquatable<StandardPlayingCard> implementation region.

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

        #region Methods

        #region Public methods

        #endregion // End public methods region.

        #endregion // End methods region.

        #region Operators

        /// <summary>
        /// Determines whether two <see cref="StandardPlayingCard"/>
        /// instances are equal to each other.
        /// </summary>
        /// <param name="card1">
        /// The <see cref="StandardPlayingCard"/> on the left hand of the
        /// expression.
        /// </param>
        /// <param name="card2">
        /// The <see cref="StandardPlayingCard"/> on the right hand of the
        /// expression.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="card1"/> is equal to
        /// <paramref name="card2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(StandardPlayingCard card1, StandardPlayingCard card2)
        {
            if (ReferenceEquals(card1, card2))
            {
                return true;
            }

            if (card1 is null)
            {
                return false;
            }

            if (card2 is null)
            {
                return false;
            }

            return card1.Equals(card2);
        }

        /// <summary>
        /// Determines whether two <see cref="StandardPlayingCard"/>
        /// instances are not equal to each other.
        /// </summary>
        /// <param name="card1">
        /// The <see cref="StandardPlayingCard"/> on the left hand of the
        /// expression.
        /// </param>
        /// <param name="card2">
        /// The <see cref="StandardPlayingCard"/> on the right hand of the
        /// expression.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="card1"/> is not equal to
        /// <paramref name="card2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(StandardPlayingCard card1, StandardPlayingCard card2)
        {
            if (ReferenceEquals(card1, card2))
            {
                return false;
            }

            if (card1 is null)
            {
                return true;
            }

            if (card2 is null)
            {
                return true;
            }

            return !card1.Equals(card2);
        }

        #endregion // End operators region.
    }
}

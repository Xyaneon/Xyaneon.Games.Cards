namespace Xyaneon.Games.Cards.Test.Helpers
{
    /// <summary>
    /// Represents a card with an associated <see langword="int"/> value.
    /// </summary>
    public class IntCard : Card
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IntCard"/> class with
        /// the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The value of the card.
        /// </param>
        public IntCard(int value)
        {
            Value = value;
        }

        #endregion // End constructors region.

        #region Properties

        /// <summary>
        /// Gets the value of this <see cref="IntCard"/>.
        /// </summary>
        public int Value { get; }

        #endregion // End properties region.
    }
}

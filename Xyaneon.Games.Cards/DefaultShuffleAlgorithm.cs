using System;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.Games.Cards
{
    /// <summary>
    /// A default shuffling algorithm for cards.
    /// </summary>
    /// <typeparam name="TCard">
    /// The <see cref="Type"/> of cards this algorithm will be used to shuffle.
    /// </typeparam>
    /// <remarks>
    /// This class is deprecated, along with the
    /// <see cref="IShuffleAlgorithm{TCard}"/> interface itself. Instead, you
    /// should call the shuffling methods on <see cref="DrawPile{TCard}"/>
    /// that do not need a shuffling algorithm supplied to get the same
    /// default shuffling behavior.
    /// </remarks>
    [Obsolete("This class is deprecated along with the IShuffleAlgorithm interface. Instead, use DrawPile<TCard> shuffling methods without specifying a shuffling algorithm.")]
    public sealed class DefaultShuffleAlgorithm<TCard> : IShuffleAlgorithm<TCard> where TCard : Card
    {
        #region IShuffleAlgorithm<TCard> implementation

        /// <summary>
        /// Shuffles the provided collection of cards and returns them in a new
        /// <see cref="IList{T}"/> indicating the shuffled order.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle.
        /// </param>
        /// <returns>
        /// A new list containing the supplied <paramref name="cards"/>
        /// in shuffled order.
        /// </returns>
        public IList<TCard> Shuffle(IEnumerable<TCard> cards)
        {
            var random = new Random();
            return cards.OrderBy(c => random.Next()).ToList();
        }

        #endregion // End IShuffleAlgorithm<TCard> implementation region.
    }
}

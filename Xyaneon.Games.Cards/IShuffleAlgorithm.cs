using System;
using System.Collections.Generic;

namespace Xyaneon.Games.Cards
{
    /// <summary>
    /// Interface for classes which can be used for shuffling cards.
    /// </summary>
    /// <typeparam name="TCard">
    /// The <see cref="Type"/> of cards this algorithm will be used to shuffle.
    /// </typeparam>
    public interface IShuffleAlgorithm<TCard> where TCard : Card
    {
        #region Methods

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
        IList<TCard> Shuffle(IEnumerable<TCard> cards);

        #endregion // End methods region.
    }
}

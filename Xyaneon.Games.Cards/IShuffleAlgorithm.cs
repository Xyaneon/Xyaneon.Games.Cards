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
    /// <remarks>
    /// This interface is deprecated and will be removed in an upcoming major
    /// release. Instead, you should provide your own
    /// <see cref="ShuffleFunction{TCard}"/> delegate implementations to the
    /// shuffling methods on <see cref="DrawPile{TCard}"/> which accept one.
    /// </remarks>
    [Obsolete("This interface is deprecated. Instead, provide your own ShuffleFunction<TCard> delegate implementation to DrawPile<TCard> shuffling methods which accept one.")]
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

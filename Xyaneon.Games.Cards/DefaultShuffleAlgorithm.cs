﻿using System;
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

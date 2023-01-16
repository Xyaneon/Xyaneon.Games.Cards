using System;
using System.Collections.Generic;

namespace Xyaneon.Games.Cards
{
    /// <summary>
    /// Represents a collection of cards which can be drawn from.
    /// </summary>
    /// <typeparam name="TCard">
    /// The type of cards stored in this draw pile.
    /// Must inherit from the <see cref="Card"/> class.
    /// </typeparam>
    public interface IDrawPile<TCard> where TCard : Card
    {
        #region Properties

        /// <summary>
        /// Gets a read-only view of the cards currently in this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </summary>
        IReadOnlyList<TCard> Cards { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IDrawPile{TCard}"/>
        /// is currently empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IDrawPile{TCard}"/>
        /// is supposed to be face-up (<see langword="true"/>) or face-down
        /// (<see langword="false"/>).
        /// </summary>
        bool IsFaceUp { get; }

        #endregion // End properties region.

        #region Methods

        /// <summary>
        /// Draws a single <see cref="Card"/> from the top of this
        /// <see cref="IDrawPile{TCard}"/> and returns it.
        /// </summary>
        /// <returns>
        /// The <see cref="Card"/> which was at the top of this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The draw pile is empty.
        /// </exception>
        TCard Draw();

        /// <summary>
        /// Draws the specified number of cards from this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </summary>
        /// <param name="count">
        /// The number of cards to draw.
        /// </param>
        /// <returns>
        /// The drawn cards in the order in which they were drawn.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> exceeds the current number of cards stored
        /// in this <see cref="IDrawPile{TCard}"/>.
        /// </exception>
        IEnumerable<TCard> Draw(int count);

        /// <summary>
        /// Draws all cards from this <see cref="IDrawPile{TCard}"/>.
        /// </summary>
        /// <returns>
        /// The drawn cards in the order in which they were drawn.
        /// </returns>
        /// <remarks>
        /// After this method is called, this <see cref="IDrawPile{TCard}"/>
        /// will be empty.
        /// </remarks>
        IEnumerable<TCard> DrawAll();

        /// <summary>
        /// Draws the specified number of cards from this
        /// <see cref="IDrawPile{TCard}"/> if able, or all of the cards in this
        /// <see cref="IDrawPile{TCard}"/> if the current number of cards it
        /// currently holds is less than that.
        /// </summary>
        /// <param name="count">
        /// The maximum number of cards to draw.
        /// </param>
        /// <returns>
        /// The drawn cards in the order in which they were drawn. If this
        /// <see cref="IDrawPile{TCard}"/> is empty at the time of the call,
        /// then the returned collection will also be empty.
        /// </returns>
        IEnumerable<TCard> DrawAtMost(int count);

        /// <summary>
        /// Inserts a single <see cref="Card"/> into this
        /// <see cref="IDrawPile{TCard}"/> at the specified
        /// <paramref name="index"/>.
        /// </summary>
        /// <param name="index">
        /// The zero-based index where the <paramref name="card"/> should be
        /// inserted at.
        /// </param>
        /// <param name="card">
        /// The <see cref="Card"/> to insert.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="card"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero.
        /// -or-
        /// <paramref name="index"/> is greater than this object's current
        /// number of cards.
        /// </exception>
        void Insert(int index, TCard card);

        /// <summary>
        /// Places a single <see cref="Card"/> at the bottom of this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </summary>
        /// <param name="card">
        /// The <see cref="Card"/> to place at the bottom.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="card"/> is <see langword="null"/>.
        /// </exception>
        void PlaceAtBottom(TCard card);

        /// <summary>
        /// Places a single <see cref="Card"/> on top of this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </summary>
        /// <param name="card">
        /// The <see cref="Card"/> to place on top.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="card"/> is <see langword="null"/>.
        /// </exception>
        void PlaceOnTop(TCard card);

        /// <summary>
        /// Shuffles all of the cards in this <see cref="IDrawPile{TCard}"/>
        /// using a default shuffling algorithm.
        /// </summary>
        /// <remarks>
        /// If you want to use a custom shuffling method instead, then consider
        /// using the <see cref="Shuffle(IShuffleAlgorithm{TCard})"/> overload
        /// method.
        /// </remarks>
        void Shuffle();

        /// <summary>
        /// Shuffles all of the cards in this <see cref="IDrawPile{TCard}"/>
        /// using the supplied shuffling algorithm.
        /// </summary>
        /// <param name="shuffleAlgorithm">
        /// The object providing the shuffling algorithm to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="shuffleAlgorithm"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This interface method is deprecated, and will be replaced by a new
        /// one in an upcoming major release which will replace the usage of
        /// <see cref="IShuffleAlgorithm{TCard}"/> with a named delegate type.
        /// </remarks>
        [Obsolete("IShuffleAlgorithm will be removed in favor of a named delegate in an upcoming major release.")]
        void Shuffle(IShuffleAlgorithm<TCard> shuffleAlgorithm);

        /// <summary>
        /// Shuffles the provided draw pile into this
        /// <see cref="IDrawPile{TCard}"/> using a default shuffling algorithm.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="IDrawPile{TCard}"/> to shuffle into this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method will use the shuffling algorithm provided by the
        /// <see cref="DefaultShuffleAlgorithm{TCard}"/>. If you want to use
        /// a custom shuffling method instead, then consider using the
        /// <see cref="ShuffleIn(IEnumerable{TCard}, IShuffleAlgorithm{TCard})"/>
        /// overload method.
        /// </para>
        /// <para>
        /// <paramref name="other"/> will be emptied of all of its cards as a
        /// result of calling this algorithm.
        /// </para>
        /// </remarks>
        void ShuffleIn(IDrawPile<TCard> other);

        /// <summary>
        /// Shuffles the provided draw pile into this
        /// <see cref="IDrawPile{TCard}"/> using the supplied shuffling
        /// algorithm.
        /// </summary>
        /// <param name="other">
        /// The <see cref="IDrawPile{TCard}"/> to shuffle into this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </param>
        /// <param name="shuffleAlgorithm">
        /// The object providing the shuffling algorithm to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="shuffleAlgorithm"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="other"/> will be emptied of all of its cards as a
        /// result of calling this algorithm.
        /// </para>
        /// <para>
        /// This interface method is deprecated, and will be replaced by a new
        /// one in an upcoming major release which will replace the usage of
        /// <see cref="IShuffleAlgorithm{TCard}"/> with a named delegate type.
        /// </para>
        /// </remarks>
        [Obsolete("IShuffleAlgorithm will be removed in favor of a named delegate in an upcoming major release.")]
        void ShuffleIn(IDrawPile<TCard> other, IShuffleAlgorithm<TCard> shuffleAlgorithm);

        /// <summary>
        /// Shuffles the provided <paramref name="cards"/> into this
        /// <see cref="IDrawPile{TCard}"/> using a default shuffling algorithm.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle into this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cards"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This method will use the shuffling algorithm provided by the
        /// <see cref="DefaultShuffleAlgorithm{TCard}"/>. If you want to use
        /// a custom shuffling method instead, then consider using the
        /// <see cref="ShuffleIn(IEnumerable{TCard}, IShuffleAlgorithm{TCard})"/>
        /// overload method.
        /// </remarks>
        void ShuffleIn(IEnumerable<TCard> cards);

        /// <summary>
        /// Shuffles the provided <paramref name="cards"/> into this
        /// <see cref="IDrawPile{TCard}"/> using the supplied shuffling
        /// algorithm.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle into this
        /// <see cref="IDrawPile{TCard}"/>.
        /// </param>
        /// <param name="shuffleAlgorithm">
        /// The object providing the shuffling algorithm to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cards"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="shuffleAlgorithm"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This interface method is deprecated, and will be replaced by a new
        /// one in an upcoming major release which will replace the usage of
        /// <see cref="IShuffleAlgorithm{TCard}"/> with a named delegate type.
        /// </remarks>
        [Obsolete("IShuffleAlgorithm will be removed in favor of a named delegate in an upcoming major release.")]
        void ShuffleIn(IEnumerable<TCard> cards, IShuffleAlgorithm<TCard> shuffleAlgorithm);

        #endregion // End methods region.
    }
}
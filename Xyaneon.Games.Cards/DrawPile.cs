using System;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.Games.Cards
{
    /// <summary>
    /// A collection of cards which can be drawn from.
    /// </summary>
    /// <typeparam name="TCard">
    /// The type of cards stored in this draw pile.
    /// Must inherit from the <see cref="Card"/> class.
    /// </typeparam>
    /// <remarks>
    /// This class provides a concrete implementation of the
    /// <see cref="IDrawPile{TCard}"/> interface.
    /// </remarks>
    /// <seealso cref="IDrawPile{TCard}"/>
    /// <seealso cref="ShuffleFunction{TCard}"/>
    public class DrawPile<TCard> : IDrawPile<TCard> where TCard : Card
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawPile{TCard}"/> class
        /// using the provided visibility.
        /// </summary>
        /// <param name="isFaceUp">
        /// A value indicating whether this <see cref="DrawPile{TCard}"/>
        /// is supposed to be face-up (<see langword="true"/>) or face-down
        /// (<see langword="false"/>).
        /// </param>
        public DrawPile(bool isFaceUp = false)
        {
            _cards = new Stack<TCard>();
            IsFaceUp = isFaceUp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawPile{TCard}"/> class
        /// using the provided collection of cards and visibility.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards initially present in this draw pile.
        /// </param>
        /// <param name="isFaceUp">
        /// A value indicating whether this <see cref="DrawPile{TCard}"/>
        /// is supposed to be face-up (<see langword="true"/>) or face-down
        /// (<see langword="false"/>).
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cards"/> is <see langword="null"/>.
        /// </exception>
        public DrawPile(IEnumerable<TCard> cards, bool isFaceUp = false)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards), "The collection of cards to create the draw pile from cannot be null.");
            }

            _cards = new Stack<TCard>(cards);
            IsFaceUp = isFaceUp;
        }

        #endregion // End constructors region.

        #region IDrawPile<TCard> implementation

        #region Properties

        /// <summary>
        /// Gets a read-only view of the cards currently in this
        /// <see cref="DrawPile{TCard}"/>.
        /// </summary>
        public IReadOnlyList<TCard> Cards
        {
            get => _cards.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DrawPile{TCard}"/>
        /// is currently empty.
        /// </summary>
        public bool IsEmpty
        {
            get => _cards.Count == 0;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DrawPile{TCard}"/>
        /// is supposed to be face-up (<see langword="true"/>) or face-down
        /// (<see langword="false"/>).
        /// </summary>
        public bool IsFaceUp { get; }

        #endregion // End properties region.

        #region Methods

        /// <summary>
        /// Draws a single <see cref="Card"/> from the top of this
        /// <see cref="DrawPile{TCard}"/> and returns it.
        /// </summary>
        /// <returns>
        /// The <see cref="Card"/> which was at the top of this
        /// <see cref="DrawPile{TCard}"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The draw pile is empty.
        /// </exception>
        public TCard Draw()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("There are no cards left to draw.");
            }

            return _cards.Pop();
        }

        /// <summary>
        /// Draws the specified number of cards from this
        /// <see cref="DrawPile{TCard}"/>.
        /// </summary>
        /// <param name="count">
        /// The number of cards to draw.
        /// </param>
        /// <returns>
        /// The drawn cards in the order in which they were drawn.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> exceeds the current number of cards stored
        /// in this <see cref="DrawPile{TCard}"/>.
        /// </exception>
        public IEnumerable<TCard> Draw(int count)
        {
            if (count > _cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Too few cards to draw in this draw pile.");
            }

            var drawnCards = new Queue<TCard>(count);
            for (int i = 0; i < count; i++)
            {
                drawnCards.Enqueue(Draw());
            }

            return drawnCards;
        }

        /// <summary>
        /// Draws all cards from this <see cref="DrawPile{TCard}"/>.
        /// </summary>
        /// <returns>
        /// The drawn cards in the order in which they were drawn.
        /// </returns>
        /// <remarks>
        /// After this method is called, this <see cref="DrawPile{TCard}"/>
        /// will be empty.
        /// </remarks>
        public IEnumerable<TCard> DrawAll()
        {
            var drawnCards = new Queue<TCard>(_cards.Count);
            while (_cards.Count > 0)
            {
                drawnCards.Enqueue(Draw());
            }

            return drawnCards;
        }

        /// <summary>
        /// Draws the specified number of cards from this
        /// <see cref="DrawPile{TCard}"/> if able, or all of the cards in this
        /// <see cref="DrawPile{TCard}"/> if the current number of cards it
        /// currently holds is less than that.
        /// </summary>
        /// <param name="count">
        /// The maximum number of cards to draw.
        /// </param>
        /// <returns>
        /// The drawn cards in the order in which they were drawn. If this
        /// <see cref="DrawPile{TCard}"/> is empty at the time of the call,
        /// then the returned collection will also be empty.
        /// </returns>
        public IEnumerable<TCard> DrawAtMost(int count)
        {
            var drawnCards = new Queue<TCard>(count);
            for (int i = 0; i < count && _cards.Count > 0; i++)
            {
                drawnCards.Enqueue(Draw());
            }

            return drawnCards;
        }

        /// <summary>
        /// Inserts a single <see cref="Card"/> into this
        /// <see cref="DrawPile{TCard}"/> at the specified
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
        public void Insert(int index, TCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), "The card to insert into the draw pile cannot be null.");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "The position to insert the card into the draw pile at cannot be less than zero.");
            }

            if (index > _cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "The position to insert the card into the draw pile at cannot be greater than the number of cards in the draw pile.");
            }

            List<TCard> cards = _cards.ToList();
            cards.Insert(index, card);
            _cards = new Stack<TCard>(cards);
        }

        /// <summary>
        /// Places a single <see cref="Card"/> at the bottom of this
        /// <see cref="DrawPile{TCard}"/>.
        /// </summary>
        /// <param name="card">
        /// The <see cref="Card"/> to place at the bottom.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="card"/> is <see langword="null"/>.
        /// </exception>
        public void PlaceAtBottom(TCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), "The card to place at the bottom of the draw pile cannot be null.");
            }
            _cards = new Stack<TCard>(new TCard[] { card }.Concat(_cards.Reverse()));
        }

        /// <summary>
        /// Places a single <see cref="Card"/> on top of this
        /// <see cref="DrawPile{TCard}"/>.
        /// </summary>
        /// <param name="card">
        /// The <see cref="Card"/> to place on top.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="card"/> is <see langword="null"/>.
        /// </exception>
        public void PlaceOnTop(TCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), "The card to place on top of the draw pile cannot be null.");
            }

            _cards.Push(card);
        }

        /// <summary>
        /// Shuffles all of the cards in this <see cref="DrawPile{TCard}"/>.
        /// </summary>
        /// <remarks>
        /// This method will use a default shuffling algorithm.
        /// If you want to use a custom shuffling method instead, then
        /// consider using the <see cref="Shuffle(ShuffleFunction{TCard})"/>
        /// overload method.
        /// </remarks>
        public void Shuffle()
        {
            ShuffleBase(DefaultShuffleAlgorithm);
        }

        /// <summary>
        /// Shuffles all of the cards in this <see cref="DrawPile{TCard}"/>
        /// using the supplied shuffling algorithm.
        /// </summary>
        /// <param name="shuffleAlgorithm">
        /// The delegate providing the shuffling algorithm to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="shuffleAlgorithm"/> is <see langword="null"/>.
        /// </exception>
        public void Shuffle(ShuffleFunction<TCard> shuffleAlgorithm)
        {
            if (shuffleAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(shuffleAlgorithm), "The shuffling algorithm to use cannot be null.");
            }

            ShuffleBase(shuffleAlgorithm);
        }

        /// <summary>
        /// Shuffles the provided draw pile into this
        /// <see cref="DrawPile{TCard}"/> using a default shuffling algorithm.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="IDrawPile{TCard}"/> to shuffle into this
        /// <see cref="DrawPile{TCard}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method will use a default shuffling algorithm. If you want to
        /// use a custom shuffling method instead, then consider using the
        /// <see cref="ShuffleIn(IEnumerable{TCard}, ShuffleFunction{TCard})"/>
        /// overload method.
        /// </para>
        /// <para>
        /// <paramref name="other"/> will be emptied of all of its cards as a
        /// result of calling this algorithm.
        /// </para>
        /// </remarks>
        public void ShuffleIn(IDrawPile<TCard> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "The draw pile to shuffle into this draw pile cannot be null.");
            }

            ShuffleInBase(other, DefaultShuffleAlgorithm);
        }

        /// <summary>
        /// Shuffles the provided draw pile into this
        /// <see cref="DrawPile{TCard}"/> using the supplied shuffling
        /// algorithm.
        /// </summary>
        /// <param name="other">
        /// The <see cref="IDrawPile{TCard}"/> to shuffle into this
        /// <see cref="DrawPile{TCard}"/>.
        /// </param>
        /// <param name="shuffleAlgorithm">
        /// The delegate providing the shuffling algorithm to use.
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
        /// </remarks>
        public void ShuffleIn(IDrawPile<TCard> other, ShuffleFunction<TCard> shuffleAlgorithm)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "The draw pile to shuffle into this draw pile cannot be null.");
            }

            if (shuffleAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(shuffleAlgorithm), "The shuffling algorithm to use cannot be null.");
            }

            ShuffleInBase(other, shuffleAlgorithm);
        }

        /// <summary>
        /// Shuffles the provided <paramref name="cards"/> into this
        /// <see cref="DrawPile{TCard}"/> using a default shuffling algorithm.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle into this
        /// <see cref="DrawPile{TCard}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cards"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This method will use the default shuffling algorithm. If you want
        /// to use a custom shuffling method instead, then consider using the
        /// <see cref="ShuffleIn(IEnumerable{TCard}, ShuffleFunction{TCard})"/>
        /// overload method.
        /// </remarks>
        public void ShuffleIn(IEnumerable<TCard> cards)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards), "The collection of cards to shuffle into this draw pile cannot be null.");
            }

            ShuffleInBase(cards, DefaultShuffleAlgorithm);
        }

        /// <summary>
        /// Shuffles the provided <paramref name="cards"/> into this
        /// <see cref="DrawPile{TCard}"/> using the supplied shuffling
        /// algorithm.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle into this
        /// <see cref="DrawPile{TCard}"/>.
        /// </param>
        /// <param name="shuffleAlgorithm">
        /// The delegate providing the shuffling algorithm to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cards"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="shuffleAlgorithm"/> is <see langword="null"/>.
        /// </exception>
        public void ShuffleIn(IEnumerable<TCard> cards, ShuffleFunction<TCard> shuffleAlgorithm)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards), "The collection of cards to shuffle into this draw pile cannot be null.");
            }

            if (shuffleAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(shuffleAlgorithm), "The shuffling algorithm to use cannot be null.");
            }

            ShuffleInBase(cards, shuffleAlgorithm);
        }

        #endregion // End methods region.

        #endregion // End IDrawPile<TCard> implementation region.

        #region Fields

        private Stack<TCard> _cards;

        #endregion // End fields region.

        #region Methods

        #region Private methods

        private static IList<TCard> DefaultShuffleAlgorithm(IEnumerable<TCard> cards)
        {
            var random = new Random();
            return cards.OrderBy(c => random.Next()).ToList();
        }

        private void ShuffleBase(ShuffleFunction<TCard> shuffleAlgorithm)
        {
            IList<TCard> shuffledCards = shuffleAlgorithm(_cards);
            _cards = new Stack<TCard>(shuffledCards);
        }

        private void ShuffleInBase(IDrawPile<TCard> other, ShuffleFunction<TCard> shuffleAlgorithm)
        {
            IEnumerable<TCard> cardsToShuffle = _cards.Concat(other.DrawAll());
            IList<TCard> shuffledCards = shuffleAlgorithm(cardsToShuffle);
            _cards = new Stack<TCard>(shuffledCards);
        }

        private void ShuffleInBase(IEnumerable<TCard> cards, ShuffleFunction<TCard> shuffleAlgorithm)
        {
            IEnumerable<TCard> cardsToShuffle = _cards.Concat(cards);
            IList<TCard> shuffledCards = shuffleAlgorithm(cardsToShuffle);
            _cards = new Stack<TCard>(shuffledCards);
        }

        #endregion // End private methods region.

        #endregion // End methods region.
    }
}

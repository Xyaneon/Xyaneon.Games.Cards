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
    public class DrawPile<TCard> where TCard : Card
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

        #region Fields

        /// <summary>
        /// Private backing field for the <see cref="Cards"/> property.
        /// </summary>
        private Stack<TCard> _cards;

        #endregion // End fields region.

        #region Methods

        #region Public methods

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

            _cards = new Stack<TCard>(_cards.Concat(new TCard[] { card }));
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
        /// Shuffles all of the cards in this <see cref="DrawPile{TCard}"/>
        /// using a default shuffling algorithm.
        /// </summary>
        public void Shuffle()
        {
            ShuffleBase(DefaultShuffleAlgorithm);
        }

        /// <summary>
        /// Shuffles all of the cards in this <see cref="DrawPile{TCard}"/>
        /// using the supplied shuffling algorithm.
        /// </summary>
        /// <param name="shuffleAlgorithm">
        /// The delegate to use which implements the custom shuffling
        /// algorithm.
        /// </param>
        public void Shuffle(Func<IEnumerable<TCard>, IList<TCard>> shuffleAlgorithm)
        {
            ShuffleBase(shuffleAlgorithm);
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
        /// The delegate to use which implements the custom shuffling
        /// algorithm.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cards"/> is <see langword="null"/>.
        /// </exception>
        public void ShuffleIn(IEnumerable<TCard> cards, Func<IEnumerable<TCard>, IList<TCard>> shuffleAlgorithm)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards), "The collection of cards to shuffle into this draw pile cannot be null.");
            }

            ShuffleInBase(cards, shuffleAlgorithm);
        }

        #endregion // End public methods region.

        #region Private methods

        /// <summary>
        /// Provides the default shuffling algorithm used by this class.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle.
        /// </param>
        /// <returns>
        /// A new list containing the supplied <paramref name="cards"/>
        /// in shuffled order.
        /// </returns>
        private static IList<TCard> DefaultShuffleAlgorithm(IEnumerable<TCard> cards)
        {
            var random = new Random();
            return cards.OrderBy(c => random.Next()).ToList();
        }

        /// <summary>
        /// The base method for shuffling the cards stored in this object.
        /// The <paramref name="shuffleAlgorithm"/> must always be specified.
        /// </summary>
        /// <param name="shuffleAlgorithm">
        /// The delegate to use which implements the desired shuffling
        /// algorithm.
        /// </param>
        /// <remarks>
        /// The <paramref name="shuffleAlgorithm"/> supplied can just be the
        /// <see cref="DefaultShuffleAlgorithm(IEnumerable{TCard})"/> method
        /// defined within this class for default behavior, such as when called
        /// by the public <see cref="Shuffle()"/> method. However, an
        /// alternative can be supplied when needed.
        /// </remarks>
        private void ShuffleBase(Func<IEnumerable<TCard>, IList<TCard>> shuffleAlgorithm)
        {
            IList<TCard> shuffledCards = shuffleAlgorithm(_cards);
            _cards = new Stack<TCard>(shuffledCards);
        }

        /// <summary>
        /// The base method for shuffling the supplied <paramref name="cards"/>
        /// into this <see cref="DrawPile{TCard}"/>. The
        /// <paramref name="shuffleAlgorithm"/> must always be specified.
        /// </summary>
        /// <param name="cards">
        /// The collection of cards to shuffle into this
        /// <see cref="DrawPile{TCard}"/>.
        /// </param>
        /// <param name="shuffleAlgorithm">
        /// The delegate to use which implements the desired shuffling
        /// algorithm.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method assumes that the <paramref name="cards"/> parameter
        /// supplied was already checked to not be <see langword="null"/> by
        /// the caller.
        /// </para>
        /// <para>
        /// The <paramref name="shuffleAlgorithm"/> supplied can just be the
        /// <see cref="DefaultShuffleAlgorithm(IEnumerable{TCard})"/> method
        /// defined within this class for default behavior, such as when called
        /// by the public <see cref="Shuffle()"/> method. However, an
        /// alternative can be supplied when needed.
        /// </para>
        /// </remarks>
        private void ShuffleInBase(IEnumerable<TCard> cards, Func<IEnumerable<TCard>, IList<TCard>> shuffleAlgorithm)
        {
            IEnumerable<TCard> cardsToShuffle = _cards.Concat(cards);
            IList<TCard> shuffledCards = shuffleAlgorithm(cardsToShuffle);
            _cards = new Stack<TCard>(shuffledCards);
        }

        #endregion // End private methods region.

        #endregion // End methods region.
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Games.Cards.Test.Extensions;
using Xyaneon.Games.Cards.Test.Helpers;

namespace Xyaneon.Games.Cards.Test
{
    /// <summary>
    /// Provides unit testing methods for the <see cref="DrawPile{TCard}"/>
    /// class.
    /// </summary>
    [TestClass]
    public class DrawPileTests
    {
        /// <summary>
        /// Tests basic initialization of the <see cref="DrawPile{TCard}"/>
        /// class.
        /// </summary>
        [TestMethod]
        public void DrawPile_BasicInitializationTest()
        {
            // Arrange.
            const bool expectedFaceUp = true;
            const bool expectedFaceDown = false;
            DrawPile<Card> defaultDrawPile;
            DrawPile<Card> faceUpDrawPile;
            DrawPile<Card> faceDownDrawPile;

            // Act.
            defaultDrawPile = new DrawPile<Card>();
            faceUpDrawPile = new DrawPile<Card>(expectedFaceUp);
            faceDownDrawPile = new DrawPile<Card>(expectedFaceDown);

            // Assert.
            Assert.AreEqual(expectedFaceDown, defaultDrawPile.IsFaceUp);
            Assert.AreEqual(expectedFaceUp, faceUpDrawPile.IsFaceUp);
            Assert.AreEqual(expectedFaceDown, faceDownDrawPile.IsFaceUp);
            foreach (var drawPile in new DrawPile<Card>[] { defaultDrawPile, faceUpDrawPile, faceDownDrawPile })
            {
                Assert.That.DrawPileIsEmpty(drawPile);
            }
        }

        /// <summary>
        /// Tests basic shuffling of the <see cref="DrawPile{TCard}"/> class.
        /// </summary>
        [TestMethod]
        public void DrawPile_BasicShuffleTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var expectedCardSet = new HashSet<IntCard>(cards);
            HashSet<IntCard> actualCardSet;
            var drawPile = new DrawPile<IntCard>(cards);

            // Act.
            drawPile.Shuffle();
            actualCardSet = new HashSet<IntCard>(drawPile.Cards);

            // Assert.
            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
        }

        /// <summary>
        /// Ensures custom shuffling of the <see cref="DrawPile{TCard}"/> class
        /// will not accept a null delegate.
        /// </summary>
        [TestMethod]
        public void DrawPile_CustomShuffleAlgorithmShouldRejectNullTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.Shuffle((ShuffleFunction<IntCard>)null);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The shuffling algorithm to use cannot be null.");
        }

        /// <summary>
        /// Tests custom shuffling of the <see cref="DrawPile{TCard}"/> class.
        /// </summary>
        [TestMethod]
        public void DrawPile_CustomShuffleAlgorithmTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var expectedCardSet = new HashSet<IntCard>(cards);
            List<IntCard> actualCardList;
            var drawPile = new DrawPile<IntCard>(cards);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();
            var expectedCardList = cards.Reverse().ToList();

            // Act.
            drawPile.Shuffle(shuffleAlgorithm);
            actualCardList = drawPile.Cards.ToList();

            // Assert.
            Assert.That.CardListsAreEqual(expectedCardList, actualCardList);
        }

        /// <summary>
        /// Ensures the <see cref="DrawPile{TCard}.ShuffleIn(IDrawPile{TCard})"/>
        /// method rejects a null <see cref="DrawPile{TCard}"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInDrawPileShouldRejectNullDrawPileTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IDrawPile<IntCard>)null);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The draw pile to shuffle into this draw pile cannot be null.");
        }

        /// <summary>
        /// Tests the <see cref="DrawPile{TCard}.ShuffleIn(IDrawPile{TCard})"/>
        /// functionality with a valid <see cref="DrawPile{TCard}"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInDrawPileTest()
        {
            // Arrange.
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile1 = new DrawPile<IntCard>(cards1);
            var drawPile2 = new DrawPile<IntCard>(cards2);
            HashSet<IntCard> actualCardSet;
            var expectedCardSet = cards1.Concat(cards2).ToHashSet();

            // Act.
            drawPile1.ShuffleIn(drawPile2);
            actualCardSet = drawPile1.Cards.ToHashSet();

            // Assert.
            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
            Assert.AreEqual(expectedCardSet.Count, drawPile1.Cards.Count);
            Assert.That.DrawPileIsEmpty(drawPile2);
        }

        /// <summary>
        /// Ensures the <see cref="DrawPile{TCard}.ShuffleIn(IDrawPile{TCard}, ShuffleFunction{TCard})"/>
        /// method rejects a null <see cref="DrawPile{TCard}"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInCustomDrawPileShouldRejectNullDrawPileTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IDrawPile<IntCard>)null, shuffleAlgorithm);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The draw pile to shuffle into this draw pile cannot be null.");
        }

        /// <summary>
        /// Ensures the <see cref="DrawPile{TCard}.ShuffleIn(IDrawPile{TCard}, ShuffleFunction{TCard})"/>
        /// method rejects a null <see cref="ShuffleFunction{TCard})"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInCustomDrawPileShouldRejectNullAlgorithmTest()
        {
            // Arrange.
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile1 = new DrawPile<IntCard>(cards1);
            var drawPile2 = new DrawPile<IntCard>(cards2);

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile1.ShuffleIn(drawPile2, (ShuffleFunction<IntCard>)null);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The shuffling algorithm to use cannot be null.");
        }

        /// <summary>
        /// Tests the <see cref="DrawPile{TCard}.ShuffleIn(IDrawPile{TCard}, ShuffleFunction{TCard})"/>
        /// functionality with valid arguments.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInCustomDrawPileTest()
        {
            // Arrange.
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile1 = new DrawPile<IntCard>(cards1);
            var drawPile2 = new DrawPile<IntCard>(cards2);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();
            var expectedCardList = cards2.Concat(cards1).Reverse().ToList();

            // Act.
            drawPile1.ShuffleIn(drawPile2, shuffleAlgorithm);
            var actualCardList = drawPile1.Cards.ToList();

            // Assert.
            Assert.That.CardListsAreEqual(expectedCardList, actualCardList);
            Assert.That.DrawPileIsEmpty(drawPile2);
        }

        /// <summary>
        /// Ensures the <see cref="DrawPile{TCard}.ShuffleIn(IEnumerable{TCard})"/>
        /// method rejects a null <see cref="IEnumerable{TCard}"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInEnumerableShouldRejectNullEnumerableTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IEnumerable<IntCard>)null);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The collection of cards to shuffle into this draw pile cannot be null.");
        }

        /// <summary>
        /// Tests the <see cref="DrawPile{TCard}.ShuffleIn(IEnumerable{TCard})"/>
        /// functionality with a valid <see cref="IEnumerable{TCard}"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInEnumerableTest()
        {
            // Arrange.
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile = new DrawPile<IntCard>(cards1);
            HashSet<IntCard> actualCardSet;
            var expectedCardSet = cards1.Concat(cards2).ToHashSet();

            // Act.
            drawPile.ShuffleIn(cards2);
            actualCardSet = drawPile.Cards.ToHashSet();

            // Assert.
            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
            Assert.AreEqual(expectedCardSet.Count, drawPile.Cards.Count);
        }

        /// <summary>
        /// Ensures the <see cref="DrawPile{TCard}.ShuffleIn(IEnumerable{TCard}, ShuffleFunction{TCard})"/>
        /// method rejects a null <see cref="IEnumerable{TCard}"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInCustomEnumerableShouldRejectNullEnumerableTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IEnumerable<IntCard>)null, shuffleAlgorithm);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The collection of cards to shuffle into this draw pile cannot be null.");
        }

        /// <summary>
        /// Ensures the <see cref="DrawPile{TCard}.ShuffleIn(IEnumerable{TCard}, ShuffleFunction{TCard})"/>
        /// method rejects a null <see cref="ShuffleFunction{TCard})"/> argument.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInCustomEnumerableShouldRejectNullAlgorithmTest()
        {
            // Arrange.
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile = new DrawPile<IntCard>(cards1);

            // Act.
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn(cards2, (ShuffleFunction<IntCard>)null);
            });

            // Assert.
            Assert.That.ExceptionMessageStartsWith(actualException, "The shuffling algorithm to use cannot be null.");
        }

        /// <summary>
        /// Tests the <see cref="DrawPile{TCard}.ShuffleIn(IEnumerable{TCard}, ShuffleFunction{TCard})"/>
        /// functionality with valid arguments.
        /// </summary>
        [TestMethod]
        public void DrawPile_ShuffleInCustomEnumerableTest()
        {
            // Arrange.
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile = new DrawPile<IntCard>(cards1);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();
            var expectedCardList = cards2.Reverse().Concat(cards1).Reverse().ToList();

            // Act.
            drawPile.ShuffleIn(cards2, shuffleAlgorithm);
            var actualCardList = drawPile.Cards;

            // Assert.
            Assert.That.CardListsAreEqual(expectedCardList, actualCardList);
        }

        /// <summary>
        /// Tests the <see cref="DrawPile{TCard}.DrawAll"/> method.
        /// </summary>
        [TestMethod]
        public void DrawPile_DrawAllTest()
        {
            // Arrange.
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var expectedCardSet = new HashSet<IntCard>(cards);
            HashSet<IntCard> actualCardSet;
            var drawPile = new DrawPile<IntCard>(cards);
            IEnumerable<IntCard> drawnCards;

            // Act.
            actualCardSet = new HashSet<IntCard>(drawPile.Cards);
            drawnCards = drawPile.DrawAll();

            // Assert.
            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
            Assert.That.CardSetsAreEqual(expectedCardSet, new HashSet<IntCard>(drawnCards));
            Assert.That.DrawPileIsEmpty(drawPile);
        }

        /// <summary>
        /// Tests the <see cref="DrawPile{TCard}.PlaceAtBottom"/> method.
        /// </summary>
        [TestMethod]
        public void DrawPile_PlaceAtBottomTest()
        {
            // Arrange.
            var expectedCards = new IntCard[]
            {
                // 3 (new top), 2, 1, 4 (new bottom)
                new IntCard(3), new IntCard(2), new IntCard(1), new IntCard(4)
            };
            var cards = new IntCard[]
            {
                // 1 (old bottom), 2, 3, 4 (old top)
                new IntCard(1), new IntCard(2), new IntCard(3), new IntCard(4)
            };
            var drawPile = new DrawPile<IntCard>(cards);
            List<IntCard> actualCards;

            // Act.
            var drawnCard = drawPile.Draw();
            drawPile.PlaceAtBottom(drawnCard); // place card with value 4 at the bottom
            actualCards = new List<IntCard>(drawPile.Cards);

            // Assert.
            // Use collections assertions for lists because HashSet is unable to compare different sets.
            // Otherwise, GetHashCode and Equals methods would have to be tamed.
            CollectionAssert.AreEqual(expectedCards, actualCards, Comparer<IntCard>.Create((a, b) =>
            {
                return a.Value - b.Value;
            }));
        }
    }
}

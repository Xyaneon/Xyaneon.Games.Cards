using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Games.Cards.Test.Extensions;
using Xyaneon.Games.Cards.Test.Helpers;

namespace Xyaneon.Games.Cards.Test
{
    [TestClass]
    public class DrawPileTests
    {
        [TestMethod]
        public void DrawPile_BasicInitializationTest()
        {
            const bool expectedFaceUp = true;
            const bool expectedFaceDown = false;
            DrawPile<Card> defaultDrawPile;
            DrawPile<Card> faceUpDrawPile;
            DrawPile<Card> faceDownDrawPile;

            defaultDrawPile = new DrawPile<Card>();
            faceUpDrawPile = new DrawPile<Card>(expectedFaceUp);
            faceDownDrawPile = new DrawPile<Card>(expectedFaceDown);

            Assert.AreEqual(expectedFaceDown, defaultDrawPile.IsFaceUp);
            Assert.AreEqual(expectedFaceUp, faceUpDrawPile.IsFaceUp);
            Assert.AreEqual(expectedFaceDown, faceDownDrawPile.IsFaceUp);
            foreach (var drawPile in new DrawPile<Card>[] { defaultDrawPile, faceUpDrawPile, faceDownDrawPile })
            {
                Assert.That.DrawPileIsEmpty(drawPile);
            }
        }

        [TestMethod]
        public void DrawPile_CardsConstructor_ShouldRejectNullCardsCollection()
        {
            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                _ = new DrawPile<Card>(null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The collection of cards to create the draw pile from cannot be null.");
        }

        [TestMethod]
        public void DrawPile_BasicShuffleTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var expectedCardSet = new HashSet<IntCard>(cards);
            HashSet<IntCard> actualCardSet;
            var drawPile = new DrawPile<IntCard>(cards);

            drawPile.Shuffle();
            actualCardSet = new HashSet<IntCard>(drawPile.Cards);

            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
        }

        [TestMethod]
        public void DrawPile_CustomShuffleAlgorithmShouldRejectNullTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.Shuffle((ShuffleFunction<IntCard>)null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The shuffling algorithm to use cannot be null.");
        }

        [TestMethod]
        public void DrawPile_CustomShuffleAlgorithmTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var expectedCardSet = new HashSet<IntCard>(cards);
            List<IntCard> actualCardList;
            var drawPile = new DrawPile<IntCard>(cards);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();
            var expectedCardList = cards.Reverse().ToList();

            drawPile.Shuffle(shuffleAlgorithm);
            actualCardList = drawPile.Cards.ToList();

            Assert.That.CardListsAreEqual(expectedCardList, actualCardList);
        }

        [TestMethod]
        public void DrawPile_ShuffleInDrawPileShouldRejectNullDrawPileTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IDrawPile<IntCard>)null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The draw pile to shuffle into this draw pile cannot be null.");
        }

        [TestMethod]
        public void DrawPile_ShuffleInDrawPileTest()
        {
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile1 = new DrawPile<IntCard>(cards1);
            var drawPile2 = new DrawPile<IntCard>(cards2);
            HashSet<IntCard> actualCardSet;
            var expectedCardSet = cards1.Concat(cards2).ToHashSet();

            drawPile1.ShuffleIn(drawPile2);
            actualCardSet = drawPile1.Cards.ToHashSet();

            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
            Assert.AreEqual(expectedCardSet.Count, drawPile1.Cards.Count);
            Assert.That.DrawPileIsEmpty(drawPile2);
        }

        [TestMethod]
        public void DrawPile_ShuffleInCustomDrawPileShouldRejectNullDrawPileTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IDrawPile<IntCard>)null, shuffleAlgorithm);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The draw pile to shuffle into this draw pile cannot be null.");
        }

        [TestMethod]
        public void DrawPile_ShuffleInCustomDrawPileShouldRejectNullAlgorithmTest()
        {
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile1 = new DrawPile<IntCard>(cards1);
            var drawPile2 = new DrawPile<IntCard>(cards2);

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile1.ShuffleIn(drawPile2, (ShuffleFunction<IntCard>)null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The shuffling algorithm to use cannot be null.");
        }

        [TestMethod]
        public void DrawPile_ShuffleInCustomDrawPileTest()
        {
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile1 = new DrawPile<IntCard>(cards1);
            var drawPile2 = new DrawPile<IntCard>(cards2);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();
            var expectedCardList = cards2.Concat(cards1).Reverse().ToList();

            drawPile1.ShuffleIn(drawPile2, shuffleAlgorithm);
            var actualCardList = drawPile1.Cards.ToList();

            Assert.That.CardListsAreEqual(expectedCardList, actualCardList);
            Assert.That.DrawPileIsEmpty(drawPile2);
        }

        [TestMethod]
        public void DrawPile_ShuffleInEnumerableShouldRejectNullEnumerableTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IEnumerable<IntCard>)null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The collection of cards to shuffle into this draw pile cannot be null.");
        }

        [TestMethod]
        public void DrawPile_ShuffleInEnumerableTest()
        {
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile = new DrawPile<IntCard>(cards1);
            HashSet<IntCard> actualCardSet;
            var expectedCardSet = cards1.Concat(cards2).ToHashSet();

            drawPile.ShuffleIn(cards2);
            actualCardSet = drawPile.Cards.ToHashSet();

            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
            Assert.AreEqual(expectedCardSet.Count, drawPile.Cards.Count);
        }

        [TestMethod]
        public void DrawPile_ShuffleInCustomEnumerableShouldRejectNullEnumerableTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn((IEnumerable<IntCard>)null, shuffleAlgorithm);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The collection of cards to shuffle into this draw pile cannot be null.");
        }

        [TestMethod]
        public void DrawPile_ShuffleInCustomEnumerableShouldRejectNullAlgorithmTest()
        {
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile = new DrawPile<IntCard>(cards1);

            var actualException = Assert.ThrowsException<ArgumentNullException>(() => {
                drawPile.ShuffleIn(cards2, (ShuffleFunction<IntCard>)null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The shuffling algorithm to use cannot be null.");
        }

        [TestMethod]
        public void DrawPile_ShuffleInCustomEnumerableTest()
        {
            var cards1 = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var cards2 = new IntCard[] { new IntCard(4), new IntCard(5) };
            var drawPile = new DrawPile<IntCard>(cards1);
            // For the custom shuffling algorithm, simply reverse the existing
            // card order as a predictable way of determining the shuffling was
            // done correctly in a unit testing context.
            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();
            var expectedCardList = cards2.Reverse().Concat(cards1).Reverse().ToList();

            drawPile.ShuffleIn(cards2, shuffleAlgorithm);
            var actualCardList = drawPile.Cards;

            Assert.That.CardListsAreEqual(expectedCardList, actualCardList);
        }

        [TestMethod]
        public void DrawPile_DrawAllTest()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var expectedCardSet = new HashSet<IntCard>(cards);
            HashSet<IntCard> actualCardSet;
            var drawPile = new DrawPile<IntCard>(cards);
            IEnumerable<IntCard> drawnCards;

            actualCardSet = new HashSet<IntCard>(drawPile.Cards);
            drawnCards = drawPile.DrawAll();

            Assert.AreEqual(expectedCardSet.Count, actualCardSet.Count);
            Assert.That.CardSetsAreEqual(expectedCardSet, actualCardSet);
            Assert.That.CardSetsAreEqual(expectedCardSet, new HashSet<IntCard>(drawnCards));
            Assert.That.DrawPileIsEmpty(drawPile);
        }

        [TestMethod]
        public void DrawPile_PlaceAtBottomTest()
        {
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

            var drawnCard = drawPile.Draw();
            drawPile.PlaceAtBottom(drawnCard); // place card with value 4 at the bottom
            actualCards = new List<IntCard>(drawPile.Cards);

            // Use collections assertions for lists because HashSet is unable to compare different sets.
            // Otherwise, GetHashCode and Equals methods would have to be tamed.
            CollectionAssert.AreEqual(expectedCards, actualCards, Comparer<IntCard>.Create((a, b) =>
            {
                return a.Value - b.Value;
            }));
        }
    }
}

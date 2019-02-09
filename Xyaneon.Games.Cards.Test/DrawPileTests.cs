using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
                Assert.IsTrue(drawPile.IsEmpty);
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
            Assert.IsTrue(expectedCardSet.SetEquals(actualCardSet));
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
            Assert.IsTrue(expectedCardSet.SetEquals(actualCardSet));
            Assert.IsTrue(expectedCardSet.SetEquals(new HashSet<IntCard>(drawnCards)));
            Assert.AreEqual(0, drawPile.Cards.Count);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xyaneon.Games.Cards.StandardPlayingCards;

namespace Xyaneon.Games.Cards.Test
{
    /// <summary>
    /// Provides unit testing methods for the
    /// <see cref="StandardPlayingCardDeck"/> class.
    /// </summary>
    [TestClass]
    public class StandardPlayingCardDeckTests
    {
        /// <summary>
        /// Tests basic initialization of the
        /// <see cref="StandardPlayingCardDeck"/> class.
        /// </summary>
        [TestMethod]
        public void StandardPlayingCardDeck_BasicInitializationTest()
        {
            // Arrange.
            const bool expectedFaceUp = true;
            const bool expectedFaceDown = false;
            StandardPlayingCardDeck defaultDeck;
            StandardPlayingCardDeck faceUpDeck;
            StandardPlayingCardDeck faceDownDeck;

            // Act.
            defaultDeck = new StandardPlayingCardDeck();
            faceUpDeck = new StandardPlayingCardDeck(expectedFaceUp);
            faceDownDeck = new StandardPlayingCardDeck(expectedFaceDown);

            // Assert.
            Assert.AreEqual(expectedFaceDown, defaultDeck.IsFaceUp);
            Assert.AreEqual(expectedFaceUp, faceUpDeck.IsFaceUp);
            Assert.AreEqual(expectedFaceDown, faceDownDeck.IsFaceUp);
            foreach (var deck in new StandardPlayingCardDeck[] { defaultDeck, faceUpDeck, faceDownDeck })
            {
                Assert.IsFalse(deck.IsEmpty);
                Assert.AreEqual(52, deck.Cards.Count);
            }
        }
    }
}

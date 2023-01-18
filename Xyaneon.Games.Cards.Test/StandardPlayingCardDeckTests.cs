using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Games.Cards.StandardPlayingCards;

namespace Xyaneon.Games.Cards.Test
{
    [TestClass]
    public class StandardPlayingCardDeckTests
    {
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

        [TestMethod]
        [Timeout(1000)]
        public void StandardPlayingCardDeck_JokersInitializationTest()
        {
            // Arrange.
            const int testUpTo = 3;
            var decks = new List<StandardPlayingCardDeck>(testUpTo);

            // Act.
            for (int i = 0; i < testUpTo; i++)
            {
                decks.Add(new StandardPlayingCardDeck(numberOfJokers: i));
            }

            // Assert.
            for (int i = 0; i < testUpTo; i++)
            {
                Assert.AreEqual(52 + i, decks[i].Cards.Count);
                Assert.AreEqual(i, decks[i].Cards.OfType<Joker>().Count());
            }
        }
    }
}

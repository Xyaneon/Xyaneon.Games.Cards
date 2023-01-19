using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Xyaneon.Games.Cards.StandardPlayingCards;
using Xyaneon.Games.Cards.Test.Extensions;

namespace Xyaneon.Games.Cards.Test
{
    [TestClass]
    public class StandardPlayingCardDeckTests
    {
        [TestMethod]
        public void StandardPlayingCardDeck_Constructor_ShouldInitializeDefaultNumberOfJokers()
        {
            const bool expectedFaceUp = true;
            const bool expectedFaceDown = false;
            StandardPlayingCardDeck defaultDeck;
            StandardPlayingCardDeck faceUpDeck;
            StandardPlayingCardDeck faceDownDeck;

            defaultDeck = new StandardPlayingCardDeck();
            faceUpDeck = new StandardPlayingCardDeck(expectedFaceUp);
            faceDownDeck = new StandardPlayingCardDeck(expectedFaceDown);

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
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [Timeout(1000)]
        public void StandardPlayingCardDeck_Constructor_ShouldInitializeWithExpectedNumberOfCardsForJokers(int numberOfJokers)
        {
            var deck = new StandardPlayingCardDeck(numberOfJokers: numberOfJokers);
            var expectedTotalCards = 52 + numberOfJokers;

            Assert.AreEqual(expectedTotalCards, deck.Cards.Count);
            Assert.AreEqual(numberOfJokers, deck.Cards.OfType<Joker>().Count());
        }

        [TestMethod]
        [Timeout(1000)]
        public void StandardPlayingCardDeck_Constructor_ShouldThrowForNegativeNumberOfJokers()
        {
            var actualException = Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                _ = new StandardPlayingCardDeck(numberOfJokers: -1);
            });

            Assert.That.ExceptionMessageStartsWith(
                actualException,
                "The number of jokers to include in the deck cannot be less than zero."
            );
        }
    }
}

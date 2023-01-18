﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Games.Cards.StandardPlayingCards;
using Xyaneon.Games.Cards.Test.Extensions;

namespace Xyaneon.Games.Cards.Test
{
    [TestClass]
    public class StandardPlayingCardDeckTests
    {
        [TestMethod]
        public void StandardPlayingCardDeck_BasicInitializationTest()
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
        [Timeout(1000)]
        public void StandardPlayingCardDeck_JokersInitializationTest()
        {
            const int testUpTo = 3;
            var decks = new List<StandardPlayingCardDeck>(testUpTo);

            for (int i = 0; i < testUpTo; i++)
            {
                decks.Add(new StandardPlayingCardDeck(numberOfJokers: i));
            }

            for (int i = 0; i < testUpTo; i++)
            {
                Assert.AreEqual(52 + i, decks[i].Cards.Count);
                Assert.AreEqual(i, decks[i].Cards.OfType<Joker>().Count());
            }
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

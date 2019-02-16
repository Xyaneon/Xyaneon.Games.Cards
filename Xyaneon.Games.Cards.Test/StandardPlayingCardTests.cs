using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Xyaneon.Games.Cards.StandardPlayingCards;

namespace Xyaneon.Games.Cards.Test
{
    /// <summary>
    /// Provides unit testing methods for the
    /// <see cref="StandardPlayingCard"/> class.
    /// </summary>
    [TestClass]
    public class StandardPlayingCardTests
    {
        /// <summary>
        /// Tests basic initialization of the <see cref="StandardPlayingCard"/>
        /// class.
        /// </summary>
        [TestMethod]
        public void StandardPlayingCard_BasicInitializationTest()
        {
            // Arrange.
            StandardPlayingCard card;

            // Act and assert.
            foreach (Rank rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
                {
                    card = new StandardPlayingCard(rank, suit);
                    Assert.AreEqual(rank, card.Rank);
                    Assert.AreEqual(suit, card.Suit);
                }
            }
        }

        /// <summary>
        /// Tests the <see cref="IEquatable{T}"/> implementation of the
        /// <see cref="StandardPlayingCard"/> class.
        /// </summary>
        [TestMethod]
        public void StandardPlayingCard_EqualityTest()
        {
            // Arrange.
            StandardPlayingCard card1;
            StandardPlayingCard card2;

            // Act and assert.
            foreach (Rank rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
                {
                    card1 = new StandardPlayingCard(rank, suit);
                    card2 = new StandardPlayingCard(rank, suit);
                    Assert.IsFalse(ReferenceEquals(card1, card2));
                    Assert.IsTrue(card1 == card2);
                    Assert.IsFalse(card1 != card2);
                }
            }
        }
    }
}

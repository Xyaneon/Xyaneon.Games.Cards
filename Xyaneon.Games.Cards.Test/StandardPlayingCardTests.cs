﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Games.Cards.StandardPlayingCards;

namespace Xyaneon.Games.Cards.Test
{
    [TestClass]
    public class StandardPlayingCardTests
    {
        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_BasicInitializationTest(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.AreEqual(rank, card.Rank);
            Assert.AreEqual(suit, card.Suit);
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_Equals_StandardPlayingCard_ShouldReturnTrueWhenBothCardsHaveSameRankAndSuit(Rank rank, Suit suit)
        {
            var card1 = new StandardPlayingCard(rank, suit);
            var card2 = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(ReferenceEquals(card1, card2));
            Assert.IsTrue(card1.Equals(card2));
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_Equals_StandardPlayingCard_ShouldReturnFalseWhenOtherCardIsNull(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(card.Equals(null));
        }

        [TestMethod]
        public void StandardPlayingCard_Equals_StandardPlayingCard_ShouldReturnFalseWhenRanksDoNotMatch()
        {
            var suit = Suit.Clubs;
            var card1 = new StandardPlayingCard(Rank.Ace, suit);
            var card2 = new StandardPlayingCard(Rank.Two, suit);

            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void StandardPlayingCard_Equals_StandardPlayingCard_ShouldReturnFalseWhenSuitsDoNotMatch()
        {
            var rank = Rank.Ace;
            var card1 = new StandardPlayingCard(rank, Suit.Clubs);
            var card2 = new StandardPlayingCard(rank, Suit.Diamonds);

            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_Equals_Object_ShouldReturnTrueWhenBothCardsHaveSameRankAndSuit(Rank rank, Suit suit)
        {
            var card1 = new StandardPlayingCard(rank, suit);
            var card2 = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(ReferenceEquals(card1, card2));
            Assert.IsTrue(card1.Equals((object)card2));
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_Equals_Object_ShouldReturnFalseWhenOtherCardIsNull(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(card.Equals((object)null));
        }

        [TestMethod]
        public void StandardPlayingCard_Equals_Object_ShouldReturnFalseWhenRanksDoNotMatch()
        {
            var suit = Suit.Clubs;
            var card1 = new StandardPlayingCard(Rank.Ace, suit);
            var card2 = new StandardPlayingCard(Rank.Two, suit);

            Assert.IsFalse(card1.Equals((object)card2));
        }

        [TestMethod]
        public void StandardPlayingCard_Equals_Object_ShouldReturnFalseWhenSuitsDoNotMatch()
        {
            var rank = Rank.Ace;
            var card1 = new StandardPlayingCard(rank, Suit.Clubs);
            var card2 = new StandardPlayingCard(rank, Suit.Diamonds);

            Assert.IsFalse(card1.Equals((object)card2));
        }

        [TestMethod]
        public void StandardPlayingCard_Equals_Object_ShouldReturnFalseWhenOtherIsNotStandardPlayingCard()
        {
            var card = new StandardPlayingCard(Rank.Ace, Suit.Clubs);
            object obj = new List<int>();

            Assert.IsFalse(card.Equals(obj));
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_GetHashCode_ShouldReturnHashCode(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);
            int expectedHashCode = rank.GetHashCode() ^ suit.GetHashCode();

            Assert.AreEqual(expectedHashCode, card.GetHashCode());
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_EqualsOperator_ShouldReturnTrueWhenRankAndSuitMatch(Rank rank, Suit suit)
        {
            var card1 = new StandardPlayingCard(rank, suit);
            var card2 = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(ReferenceEquals(card1, card2));
            Assert.IsTrue(card1 == card2);
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_EqualsOperator_ShouldReturnTrueForSameCardInstance(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            #pragma warning disable 1718
            Assert.IsTrue(card == card);
            #pragma warning restore 1718
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_EqualsOperator_ShouldReturnFalseWhenLeftCardIsNull(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(null == card);
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_EqualsOperator_ShouldReturnFalseWhenRightCardIsNull(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(card == null);
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_NotEqualsOperator_ShouldReturnFalseWhenRankAndSuitMatch(Rank rank, Suit suit)
        {
            var card1 = new StandardPlayingCard(rank, suit);
            var card2 = new StandardPlayingCard(rank, suit);

            Assert.IsFalse(ReferenceEquals(card1, card2));
            Assert.IsFalse(card1 != card2);
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_NotEqualsOperator_ShouldReturnFalseForSameCardInstance(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            #pragma warning disable 1718
            Assert.IsFalse(card != card);
            #pragma warning restore 1718
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_NotEqualsOperator_ShouldReturnTrueWhenLeftCardIsNull(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.IsTrue(null != card);
        }

        [TestMethod]
        [DynamicData(nameof(AllRanksAndSuitsData), DynamicDataSourceType.Method)]
        public void StandardPlayingCard_NotEqualsOperator_ShouldReturnTrueWhenRightCardIsNull(Rank rank, Suit suit)
        {
            var card = new StandardPlayingCard(rank, suit);

            Assert.IsTrue(card != null);
        }

        public static IEnumerable<object[]> AllRanksAndSuitsData() {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
                {
                    yield return new object[] {rank, suit};
                }
            }
        }
    }
}

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xyaneon.Games.Cards.Test.Extensions;
using Xyaneon.Games.Cards.Test.Helpers;

namespace Xyaneon.Games.Cards.Test
{
    [TestClass]
    public class DefaultShuffleAlgorithmTests
    {
        [TestMethod]
        public void DefaultShuffleAlgorithm_ShuffleShouldReturnSameCardsInNewList()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            #pragma warning disable 618
            var shuffleAlgorithm = new DefaultShuffleAlgorithm<IntCard>();
            #pragma warning restore 618

            IList<IntCard> actualShuffledCards = shuffleAlgorithm.Shuffle(cards);

            Assert.AreNotSame(cards, actualShuffledCards);
            Assert.That.CardSetsAreEqual(
                new HashSet<IntCard>(cards),
                new HashSet<IntCard>(actualShuffledCards)
            );
        }
    }
}

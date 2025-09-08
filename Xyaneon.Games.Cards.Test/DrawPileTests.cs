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
            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
                _ = new DrawPile<Card>(null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The collection of cards to create the draw pile from cannot be null.");
        }

        [TestMethod]
        public void DrawPile_Draw_ShouldRemoveAndReturnTopCard()
        {
            var topCard = new IntCard(1);
            var middleCard = new IntCard(2);
            var bottomCard = new IntCard(3);
            var drawPile = new DrawPile<IntCard>(new IntCard[] { bottomCard, middleCard, topCard });
            var expectedRemainingCardsList = new IntCard[] { middleCard, bottomCard };

            var actualDrawnCard = drawPile.Draw();

            Assert.AreSame(topCard, actualDrawnCard);
            Assert.That.CardListsAreEqual(expectedRemainingCardsList, drawPile.Cards);
        }

        [TestMethod]
        public void DrawPile_Draw_ShouldThrowExceptionWhenEmpty()
        {
            var drawPile = new DrawPile<IntCard>();

            var actualException = Assert.ThrowsExactly<InvalidOperationException>(() => {
                drawPile.Draw();
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "There are no cards left to draw.");
        }

        [TestMethod]
        public void DrawPile_DrawCount_ShouldRemoveAndReturnTopCards()
        {
            var topCard = new IntCard(1);
            var middleCard = new IntCard(2);
            var bottomCard = new IntCard(3);
            var drawPile = new DrawPile<IntCard>(new IntCard[] { bottomCard, middleCard, topCard });
            var expectedDrawnCardsList = new IntCard[] { topCard, middleCard };
            var expectedRemainingCardsList = new IntCard[] { bottomCard };

            IEnumerable<IntCard> actualDrawnCards = drawPile.Draw(2);

            CollectionAssert.AreEqual(expectedDrawnCardsList, actualDrawnCards.ToList());
            Assert.That.CardListsAreEqual(expectedRemainingCardsList, drawPile.Cards);
        }

        [TestMethod]
        public void DrawPile_DrawCount_ShouldThrowExceptionWhenEmpty()
        {
            var drawPile = new DrawPile<IntCard>();

            var actualException = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                drawPile.Draw(1);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "Too few cards to draw in this draw pile.");
        }

        [TestMethod]
        public void DrawPile_DrawCount_ShouldThrowExceptionWhenTooManyCardsDrawn()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                drawPile.Draw(4);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "Too few cards to draw in this draw pile.");
        }

        [TestMethod]
        public void DrawPile_DrawAtMost_ShouldRemoveAndReturnTopCards()
        {
            var topCard = new IntCard(1);
            var middleCard = new IntCard(2);
            var bottomCard = new IntCard(3);
            var drawPile = new DrawPile<IntCard>(new IntCard[] { bottomCard, middleCard, topCard });
            var expectedDrawnCardsList = new IntCard[] { topCard, middleCard };
            var expectedRemainingCardsList = new IntCard[] { bottomCard };

            IEnumerable<IntCard> actualDrawnCards = drawPile.DrawAtMost(2);

            CollectionAssert.AreEqual(expectedDrawnCardsList, actualDrawnCards.ToList());
            Assert.That.CardListsAreEqual(expectedRemainingCardsList, drawPile.Cards);
        }

        [TestMethod]
        public void DrawPile_DrawAtMost_ShouldRemoveAndReturnAllCardsIfMoreRequested()
        {
            var topCard = new IntCard(1);
            var middleCard = new IntCard(2);
            var bottomCard = new IntCard(3);
            var drawPile = new DrawPile<IntCard>(new IntCard[] { bottomCard, middleCard, topCard });
            var expectedDrawnCardsList = new IntCard[] { topCard, middleCard, bottomCard };
            var expectedRemainingCardsList = new IntCard[] { bottomCard };

            IEnumerable<IntCard> actualDrawnCards = drawPile.DrawAtMost(4);

            CollectionAssert.AreEqual(expectedDrawnCardsList, actualDrawnCards.ToList());
            Assert.That.DrawPileIsEmpty(drawPile);
        }

        [TestMethod]
        public void DrawPile_DrawAtMost_ShouldReturnEmptyCollectionForEmptyDrawPile()
        {
            var drawPile = new DrawPile<IntCard>();

            IEnumerable<IntCard> actualDrawnCards = drawPile.DrawAtMost(1);

            Assert.That.DrawPileIsEmpty(drawPile);
            Assert.IsNotNull(actualDrawnCards);
            Assert.AreEqual(0, actualDrawnCards.Count());
        }

        [TestMethod]
        public void DrawPile_DrawAtMost_ShouldReturnEmptyCollectionForZeroRequestedCards()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            IEnumerable<IntCard> actualDrawnCards = drawPile.DrawAtMost(0);

            Assert.That.CardListsAreEqual(cards.Reverse().ToList(), drawPile.Cards);
            Assert.IsNotNull(actualDrawnCards);
            Assert.AreEqual(0, actualDrawnCards.Count());
        }

        [TestMethod]
        public void DrawPile_DrawAtMost_ShouldThrowExceptionForNegativeRequestedCards()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                drawPile.DrawAtMost(-1);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The number of cards to draw must be non-negative.");
        }

        [TestMethod]
        public void DrawPile_Insert_ShouldInsertCardAtExpectedIndex()
        {
            var topCard = new IntCard(1);
            var middleCard = new IntCard(2);
            var bottomCard = new IntCard(3);
            var insertedCard = new IntCard(4);
            var drawPile = new DrawPile<IntCard>(new IntCard[] { bottomCard, middleCard, topCard });
            var expectedRemainingCardsList = new IntCard[] { topCard, insertedCard, middleCard, bottomCard };

            drawPile.Insert(1, insertedCard);

            Assert.That.CardListsAreEqual(expectedRemainingCardsList.Reverse().ToList(), drawPile.Cards);
        }

        [TestMethod]
        public void DrawPile_Insert_ShouldThrowExceptionForNullCard()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
                drawPile.Insert(1, null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The card to insert into the draw pile cannot be null.");
        }

        [TestMethod]
        public void DrawPile_Insert_ShouldThrowExceptionForNegativeIndex()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);
            var insertedCard = new IntCard(4);

            var actualException = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                drawPile.Insert(-1, insertedCard);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The position to insert the card into the draw pile at cannot be less than zero.");
        }

        [TestMethod]
        public void DrawPile_Insert_ShouldThrowExceptionForIndexAboveUpperBound()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);
            var insertedCard = new IntCard(4);

            var actualException = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => {
                drawPile.Insert(4, insertedCard);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The position to insert the card into the draw pile at cannot be greater than the number of cards in the draw pile.");
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

        [TestMethod]
        public void DrawPile_PlaceAtBottom_ShouldThrowExceptionForNullCard()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
                drawPile.PlaceAtBottom(null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The card to place at the bottom of the draw pile cannot be null.");
        }

        [TestMethod]
        public void DrawPile_PlaceOnTop_ShouldPlaceCardOnTop()
        {
            var topCard = new IntCard(1);
            var middleCard = new IntCard(2);
            var bottomCard = new IntCard(3);
            var insertedCard = new IntCard(4);
            var drawPile = new DrawPile<IntCard>(new IntCard[] { bottomCard, middleCard, topCard });
            var expectedCardsList = new IntCard[] { insertedCard, topCard, middleCard, bottomCard };

            drawPile.PlaceOnTop(insertedCard);

            Assert.That.CardListsAreEqual(expectedCardsList, drawPile.Cards);
        }

        [TestMethod]
        public void DrawPile_PlaceOnTop_ShouldThrowExceptionForNullCard()
        {
            var cards = new IntCard[] { new IntCard(1), new IntCard(2), new IntCard(3) };
            var drawPile = new DrawPile<IntCard>(cards);

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
                drawPile.PlaceOnTop(null);
            });

            Assert.That.ExceptionMessageStartsWith(actualException, "The card to place on top of the draw pile cannot be null.");
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

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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

            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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

            ShuffleFunction<IntCard> shuffleAlgorithm = cards => cards.Reverse().ToList();

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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

            var actualException = Assert.ThrowsExactly<ArgumentNullException>(() => {
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
    }
}

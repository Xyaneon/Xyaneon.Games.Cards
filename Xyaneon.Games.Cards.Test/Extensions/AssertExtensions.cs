using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xyaneon.Games.Cards.Test.Extensions
{
    public static class AssertExtensions
    {
        public static void CardListsAreEqual<TCard>(this Assert assert, IEnumerable<TCard> expectedCardList, IEnumerable<TCard> actualCardList) where TCard : Card
        {
            if (expectedCardList.SequenceEqual(actualCardList))
            {
                return;
            }

            throw new AssertFailedException(CreateUnequalCardListsMessage(expectedCardList, actualCardList));
        }

        public static void CardSetsAreEqual<TCard>(this Assert assert, ISet<TCard> expectedCardList, ISet<TCard> actualCardList) where TCard : Card
        {
            if (expectedCardList.SetEquals(actualCardList))
            {
                return;
            }

            throw new AssertFailedException(CreateUnequalCardSetsMessage(expectedCardList, actualCardList));
        }

        public static void DrawPileIsEmpty<TCard>(this Assert assert, IDrawPile<TCard> drawPile) where TCard : Card
        {
            if (drawPile.IsEmpty)
            {
                return;
            }

            throw new AssertFailedException($"The draw pile was expected to be empty, but contained {drawPile.Cards.Count} card(s).");
        }

        public static void StringContains(this Assert assert, string actualString, string expectedSubstring)
        {
            if (actualString.Contains(expectedSubstring))
            {
                return;
            }

            throw new AssertFailedException(CreateSubstringNotFoundMessage(expectedSubstring, actualString));
        }

        private static string CreateSubstringNotFoundMessage(string expectedSubstring, string actualString)
        {
            return (new StringBuilder())
                .AppendLine("Substring not found.")
                .Append("Expected: ")
                .AppendLine($"\"{expectedSubstring}\"")
                .Append("Actual  : ")
                .AppendLine($"\"{actualString}\"")
                .ToString();
        }

        private static string CreateUnequalCardListsMessage<TCard>(IEnumerable<TCard> expectedCardList, IEnumerable<TCard> actualCardList) where TCard : Card
        {
            return (new StringBuilder())
                .AppendLine("Card lists differ.")
                .Append("Expected: ")
                .AppendLine(FormatCardList(expectedCardList))
                .Append("Actual  : ")
                .AppendLine(FormatCardList(actualCardList))
                .ToString();
        }

        private static string CreateUnequalCardSetsMessage<TCard>(IEnumerable<TCard> expectedCardList, IEnumerable<TCard> actualCardList) where TCard : Card
        {
            return (new StringBuilder())
                .AppendLine("Card sets differ.")
                .Append("Expected: ")
                .AppendLine(FormatCardList(expectedCardList))
                .Append("Actual  : ")
                .AppendLine(FormatCardList(actualCardList))
                .ToString();
        }

        private static string FormatCardList<TCard>(IEnumerable<TCard> cards) where TCard : Card
        {
            return "[" + string.Join(", ", cards) + "]";
        }
    }
}
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

        public static void ExceptionMessageStartsWith(this Assert assert, Exception actualException, string expected)
        {
            if (actualException.Message.StartsWith(expected))
            {
                return;
            }

            throw new AssertFailedException(CreateExceptionMessageDidNotStartWithMessage(actualException, expected));
        }

        private static string CreateExceptionMessageDidNotStartWithMessage(Exception actualException, string expectedSubstring)
        {
            return (new StringBuilder())
                .AppendLine("Exception message did not start with expected substring.")
                .Append("Expected: ")
                .AppendLine($"\"{expectedSubstring}\"")
                .Append("Actual  : ")
                .AppendLine($"\"{actualException.Message}\"")
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
using System.Collections.Generic;

namespace Xyaneon.Games.Cards
{
    /// <summary>
    /// Encapsulates a method which takes a collection of cards, then returns
    /// an ordered list of the same cards after shuffling them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The shuffling function should always return the same card instances it
    /// was originally given in the <paramref name="cards"/> argument, but
    /// most likely in a different order. Elements are expected not to be
    /// added, removed or modified by the provided function, although strictly
    /// speaking <see cref="DrawPile{TCard}"/> does not enforce this.
    /// </para>
    /// <para>
    /// You do not need to define nor supply your own
    /// <see cref="ShuffleFunction{TCard}"/> to shuffle instances of
    /// <see cref="DrawPile{TCard}"/>. Omitting it will simply make
    /// <see cref="DrawPile{TCard}"/> use an internal default. However, you
    /// can create one if you desire specific shuffling behavior, or for
    /// testing purposes.
    /// </para>
    /// </remarks>
    /// <seealso cref="DrawPile{TCard}"/>
    public delegate IList<TCard> ShuffleFunction<TCard>(IEnumerable<TCard> cards) where TCard : Card;
}

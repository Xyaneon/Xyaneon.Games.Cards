# Xyaneon.Games.Cards

[![License](https://img.shields.io/github/license/Xyaneon/Xyaneon.Games.Cards)][License]
[![NuGet](https://img.shields.io/nuget/v/Xyaneon.Games.Cards.svg?style=flat)][NuGet package]
![Nuget downloads](https://img.shields.io/nuget/dt/Xyaneon.Games.Cards)
[![.NET](https://github.com/Xyaneon/Xyaneon.Games.Cards/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Xyaneon/Xyaneon.Games.Cards/actions/workflows/dotnet.yml)

![Package Icon][icon]

A .NET 6.0 library for playing cards (standard and custom), draw piles
and shuffling.

## Usage

To use this library, you must first install the [NuGet package][NuGet package]
for it, then add the following `using` statement at the top of each C# source
file where you plan on using it:

```csharp
using Xyaneon.Games.Cards;
```

The two most important classes in this library are [`Card`][Card class] and
[`DrawPile<TCard>`][DrawPile class]. If you are implementing your own custom
card game application using this library, then your custom card class should
inherit from the `Card` class, and you should supply that subclass type as the
generic type parameter for your `DrawPile<TCard>` instances.

For example, you would declare your custom `MyCard` class as follows:

```csharp
using Xyaneon.Games.Cards;

public class MyCard : Card
{
    // Your code here.
}
```

...and then set up a draw pile like so:

```csharp
var myDrawPile = new DrawPile<MyCard>();
```

Note that draw piles are considered to be face-down by default. If you have a
face-up pile of cards (for e.g., a discard pile), then you have to specify it
in the constructor using the `isFaceUp` optional parameter:

```csharp
var myDrawPile = new DrawPile<MyCard>(true);
```

After the draw pile is created, you can do several things with it, including:

```csharp
// Count the number of cards in the draw pile.
int cardCount = myDrawPile.Cards.Count;

// Draw a card. An InvalidOperationException will be thrown if the pile is
// empty.
MyCard drawnCard = myDrawPile.Draw();

// Draw three cards (or at least as many as we can if there are fewer).
IEnumerable<MyCard> drawnCards = myDrawPile.DrawAtMost(3);

// Place a card on top of the draw pile.
var cardToPlaceOnTop = new MyCard();
drawPile.PlaceOnTop(cardToPlaceOnTop);

// Shuffle the draw pile using the default shuffling algorithm.
myDrawPile.Shuffle();

// Shuffle the draw pile using a custom shuffling algorithm you implemented.
IShuffleAlgorithm<MyCard> algorithm = new MyShuffleAlgorithm();
myDrawPile.Shuffle(algorithm);

// Shuffle one draw pile into another.
DrawPile<MyCard> other = myObject.YourMethodToGetAnotherDrawPile();
myDrawPile.ShuffleIn(other); // other is now empty, with its contents added to myDrawPile.
```

For a full list of the available properties and methods, see the source code
for the [`DrawPile<TCard>`][DrawPile class] class.

For an example usage of these classes, see the standard 52-card implementation
provided with this library under the
[Xyaneon.Games.Cards.StandardPlayingCards][StandardPlayingCards] namespace.
This is also provided for your convenience if you want to implement a card game
which uses the standard 52-card deck.

## License

This library is free and open-source software provided under the MIT license.
Please see the [LICENSE.txt][License] file for details.

[Card class]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/main/Xyaneon.Games.Cards/Card.cs
[DrawPile class]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/main/Xyaneon.Games.Cards/DrawPile.cs
[icon]: https://raw.githubusercontent.com/Xyaneon/Xyaneon.Games.Cards/main/Xyaneon.Games.Cards/images/icon.png
[License]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/main/LICENSE.txt
[NuGet package]: https://www.nuget.org/packages/Xyaneon.Games.Cards/
[StandardPlayingCards]: https://github.com/Xyaneon/Xyaneon.Games.Cards/tree/main/Xyaneon.Games.Cards/StandardPlayingCards
[Travis CI]: https://travis-ci.com/Xyaneon/Xyaneon.Games.Cards

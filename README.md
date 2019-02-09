# Xyaneon.Games.Cards

[![NuGet](https://img.shields.io/nuget/v/Xyaneon.Games.Cards.svg?style=flat)][NuGet package]
[![Build Status](https://travis-ci.com/Xyaneon/Xyaneon.Games.Cards.svg?branch=master)][Travis CI]

![Package Icon][icon]

A .NET Standard library for playing cards (standard and custom), draw piles
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

## License

This library is free and open-source software provided under the MIT license.
Please see the [LICENSE.txt][license] file for details.

[Card class]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/master/Xyaneon.Games.Cards/Card.cs
[DrawPile class]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/master/Xyaneon.Games.Cards/DrawPile.cs
[icon]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/master/Xyaneon.Games.Cards/icon.png
[license]: https://github.com/Xyaneon/Xyaneon.Games.Cards/blob/master/LICENSE.txt
[NuGet package]: https://www.nuget.org/packages/Xyaneon.Games.Cards/
[Travis CI]: https://travis-ci.com/Xyaneon/Xyaneon.Games.Cards
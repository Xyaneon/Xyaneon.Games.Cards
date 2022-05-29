# Changelog

# [1.0.0](https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.4.0...v1.0.0) (2022-05-29)


### Bug Fixes

* Remove deprecated PackageIconUrl property ([61e7408](https://github.com/Xyaneon/Xyaneon.Games.Cards/commit/61e74087934b748f145a41b4147978a415be74db))
* Replace absolute path for XML docs ([baaeb51](https://github.com/Xyaneon/Xyaneon.Games.Cards/commit/baaeb517217a39cd38324724680a49981fcc7e6f))


### Features

* Add GitHub Actions to Dependabot updates ([c176dac](https://github.com/Xyaneon/Xyaneon.Games.Cards/commit/c176dac7a1c7d4c429d7104d77bdd631539b7057))


### Reverts

* Revert "Specify Xenial instead of Bionic in Travis." (#18) ([ba6c47b](https://github.com/Xyaneon/Xyaneon.Games.Cards/commit/ba6c47ba011c9178400ea4156be27723743e817a)), closes [#18](https://github.com/Xyaneon/Xyaneon.Games.Cards/issues/18)

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- Added GitHub Actions pull request checks to replace Travis CI.
- Added automatic dependency updating via Dependabot.

### Changed
- Switched target from .NET Standard to .NET 6.0.
- .csproj package file cleanup.
- Updated test dependencies.

## [0.4.0] - 2019-12-04
### Added
- The `StandardPlayingCard` class now implements the
  `IEquatable<StandardPlayingCard>` interface.

### Fixed
- Fixed the `DrawPile<TCard>.PlaceAtBottom` method placing cards on top (thanks to
  [Clayton Wahlstrom](https://github.com/claywahlstrom) for finding this!)
  ([issue #14](https://github.com/Xyaneon/Xyaneon.Games.Cards/issues/14)).

## [0.3.0] - 2019-02-10
### Added
- Added the `IDrawPile<TCard>` interface.

### Changed
- Changed deployment tag regex.
- The `DrawPile<TCard>` class now implements the new `IDrawPile<TCard>`
  interface.

## [0.2.0] - 2019-02-09
### Added
- Added `DrawPile<TCard>.ShuffleIn` method overloads which accept another
  `DrawPile<TCard>` as a parameter.
- Added continuous deployment (CD) using Travis CI.
- Added basic usage instructions in the README.md file.
- Added a couple more unit test methods.

### Fixed
- Fixed the icon URL for the NuGet package.

## [0.1.1] - 2019-02-03
### Added
- Added an icon for the package.

## [0.1.0] - 2019-02-01
### Added
- Initial release.

[Unreleased]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.4.0...HEAD
[0.4.0]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.3.0...v0.4.0
[0.3.0]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.1.1...v0.2.0
[0.1.1]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.1.0...v0.1.1
[0.1.0]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/c6d59cf66aa7b320596e754b673f370e88472474...v0.1.0

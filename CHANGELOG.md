# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
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

[Unreleased]: https://github.com/Xyaneon/Xyaneon.ComputerScience.GraphTheory/compare/v0.2.0...HEAD
[0.2.0]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.1.1...v0.2.0
[0.1.1]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/v0.1.0...v0.1.1
[0.1.0]: https://github.com/Xyaneon/Xyaneon.Games.Cards/compare/c6d59cf66aa7b320596e754b673f370e88472474...v0.1.0
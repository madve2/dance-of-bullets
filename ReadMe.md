# Dance of Bullets

## A little backstory

Dance of Bullets is a bullet hell shmup I worked on mostly in 2012 using the XNA framework, mostly driven by the fact that there weren't many games in this genre on the PC outside japan at the time. (And / or they were hard to access.) This isn't an issue anymore as Steam became more open to submissons, so I slowly stopped developing DoB, especially as support for XNA has been ended by Microsoft.

## What's the point of adding it to GitHub, then?

Looking at the current selection of bullet hell games, while there's definitely a plenty of them, there's one aspect of DoB that feels unique to me: it's flexible, highly extensible and easy-to create format for levels, enemies and bosses, that could make it possible for everyone to create their own bullet hell shmup without programming knowledge of any kind.

Ideally, the goal is to release it as a free game on Steam, with workshop support for levels. Realistically, I want to clean up the code and assets a bit, make adding custom content even more flexible, and releasing it on Itch.

## The current state

The bad news

:x: There's no menu or settings. The game just starts when you run it, and you can't restart when you die (or reach the end of the last stage). There's a config file, but it only supports setting a resolution. Fixed keybindings with no controller support.
:x: Only has placeholder assets (especially on the second and third level of the alpha), barebones HUD
:x: Thins can move on screen, obviously, in very sophisticated ways. But adding animated texture still would be a good thing.
:x: No scoring, and by extension, no high scores
:x: No sounds or music either.

The good news

:heavy_check_mark: Supports levels, bosses, even mid-level bosses, and "scriptable" enemy movement
:heavy_check_mark: Complex and highly customizable (comoseable, even) bullet patterns
:heavy_check_mark: Special abilities: slow-down time and "rain of bullets"
:heavy_check_mark: A sophisticated, XAML-based level format with a huge focus on flexibility, reusability and extensibility. After reading the documentation (coming soon...) it should be easy to create new enemies, bosses and complete levels (even campaigns) without programming knowledge / modifying the code

Programmer things

:lipstick: As the codebase evolved, I noticed that some of my internal concepts (like Component and Behavior) have many similarities, offering many refactoring opportunities!
:lipstick: Some of the built-in behaviors became quite redundant as the core system got more flexible. Probably I could delete some code.
:lipstick: Although MonoGame opens the possibility to make the game work on non-Windows systems, I use Windows specific APIs for reading XAML. Porting to macOS and Linux should be possible, but challenging
:lipstick: No documentation, although it would be very important, especially for prospect level creators. This is one of the first things on my TODO list.

## Playing the game

Download the current alpha and take it for a spin from Itch, available in a few days! You can find how-to-play instructions there.

## Building the game

All you need is Visual Studio 2017 Community and MonoGame 3.6

## Reporting issues / Contributing

Err, I honestly don't know how much regulation this needs at the current state of this project. Open an issue and we'll figure this out.
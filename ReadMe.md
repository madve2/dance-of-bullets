# Dance of Bullets

## A little backstory

Dance of Bullets is a bullet hell shmup I worked on mostly in 2012 using the XNA framework, mostly driven by the fact that there weren't many games in this genre on the PC outside japan at the time. (And / or they were hard to access.) This isn't an issue anymore as Steam became more open to submissions, so I slowly stopped developing DoB, especially as support for XNA has been ended by Microsoft.

## What's the point of adding it to GitHub, then?

Looking at the current selection of bullet hell games, while there's definitely a plenty of them, there's one aspect of DoB that feels unique to me: it's flexible, highly extensible and easy-to-create format for levels, enemies and bosses, that could make it possible for everyone to create their own bullet hell shmup without programming knowledge of any kind.

Ideally, the goal is to release it as a free game on Steam, with workshop support for levels. Realistically, I want to clean up the code and assets a bit, make adding custom content even more flexible, and releasing it on Itch.

## The current state

:x: | The bad news
---|---
:x: | There's no menu or settings. The game just starts when you run it, and you can't restart when you die (or reach the end of the last stage). There's a config file, but it only supports setting a resolution. Fixed keybindings with no controller support.
:x: | Only has placeholder assets (especially on the second and third level of the alpha), barebones HUD
:x: | While objects can move across the screen in fascinating ways, there's no support for animated textures yet
:x: | No scoring, and by extension, no high scores
:x: | No sounds or music either.

:heavy_check_mark: | The good news
---|---
:heavy_check_mark: | Supports levels, bosses, even mid-level bosses, and "scriptable" enemy movement
:heavy_check_mark: | Complex and highly customizable (composable, even) bullet patterns
:heavy_check_mark: | Special abilities: slow-down time and "rain of bullets"
:heavy_check_mark: | A sophisticated, XAML-based level format with a huge focus on flexibility, reusability and extensibility. After reading the documentation (coming soon...) it should be easy to create new enemies, bosses and complete levels (even campaigns) without programming knowledge / modifying the code

:lipstick: | Code quality
---|---
:lipstick: | As the codebase evolved, I noticed that some of my internal concepts (like Component and Behavior) have many similarities, offering many refactoring opportunities!
:lipstick: | Some of the built-in behaviors became quite redundant as the core system got more flexible. Probably I could delete some code.
:lipstick: | Although MonoGame opens the possibility to make the game work on non-Windows systems, I use Windows specific APIs for reading XAML. Porting to macOS and Linux should be possible, but challenging
:lipstick: | No documentation, although it would be very important, especially for prospect level creators. This is one of the first things on my TODO list.

## Playing the game

Download the current alpha and take it for a spin from [Itch](https://madve2.itch.io/dance-of-bullets)! You can find how-to-play instructions there.

## Making your own levels

While I've yet to make a proper documentation for the level format of Dance of Bullets, open the files in the StageData folder with your favorite text editor and start tweaking! You will be able to add your own levels, enemies, bullet patterns and bosses in no time! 

Some pointers until I write some proper documentation:

- `Stages.xaml` is the main file the game loads. It contains _Stages_ (essentially, the levels) which will be loaded in the order they are in the file. A stage consists of multiple _Segments_, each with its own boss(es). Segments contain _EnemySpawners_, which orchestrate the timing of the appearance of the various enemies on the level / segment.
- `Prototypes-1.xaml`, `Prototypes-2.xaml`​  and `PrototypesRC-1.xaml`​ contain _Enemy_ definitions, referenced by the _EnemySpawners_ in `Stages.xaml`. It governs how they move, what they look like, how though they are etc. Their shooting patterns are, however, a bit more complicated, so those usually appear in the last file, called...
- `Prototypes-Common.xaml`: it contains various _constants_ (e.g. colors), _behaviors_ (which in this case are mostly bullet pattern definitions referenced by the enemies) and _bullet_ definitions for the bullet patterns. (Essentially, a bullet definition specifies how a bullet moves once it's been shot, while a `*TurretBehavior` defines how and when exactly should the shooting happen.)

There's no significance to the naming of these files other than `Stages.xaml`. You can add new files and put new enemies, behaviors, bullets etc. in them as long as you reference them in the correct order in the _PrototypePacks_ attribute of the root element of `Stages.xaml`

## Building the game

All you need is Visual Studio 2017 Community and MonoGame 3.6

## Reporting issues / Contributing

Err, I honestly don't know how much regulation this needs at the current state of this project. Open an issue and we'll figure this out.
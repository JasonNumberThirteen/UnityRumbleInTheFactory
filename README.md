# Rumble In The Factory
> An arcade game inspired by "Battle City" (1985) made using "Unity" game engine with C# language.

## Table of Contents
* [General information](#general-information)
* [Used technologies](#used-technologies)
* [Features](#features)
* [Differences](#differences)
* [Project status](#project-status)
* [Credits](#credits)

## General information
- This is a "Unity" game project of my arcade game inspired by "**Battle City**", a popular top down shooter developed by [Namco](https://en.wikipedia.org/wiki/Namco) in 1985.
- The project presents my programming skills how I do write code taking care of several optimisations.
- The game consists of defending the nuke against incoming robots. The player controls the yellow robot (player 2 controls the green one) and must shoot projectiles to destroy enemy robots which spawn on the top of the map.
- Win condition: all incoming enemy robots are destroyed
- Lose condition: the player lost all lives or the nuke is destroyed

## Used technologies
- [Unity](https://unity.com/) game engine (version **2022.3.1f1**)
- [Visual Studio Code](https://code.visualstudio.com/) IDE for writing code
- [GIMP](https://www.gimp.org/) & [Paint.NET](https://www.getpaint.net/) for making graphics
- [Tiled](https://www.mapeditor.org/) for making stage layouts

## Features
- Several scenes:
	- **Main Menu** as a starting scene with option selection
	- **Stage Selection** wherein you can select which stage you want to start from (accessible from the main menu scene after selecting one of the game modes)
	- **Stage** is a scene wherein the game is played on current stage, firstly selected and later when the player is advanced to the next one (accessible from the stage selection scene after selecting the stage & from the score scene when all points for enemies were counted)
	- **Score** displays details about defeated enemies, what types and how many units has been destroyed (accessible from the stage scene when the game is over, no matter if won or failed)
	- **Game Over** shows a big "GAME OVER" text made from bricks tiles (accessible from the score scene when the game is over & the player lost)
	- **High Score** shows a big "HISCORE" text with a new high score, this text is also made from bricks tiles (accessible from the game over scene when the player has beaten high score)
- Loading stages data from JSON files (this eliminates the necessity of building levels using builtin level editor)
	- A file must contain an array of integers for tiles layout (26x26) and another array of integers for enemies layout (indexes for array of game objects)
- Tiles:
	- **Bricks** (destructible obstacle, can take damage from every side and this may be used for tactics)
	- **Metal** (indestructible obstacle, can be damaged only by the player on the last rank)
	- **Grid** (a tile reducing visibility of all entities)
	- **Slippery Floor** (a tile moving robots forward even when the player does not change their movement)
	- **Toxic Waste** (an impassable tile for robots, however **bullets can fly through them!**)
- Bonuses:
	- **Shield** (gives the player temporary invincibility against enemy projectiles)
	- **Freeze** (temporarily freezes all enemies, both already on the map and every spawned)
	- **Fortress** (surrounds the nuke with metal tiles which change itself into brick tiles after a short time)
	- **Rank** (promotes the player upgrading his capabilities and firing power)
	- **Destruction** (destroys all enemies on the map, **does not give points for them though**)
	- **Life** (gives the player additional life)
- Enemy AI detecting which direction choose when it detects an obstacle (**uses raycasting with a layer mask**)

## Differences
Although I did my best to keep the biggest part of the original game unchanged, there **ARE** some significant differences (some of them improve flexibility) and may not be noticeable at first sight.
- Overall
	- **The whole graphics were made by myself**
- Main Menu
	- The **Construction** option was replaced with the **Exit** (instead of the builtin level editor, there is a system which read JSON files containing stage data about tiles placement and enemies layout)
	- The moving parts of the background for the stage number counter **are being moved horizontally**, not vertically as in the original
- Stage Selection
	- **There is no constant amount of stages** (35) - here it depends on put JSON files into the array of the stages loader component
- Score
	- **The constant number of enemy types** (4) **was removed**, here is dependant of defeated types of enemies and can take from 0 to N

## Project status
<p align = "center"><b>IN PROGRESS</b></p>

---
**The project is completed in ~80%**. It still requires fixes, optimisations and upgrades. Detailed information about left tasks were written below (tasks are split by the scenes):

```[tasklist]
- [ ] MAIN MENU
	- [ ] Compatibility for every aspect ratio
- [ ] STAGE SELECTION
	- [ ] Return to the main menu
	- [ ] Inclusion of case when there are no stages to select
	- [X] Fast scroll of the stage numer when one of keys is being held
- [ ] STAGE
	- [ ] Music
	- [ ] Player 2
	- [ ] Bug fixes
	- [ ] Adjustments
	- [ ] Optimisations
	- [ ] Sound effects
	- [ ] Flexible input
	- [ ] Gameplay tests
	- [ ] New enemy types
	- [ ] Refactoring of code
	- [ ] Compatibility for every aspect ratio
	- [ ] Graphics of the player robot for every rank
- [ ] SCORE
	- [ ] Refactoring of code
	- [ ] Points count for the player 2
	- [X] Compatibility for every aspect ratio
- [ ] GAME OVER
	- [ ] Music
- [ ] HIGH SCORE
	- [ ] Music
```

## Credits
This project was made by [Jason](https://jasonxiii.pl "Jason. Cała informatyka w jednym miejscu! Oficjalna strona internetowa! Setki artykułów na różne tematy! Wszystko stworzone przez jedną osobę!").
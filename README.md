# Rumble In The Factory
> An arcade game inspired by "Battle City" (1985) made using "Unity" game engine with C# language.

## Table of Contents
* [General information](#general-information)
* [Used technologies](#used-technologies)
* [Features](#features)
* [Scenes](#scenes)
* [Usage](#usage)
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
- 6 scenes
- 2 game modes
- 3 stages
- 5 types of tiles
- 6 bonuses

## Scenes
### Main Menu
A starting scene with option selection.
### Stage Selection
A scene wherein you can select which stage you want to start from (accessible from the main menu scene after selecting one of the game modes).
### Stage
A scene wherein the game is played on current stage, firstly selected and later when the player is advanced to the next one (accessible from the stage selection scene after selecting the stage & from the score scene when all points for enemies were counted).
### Score
A scene which displays details about defeated enemies, what types and how many units have been destroyed (accessible from the stage scene when the game is over, no matter if won or failed).
### Game Over
A scene which shows a big "**GAME OVER**" text (accessible from the score scene when the game is over & the player lost).
### High Score
A scene which shows a big "**HISCORE**" text with a new high score (accessible from the game over scene when the player has beaten high score).

## Usage
- Press W / S / A / D keys or arrow keys to navigate through menus & control the player one robot
- Press LMB / Space to shoot
- Press RMB / Esc to pause

## Differences
Although I did my best to keep the biggest part of the original game unchanged, there **ARE** some significant differences (some of them improve flexibility) and may not be noticeable at first sight.
- Overall
	- **The whole graphics were made by myself**
- Main Menu
	- The **Construction** option was replaced with the **Exit** (instead of the builtin level editor, there is a system which read data files)
	- The moving parts of the background for the stage number counter **are being moved horizontally**, not vertically as in the original
- Stage Selection
	- **There is no constant amount of stages** (35) - here it depends on found data files
- Score
	- **The constant number of enemy types** (4) **was removed**, here is dependant of defeated types of enemies and can take from 0 to N

## Project status
<p align = "center"><b>IN PROGRESS</b></p>

---
**The project is completed in ~80%**. It still requires fixes, optimisations and upgrades. Detailed information about left tasks were written below (tasks are split by the scenes):

```[tasklist]
- [X] STAGE SELECTION
	- [X] Return to the main menu
	- [X] Automatic load of JSON files in directory
	- [X] Inclusion of case when there are no stages to select
	- [X] Fast scroll of the stage numer when one of keys is being held
- [ ] STAGE
	- [ ] Music
	- [ ] Player 2
		- [ ] Input
		- [ ] Spawn
		- [X] Points gain
		- [ ] Sprite sheet
		- [ ] Bullet prefab
	- [ ] Bug fixes
		- [X] Bonus placement in inaccessible areas
		- [ ] Getting stuck by enemies after changing direction (occurs randomly)
		- [ ] Clash of enemies when one of them is placed on a spawner and it spawned the other one
		- [ ] No bullet movement when the player change movement direction and steps on slippery floor tiles
		- [ ] Clash of timer events when the nuke is destroyed as soon as defeated the last enemy (the score scene is loaded before the "Game Over" text stops moving)
	- [ ] Adjustments
		- [X] Balance of the player robots statistics on every rank
		- [X] Growth of difficulty everytime when beaten the last stage
		- [X] Color lerp of the bonus enemy robot dependant of time scale
		- [ ] Exclusion of tiles from stage data in occupied places (the nuke with bricks tiles surrounding it & spawners)
	- [X] Optimisations
		- [X] BonusRenderer (replacement of the Update method with the InvokeRepeating to avoid calls per frame)
		- [X] PlayerRobotMovement (update of collision detector rotation only once when it is necessary)
	- [ ] Sound effects
	- [X] Flexible input
	- [ ] Gameplay tests
	- [ ] New enemy types
	- [ ] Refactoring of code
		- [X] BonusTrigger
		- [ ] StageManager (reduction of code complexity, decomposition of the class)
		- [ ] StageUIManager (reduction of code complexity, resetting both players' lists of defeated enemies when the stage starts, methods for updating counters separately)
		- [X] PlayerRobotRank
		- [ ] EnemyRobotFreeze (editing movement directly outside of the EnemyRobotMovement script)
		- [X] EnemyRobotHealth
		- [ ] EnemySpawnManager (reduction of code complexity)
		- [X] EnemyRobotMovement
	- [X] Compatibility for every aspect ratio
	- [ ] Graphics of the player robot for every rank (both players)
- [ ] SCORE
	- [ ] Refactoring of code
	- [ ] Points count for the player 2
	- [X] Compatibility for every aspect ratio
- [ ] GAME OVER
	- [ ] Music
- [ ] HIGH SCORE
	- [ ] Music
	- [X] Refactoring of code
```

## Credits
This project was made by [Jason](https://jasonxiii.pl "Jason. Cała informatyka w jednym miejscu! Oficjalna strona internetowa! Setki artykułów na różne tematy! Wszystko stworzone przez jedną osobę!").
# Rumble In The Factory
> An arcade game inspired by "Battle City" (1985) made using "Unity" game engine with C# language.

## Table of Contents
* [General information](#general-information)
* [Used technologies](#used-technologies)
* [Features](#features)
* [Usage](#usage)
* [Differences](#differences)
* [Project status](#project-status)
* [Screenshots](#screenshots)
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
	- **Score** displays details about defeated enemies, what types and how many units have been destroyed (accessible from the stage scene when the game is over, no matter if won or failed)
	- **Game Over** shows a big "GAME OVER" text made from bricks tiles (accessible from the score scene when the game is over & the player lost)
	- **High Score** shows a big "HISCORE" text with a new high score, this text is also made from bricks tiles (accessible from the game over scene when the player has beaten high score)
- Loading stages data from JSON files (this eliminates the necessity of building levels using builtin level editor)
	- A file must contain an array of integers for tiles layout (26x26) and another array of integers for enemies layout (indexes for array of scriptable objects containing enemy data)
	- Tiles layout is used by the stage builder component
	- Enemies layout is set by the enemy spawn manager component
- Stage manager:
	- The main manager controlling the game flow
	- Can take one of five states:
		- **Active** when the game is running (this is the initial state)
		- **Paused** when the game is paused (can only take this state when the game is active)
		- **Interrupted** when the player has died or the nuke has been destroyed (one of the "fail" conditions is met)
		- **Won** when all enemies are destroyed
		- **Over** when the "Game over" starts moving to the center (blocks the player input)
- Stage UI manager:
	- Controls a user interface of the stage scene
	- Sets position of left enemy icons indicating amount of left enemies available to spawn (limit is editable)
	- Updates counters values such as player lives & current stage number
	- Instantiates gained points counter setting its position & counter value
- Enemy spawn manager:
	- Assigns enemies layout taking array of integers from the current stage data
	- Spawns new enemies if amount of alive enemies is less than a given limit
		- It switches enemy spawner everytime when it "orders" enemy spawn, so at the start the first spawner is chosen, then the next one etc. When it reaches the last one, it returns to the start.
- Tiles:
	- **Bricks** (destructible obstacle, can take damage from every side and this may be used for tactics)
	- **Metal** (indestructible obstacle, can be damaged only by the player on the last rank)
	- **Grid** (a tile reducing visibility of all entities)
	- **Slippery Floor** (a tile moving robots forward even when the player does not change their movement)
	- **Toxic Waste** (an impassable tile for robots, however **bullets can fly through them!**)
- Player:
	- Input system (mouse + keyboard, keyboard only)
	- Promotion system (array of ranks class instances which alter movement speed, health points, bullet movement speed, their damage and if they can destroy metal tiles)
- Enemy:
	- Health system (every unit can take different amount of bullets before it destroy itself)
	- AI detecting which direction choose when it detects an obstacle (**uses raycasting with a layer mask**)
	- Bonus type spawning random bonus when is hit by the player
- Bonuses:
	- **Shield** (gives the player temporary invincibility against enemy projectiles)
	- **Freeze** (temporarily freezes all enemies, both already on the map and every spawned)
	- **Fortress** (surrounds the nuke with metal tiles which change itself into brick tiles after a short time)
	- **Rank** (promotes the player upgrading his capabilities and firing power)
	- **Destruction** (destroys all enemies on the map, **does not give points for them though**)
	- **Life** (gives the player additional life)

## Usage
- Press W / S / A / D keys or arrow keys to navigate through menus & control the player one robot
- Press LMB / Space to shoot
- Press RMB / Esc to pause

## Differences
Although I did my best to keep the biggest part of the original game unchanged, there **ARE** some significant differences (some of them improve flexibility) and may not be noticeable at first sight.
- Overall
	- **The whole graphics were made by myself**
- Main Menu
	- The **Construction** option was replaced with the **Exit** (instead of the builtin level editor, there is a system which read JSON files containing stage data about tiles placement and enemies layout)
	- The moving parts of the background for the stage number counter **are being moved horizontally**, not vertically as in the original
- Stage Selection
	- **There is no constant amount of stages** (35) - here it depends on put JSON files into the "Stages" directory which are automatically loaded by the stages loader component
- Score
	- **The constant number of enemy types** (4) **was removed**, here is dependant of defeated types of enemies and can take from 0 to N

## Project status
<p align = "center"><b>IN PROGRESS</b></p>

---
**The project is completed in ~80%**. It still requires fixes, optimisations and upgrades. Detailed information about left tasks were written below (tasks are split by the scenes):

```[tasklist]
- [ ] STAGE SELECTION
	- [X] Return to the main menu
	- [X] Automatic load of JSON files in directory
	- [ ] Inclusion of case when there are no stages to select
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
		- [ ] Growth of difficulty everytime when beaten the last stage
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
	- [ ] Refactoring of code
```

## Screenshots
!["Rumble In The Factory"](./Screenshots/1.png?raw=true)
!["Rumble In The Factory"](./Screenshots/2.png?raw=true)
!["Rumble In The Factory"](./Screenshots/3.png?raw=true)
!["Rumble In The Factory"](./Screenshots/4.png?raw=true)
!["Rumble In The Factory"](./Screenshots/5.png?raw=true)
!["Rumble In The Factory"](./Screenshots/6.png?raw=true)

## Credits
This project was made by [Jason](https://jasonxiii.pl "Jason. Cała informatyka w jednym miejscu! Oficjalna strona internetowa! Setki artykułów na różne tematy! Wszystko stworzone przez jedną osobę!").
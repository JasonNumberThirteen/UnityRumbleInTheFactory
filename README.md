# Rumble In The Factory
<p align = "center"><img src="./Assets/Project/Sprites/Single/Main%20Menu/GameLogo.png?raw=true" alt = "Rumble In The Factory"/></p>

> An arcade game inspired by "Battle City" (1985) made in "Unity" game engine.

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
- This is a "Unity" game project of my arcade game inspired by "**Battle City**", a popular top down shooter developed by [Namco](https://en.wikipedia.org/wiki/Namco "Namco - Wikipedia") in 1985.
- The project presents my programming skills and how I write code taking care of several optimisations.
- The game consists of defending the nuke against incoming robots. The player controls the orange robot (the 2nd player controls the green one) and must shoot projectiles to destroy enemy robots which are spawned on the top of the map.
- Win condition: all incoming enemy robots are destroyed.
- Lose condition: all the players were destroyed and lost all lives or the nuke is destroyed.
- The repository contains entire [Wiki](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki) about the game explaining all the technical details.

## Used technologies
- [Unity](https://unity.com/ "Unity Real-Time Development Platform | 3D, 2D, VR &amp; AR Engine") game engine (version **2022.3.62f2**),
- [Visual Studio Code](https://code.visualstudio.com/ "Visual Studio Code - Code Editing. Redefined") IDE for writing code,
- [GIMP](https://www.gimp.org/ "GIMP - GNU Image Manipulation Program") for making graphics,
- [Tiled](https://www.mapeditor.org/ "Tiled | Flexible level editor") for making stage layouts,
- [FamiStudio](https://famistudio.org/ "FamiStudio - NES Music Editor") for making music & sound effects.

## Features
- [7 scenes](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki/Scenes "Scenes"),
- [2 game modes](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki/Main-Menu#1-player "Main Menu"),
- 20 stages,
- [5 types of tiles](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki/Tiles "Tiles"),
- [4 enemy types](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki/Robots "Robots"),
- [6 bonuses](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki/Bonuses "Bonuses").

## Scenes
### Boot
A scene made for initialising saved data. It has no content for the player.

### Main Menu
A starting scene with option selection.

![Rumble In The Factory (Main Menu)](./Screenshots/MainMenu.png?raw=true)
### Stage Selection
A scene wherein you can select which stage you want to start from.

![Rumble In The Factory (Stage Selection)](./Screenshots/StageSelection.png?raw=true)
### Stage
A scene wherein the game is played on the current stage, firstly selected and later when the player is advanced to the next one.

![Rumble In The Factory (Stage)](./Screenshots/Stage.png?raw=true)
### Score
A scene which displays details about defeated enemies by each of the players (what types and how many units have been destroyed).

![Rumble In The Factory (Score)](./Screenshots/Score.png?raw=true)
### Game Over
A scene which shows a big "**GAME OVER**" text.

![Rumble In The Factory (Game Over)](./Screenshots/GameOver.png?raw=true)
### High Score
A scene which shows a big "**HISCORE**" text with a new high score when the player has beaten the high score.

![Rumble In The Factory (High Score)](./Screenshots/HighScore.png?raw=true)
## Usage
- W / S / A / D or arrow keys (keyboard), D-pad or left stick (gamepad) - **Navigation through menus**,
- W / S / A / D (keyboard), D-pad or left stick (gamepad) - **Movement (player 1)**,
- arrow keys, D-pad or left stick (gamepad) - **Movement (player 2)**,
- LMB (mouse), Space (keyboard), right trigger (gamepad) - **Shoot (player 1)**,
- Right Ctrl (keyboard), right trigger (gamepad) - **Shoot (player 2)**,
- Esc (keyboard), Start (gamepad) - **Pause**.

The game supports gamepad input. Connecting it to PC allows you to play the game by using a gamepad. Depending on selected game mode, input for both the players is adjusted as follows:
| Player | Keyboard input | Gamepad input |
| :---: | :---: | :---: |
| 1 | If a gamepad is **not** connected or selected 2 player mode | If a gamepad **is** connected and selected 1 player mode
| 2 | If a gamepad is **not** connected | If a gamepad **is** connected and selected 2 player mode

## Differences
The game **has some important differences** compared to the original, both visually and functionally. Please see the Wiki page linked [here](https://github.com/JasonNumberThirteen/UnityRumbleInTheFactory/wiki/Differences-to-the-original-game "Differences to the original game") to display detailed information.

## Project status
<p align = "center"><b>COMPLETED</b></p>
<p align = "center"><img src="https://upload.wikimedia.org/wikipedia/commons/f/f3/Gasr100percent.png"/></p>

---
<p align = "center"><b>The project is complete and ready to build</b>.</p>

## Credits
This project was made **entirely** by [Stanisław "Jason" Popowski](https://jasonxiii.pl "Jason. Cała informatyka w jednym miejscu! Oficjalna strona internetowa! Setki artykułów na różne tematy! Wszystko stworzone przez jedną osobę!").
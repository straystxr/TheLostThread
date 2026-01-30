# TheLostThread

The Lost Thread is a game that features a stuffed bear who has been separated from its owner. The player must navigate through various environments to reunite the bear with its owner, overcoming obstacles and solving puzzles along the way.
The game's aim is the puzzle adventure aspect of it and to escape from the junkyard where the bear finds itself at the start of the game. If the player solves the puzzle, player escapes, else the player remained trapped in the junkyard.

## Features

The system includes the following features:
- Puzzle-solving mechanics that involves interacting with objects in the environment.
- A variety of environments to explore, each with its own unique challenges.
- Puzzles with environmental settings that require basic problem-solving skills to solve.

## Core Mechanics
The core mechanics of The Lost Thread include:
- Player Movement that has a state machine within it to include idle, walking, running, and jumping animation states.
- Object Interaction that allows the player to pick up, drag, or interact with objects within the environment that has a radius of interaction, and the player can only interact with one object at a time. 
- A sub system for a specific puzzle that is based of the object interaction mechanic, where the player has to pick up and match the objects with their corresponding slots to solve the puzzle using read-only script and conditions to check when it matches or when it doesn't match.



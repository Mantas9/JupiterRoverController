# Jupiter Rover Controller

This is the documentation for the Jupiter Rover Controller project.

## Table of Contents:
1. Project Structure
2. Global Variables
3. Main Menu
4. Terrain Generation
5. Rover Deployment
6. Rover Movement
7. Extra Methods

##  1. Project Structure

The Jupiter Rover Controller is a C# Console application.
It consists of:
	1. Program.cs (Main class which handles all functionality) 
	2. Rover.cs (Model class of the rover containing its position and facing direction)
	3. Direction.cs (Enum for rover facing direction. Directions are placed clockwise for easy rotation)

## 2. Global Variables

#### Global variables consist of:
1. Terrain grid variables such as the terrain itself and its parameters
2. A Rover object

## 3. Main Menu

The Main menu lives in the Main class of Program.cs.
It is a while loop which receives key presses from the user and executes commands accordingly.

#### Current commands:
1. Generate Terrain
2. Deploy Rover
3. Move Rover
4. Toggle Full Terrain Display
5. Exit

## 4. Terrain Generation

The Terrain itself is a 2D Vector2 array which contains all of its coordinates.

#### Terrain Generation is split up into three methods:
1. void GenerateTerrain() (Calls input assign method, actually creates the terrain and calls the population method)
2. void AssignInputTerrainDimensions() (Gets user input and assigns global terrain option variables accordingly)
3. Vector2[,] PopulateTerrain(int width, int height) (Creates a 2D array, populates it with coordinates (Vector2) and returns it)

## 5. Rover Deployment

#### Rover Deployment consists of three methods:
1. void DeployRover() (Main method, called from MainMenu. Gets rover Coordinates and Direction by calling according methods and sets the global Rover variable)
2. Vector2 GetInputCoordinates() (Gets input coordinates, parses to actual coordinates, returns as Vector2)
3. Direction GetInputDirection() (Creates dictionary of all possible directions, prints all possible directions (ignores None), gets inputted direction and returns it)

## 6. Rover Movement

#### Rover Movement consists of three Methods:
1. void RoverMovement() (Main method, called from MainMenu. Registers user input, uses regex pattern to filter command, goes through all characters of command, executes Rover movement Methods according to command in a switch case)
2. void MoveRover(int direction) (Moves rover according to passed parameter direction and current direction of Rover.)
3. void RotateRover(int direction) (Changes Rover direction according to parameter by going through all possible directions, getting current direction and adding parameter value to currentDirection )

## 7. Extra Methods

bool ValidateCoordinates(Vector2 coordinates) (checks if passed coordinates are legal in terrain)

# Jupiter Rover Controller

This is the documentation for the Jupiter Rover Controller project.

## Table of Contents:
1. Project Structure
2. Terrain.cs
3. Direction.cs
4. Rover.cs
5. Program.cs
6. Input.cs
7. Output.cs

##  1. Project Structure

The Jupiter Rover Controller is a C# Console application.
It consists of:
	1. Models (Rover.cs, Terrain.cs, Direction.cs)
	2. Business Logic (Program.cs)
	3. Input (Input.cs)
	4. Output (Output.cs)

## 2. Terrain.cs

Terrain.cs holds 3 parameters: Width, Height and Grid.
The Grid parameter is generated in the constructor and it holds the coordinate values of the terrain

## 3. Direction.cs

Direction.cs is an enum containing basic map directions (North, East, South, West)

## 4. Rover.cs

Rover.cs is a class for the actual Rover.
It consists of a Position (Vect2) and Direction (Direction) properties;
Move() and Rotate() methods used to modify Position and Direction

## Program.cs

Program.cs is the main class which handles business logic and acts as a menu for executing tasks.

## 6. Input.cs

Input.cs has a bunch of static methods which return values from user keyboard input

## 7. Output.cs

Output.cs is used for displaying GUI elements in the application


using System.Numerics;
using JupiterRoverApplication.Models;

namespace JupiterRoverApplication;

public class Input
{
    public static Vector2 InpCoordinates()
    {
        // Input
        string[] input = InpString("Enter Coordinates separated by SPACE (e.g. 0 3): ").Split(' ');

        if (!int.TryParse(input[0], out int x) || // Check for valid values and parse input
            !int.TryParse(input[1], out int y))
        {
            Output.Error("Please enter valid coordinates!");
            return new Vector2(-1, -1);
        }

        return new Vector2(x, y);
    }

    public static Direction InpDirection()
    {
        // Get possible directions
        var possibleDirections = Program.PossibleDirections();

        // Output possible directions
        Output.PossibleDirections();

        // Get Input
        string input = InpString("Enter Direction: ").ToUpper();

        if (input == string.Empty || !possibleDirections.ContainsKey(input))
        {
            return Direction.None;
        }

        // Return dictionary value of selected direction
        return possibleDirections[input];
    }

    public static Rover? InpRover(Rover? info)
    {
        if(info is not null)
            return info;

        // Get Rover Coordinates
        Vector2 coordinates = InpCoordinates();

        if (!Program.ValidateCoordinates(coordinates))
            return null;

        // Get Rover Facing Direction
        Direction roverDirection = InpDirection();

        if (roverDirection == Direction.None)
            return null;

        return new Rover(coordinates, roverDirection);
    }

    public static Terrain? InpTerrain(int _width = 0, int _height = 0)
    {
        if(_width > 0 && _height > 0)
            return new Terrain(_width, _height);

        string[] input = InpString("Enter planet grid dimensions (separate by SPACE, e.g. 3 8): ").Split(' ');

        if (input.Length < 2)
            return null;

        if (!int.TryParse(input[0], out int width) || // Check for correct values and parse coordinates
            !int.TryParse(input[1], out int height) ||
            width < 0 || height < 0)
            return null;

        return new Terrain(width, height);
    }

    public static string InpString(string message = "") // Keyboard input
    {
        string? userInput;

        Console.Write(message);
        userInput = Console.ReadLine(); // Get written value

        if (userInput == null)
            return string.Empty;

        return userInput;
    }
}

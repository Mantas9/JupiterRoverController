using System.Numerics;
using System.Text.RegularExpressions;
using JupiterRoverApplication.Models;

namespace JupiterRoverApplication;

public class Program
{
    static bool displayFullTerrain = true;

    // Terrain
    public static Terrain? terrain;

    // Rover
    public static Rover? rover;

    static void Main() // Main Menu
    {
        while (true)
        {
            Output.MenuCommands();

            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine();

            switch (key)
            {
                case ConsoleKey.G: // Terrain Generation
                    GenerateTerrain();
                    break;
                case ConsoleKey.D: // Rover Deployment
                    DeployRover();
                    break;
                case ConsoleKey.M: // Rover Movement
                    RoverMovement();
                    break;
                case ConsoleKey.S: // Toggle grid display
                    ToggleTerrainDisplay();
                    break;
                case ConsoleKey.E: // Exit
                    return;
                default:
                    break;
            }

            Console.WriteLine("Press any key to Continue...");
            Console.ReadKey();
        }
    }

    public static void GenerateTerrain(int width = 0, int height = 0)
    {
        terrain = Input.InpTerrain(width, height);

        if (terrain is null)
            Output.Error("Please Enter valid Data!");
    }

    public static void DeployRover(Rover? info = null)
    {
        if(terrain is null)
        {
            Output.Error("Please Generate a Terrain first!");
            return;
        }

        rover = Input.InpRover(info);

        if (rover is null)
            return;

        Console.WriteLine($"Rover deployed at: {rover.Position}; Facing: {rover.Direction}");
    }

    public static void RoverMovement(string command = "")
    {
        if (terrain is null || rover is null)
        {
            Output.Error("Please set up terrain/rover!");
            return;
        }

        // Register regex
        string movementRegexPattern = "^[FBLR]+$";
        Regex regexMovement = new Regex(movementRegexPattern);

        string input;

        if(command != "")
            input = command.ToUpper();
        else
        {
            // Show possible commands
            Output.RoverMovementCommands();

            // Input
            input = Input.InpString("Enter command: ").ToUpper();
        }

        if (!regexMovement.IsMatch(input)) // Check if command is valid
        {
            Output.Error("Please enter valid command!");
            return;
        }

        // Go through every char in given command
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case 'F': // Forward
                    rover.Move(1);
                    break;
                case 'B': // Backwards
                    rover.Move(-1);
                    break;
                case 'R': // Right
                    rover.Rotate(1);
                    break;
                case 'L': // Left
                    rover.Rotate(-1);
                    break;
            }
        }

        Output.Success($"Rover moved to: {rover.Position}; Facing: {rover.Direction}");
    }

    public static Dictionary<string, Direction> PossibleDirections()
    {
        Dictionary<string, Direction> possibleDirections = new (); // Make a dictionary of possible options

        // Iterate through all options
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            string? name = Enum.GetName(typeof(Direction), direction); // Get value name

            if (name is null || direction == Direction.None) // Invalid value checks
                continue;

            possibleDirections.Add(name, direction); // Add value to dictionary
        }

        return possibleDirections;
    }

    public static bool ValidateCoordinates(Vector2 coordinates)
    {
        if (terrain == null || terrain.Grid.Length == 0)
            return false;

        for (int y = 0; y < terrain.Height; y++)
        {
            for (int x = 0; x < terrain.Width; x++)
            {
                if (terrain.Grid[x, y] == coordinates)
                    return true;
            }
        }

        return false;
    }

    public static void ToggleTerrainDisplay()
    {
        displayFullTerrain = !displayFullTerrain;
        Output.Success($"Full Terrain display set to: {displayFullTerrain}.\n");
    }
}
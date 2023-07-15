using JupiterRoverApplication.Models;

namespace JupiterRoverApplication;

internal class Output
{
    #region MainMenu
    public static void MenuCommands()
    {
        Console.WriteLine("Your possible commands are: ");
        Console.WriteLine("  G - Generate the planet terrain grid");
        Console.WriteLine("  D - Deploy the Rover in a specified position");
        Console.WriteLine("  M - Send Movement commands to the Rover!");
        Console.WriteLine("  S - Toggle Full Terrain display on Main Menu");
        Console.WriteLine("  Click any other key to exit application.\n");
    }
    #endregion

    #region Terrain
    public static void TerrainGrid(Terrain terrain, bool displayFull = true, Rover? rover = null)
    {
        if (terrain is null)
        {
            Error("No Terrain Input\n");
            return;
        }

        if (!displayFull)
        {
            Console.WriteLine($"Current Terrain of Dimensions: {terrain.Width}x{terrain.Height}.\n");
            return;
        }

        for (int y = 0; y < terrain.Height; y++)
        {
            for (int x = 0; x < terrain.Width; x++)
            {
                // Get Coordinates
                string textToWrite = $"{terrain.Grid[x, y]} ";

                // If rover is deployed
                if (rover is not null && rover.Position == terrain.Grid[x, y])
                    textToWrite = $"<{terrain.Grid[x, y]}> ";

                Console.Write(textToWrite);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
    #endregion

    #region Rover
    public static void RoverInfo(Rover rover)
    {
        if (rover is null)
        {
            Error("Rover is not deployed.\n");
            return;
        }
        Console.WriteLine($"Rover deployed at Coordinates: {rover.Position}; Facing: {rover.Direction}\n");
    }

    public static void PossibleDirections()
    {
        Console.WriteLine("Possible Directions: ");

        foreach (var direction in Program.PossibleDirections())
        {
            Console.WriteLine($"  {direction.Key}");
        }
        Console.WriteLine();
    }

    public static void RoverMovementCommands()
    {
        Console.WriteLine("Rover commands:");
        Console.WriteLine("  F - Move forward");
        Console.WriteLine("  B - Move backwards");
        Console.WriteLine("  L - Turn left (stay on same coordinates)");
        Console.WriteLine("  R - Turn right (stay on same coordinates)\n");

        Console.WriteLine("Command example: FFRFF\n");
    }
    #endregion

    #region General
    public static void Error(string message)
    {
        Console.WriteLine("Error: " + message);
    }

    public static void Success(string message)
    {
        Console.WriteLine("Success: " + message);
    }
    #endregion
}

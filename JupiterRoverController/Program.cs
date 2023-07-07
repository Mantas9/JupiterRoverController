using System.Numerics;
using System.Text.RegularExpressions;

namespace MetasiteTask
{
    public class Program
    {
        // Planet grid variables
        static bool displayFullTerrain = true;
        static int terrainWidth;
        static int terrainHeight;
        static Vector2[,]? terrain;

        // Rover
        static Rover? rover;

        static void Main()
        {
            while (true) // Main Menu
            {
                DisplayMainMenu();

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
                        return;
                }

                Console.WriteLine("Press any key to Continue...");
                Console.ReadKey();
            }
        }

        #region Terrain Generation
        private static void GenerateTerrain()
        {
            AssignInputTerrainDimensions();

            // Create new planet grid and populate it with coordinates
            terrain = new Vector2[terrainWidth, terrainHeight];
            terrain = PopulateTerrain(terrainWidth, terrainHeight);

            Console.WriteLine($"Terrain of Dimensions: {terrainWidth}x{terrainHeight} successfully generated!\n");
        }

        private static void AssignInputTerrainDimensions()
        {
            string[] input = UserInput("Enter planet grid dimensions (separate by SPACE, e.g. 3 8): ").Split(' ');

            if (input.Length < 2)
            {
                Console.WriteLine("Please enter valid dimensions!");
                return;
            }

            if (!int.TryParse(input[0], out int width) || // Check for correct values and parse coordinates
                !int.TryParse(input[1], out int height) ||
                width < 0 || height < 0)
            {
                Console.WriteLine("Please enter valid dimensions!");
                return;
            }

            // Assign values
            terrainWidth = width;
            terrainHeight = height;
        }

        private static Vector2[,] PopulateTerrain(int width, int height)
        {
            Vector2[,] grid = new Vector2[width, height]; // Create 2D array

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = new Vector2(x, height - 1 - y); // Populate with coordinates
                }
            }

            return grid;
        }
        #endregion

        #region Rover
        #region Rover Deployment
        private static void DeployRover()
        {
            if (terrain == null)
            {
                Console.WriteLine("Please Generate a Terrain first!");
                return;
            }

            // Get Rover Coordinates
            Vector2 coordinates = GetInputCoordinates();
            if(!ValidateCoordinates(coordinates))
            {
                Console.WriteLine("Please enter valid coordinates!");
                return;
            }

            // Get Rover Facing Direction
            Direction roverDirection = GetInputDirection();
            if (roverDirection == Direction.None)
            {
                Console.WriteLine("Invalid rover direction!");
                return;
            }

            rover = new Rover // Deploy rover
            {
                Position = coordinates,
                Direction = roverDirection
            };

            Console.WriteLine($"Rover deployed at: {rover.Position}; Facing: {rover.Direction}");
        }

        private static Vector2 GetInputCoordinates()
        {
            // Input
            string[] input = UserInput("Enter Coordinates separated by SPACE (e.g. 0 3): ").Split(' ');

            if (!int.TryParse(input[0], out int x) || // Check for valid values and parse input
                !int.TryParse(input[1], out int y))
            {
                Console.WriteLine("Please enter valid coordinates!");
                return new Vector2(-1, -1);
            }

            return new Vector2(x, y);
        }

        private static Direction GetInputDirection()
        {
            Dictionary<string, Direction> possibleDirections = new(); // Make a dictionary of possible options
            Console.WriteLine("Possible Directions: "); // Print all possible options

            // Iterate through all options
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                string? name = Enum.GetName(typeof(Direction), direction); // Get value name

                if (name is null || direction == Direction.None) // Invalid value checks
                    continue;

                Console.WriteLine($"  {name}"); // Print value (Design)
                possibleDirections.Add(name, direction); // Add value to dictionary
            }
            Console.WriteLine();

            // Get Input
            string input = UserInput("Enter Direction: ").ToUpper();

            if (input == string.Empty || !possibleDirections.ContainsKey(input))
            {
                Console.WriteLine("Please Enter valid Direction!");
                return Direction.None;
            }

            // Return dictionary value of selected direction
            return possibleDirections[input];
        }
        #endregion

        #region Rover Movement
        private static void RoverMovement()
        {
            if(terrain is null || rover is null)
            {
                Console.WriteLine("Please set up terrain/rover!");
                return;
            }

            // Register regex
            string movementRegexPattern = "^[FBLR]+$";
            Regex regexMovement = new Regex(movementRegexPattern);

            DisplayRoverCommands();

            // Input
            string? input = UserInput("Enter command: ").ToUpper();
            if (input is null || !regexMovement.IsMatch(input)) // Check if command is valid
            {
                Console.WriteLine("Please enter valid command!");
                return;
            }

            // Go through every command character
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case 'F': // Forward
                        MoveRover(1);
                        break;
                    case 'B':
                        MoveRover(-1);
                        break;
                    case 'R':
                        RotateRover(1);
                        break;
                    case 'L':
                        RotateRover(-1);
                        break;
                }
            }

            Console.WriteLine("Execution Successful.");
            Console.WriteLine($"Rover moved to: {rover.Position}; Facing: {rover.Direction}");
        }

        private static void MoveRover(int direction)
        {
            if (rover is null)
                return;

            var position = rover.Position;

            // North and South work with the Y coordinates
            // East and West work with the X coordinates
            switch (rover.Direction)
            {
                case Direction.N: // North
                    position.Y += direction;
                    break;
                case Direction.E: // East
                    position.X += direction;
                    break;
                case Direction.S: // South
                    position.Y += -direction;
                    break;
                case Direction.W: // West
                    position.X += -direction;
                    break;
            }

            if(!ValidateCoordinates(position))
            {
                Console.WriteLine("Invalid Input. Rover would be out of bounds!");
                Console.WriteLine("Skipping...");
                return;
            }

            rover.Position = position;
        }

        private static void RotateRover(int direction)
        {
            if (rover is null)
                return;

            // Get maximum direction value
            int maxDirection = Enum.GetValues(typeof(Direction)).Length - 1; // Exclude value None

            int currentDirection = (int)rover.Direction;

            currentDirection += direction;

            if(currentDirection > maxDirection) // If wanted direction is more than max, restart cycle
                currentDirection = 1;
            if (currentDirection < 1) // If direction is less than min, go to last value
                currentDirection = maxDirection;

            rover.Direction = (Direction)currentDirection; // Assign direction
        }
        #endregion

        private static bool ValidateCoordinates(Vector2 coordinates)
        {
            if(terrain is null || terrain.Length == 0)
                return false;

            for (int y = 0; y < terrainHeight; y++)
            {
                for (int x = 0; x < terrainWidth; x++)
                {
                    if (terrain[x, y] == coordinates)
                        return true;
                }
            }

            return false;
        }
        #endregion

        #region GUI
        private static void ToggleTerrainDisplay()
        {
            displayFullTerrain = !displayFullTerrain;
            Console.WriteLine($"Full Terrain display set to: {displayFullTerrain}.\n");
        }

        public static void DisplayTerrainGrid()
        {
            if (terrain is null)
            {
                Console.WriteLine("No Terrain Generated.\n");
                return;
            }

            if (!displayFullTerrain)
            {
                Console.WriteLine($"Terrain of Dimensions: {terrainWidth}x{terrainHeight} Generated.\n");
                return;
            }

            for (int y = 0; y < terrainHeight; y++)
            {
                for (int x = 0; x < terrainWidth; x++)
                {
                    string textToWrite = $"{terrain[x, y]} ";

                    // Rover
                    if (rover is not null && rover.Position == terrain[x, y])
                        textToWrite = $"<{terrain[x, y]}> ";

                    Console.Write(textToWrite);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void DisplayRoverInfo()
        {
            if (rover is null)
            {
                Console.WriteLine("Rover is not deployed.\n");
                return;
            }

            Console.WriteLine($"Rover deployed at Coordinates: {rover.Position}; Facing: {rover.Direction}\n");
        }

        private static void DisplayRoverCommands()
        {
            Console.WriteLine("Rover commands:");
            Console.WriteLine("  F - Move forward");
            Console.WriteLine("  B - Move backwards");
            Console.WriteLine("  L - Turn left (stay on same coordinates)");
            Console.WriteLine("  R - Turn right (stay on same coordinates)\n");

            Console.WriteLine("Command example: FFRFF\n");
        }

        private static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Jupiter Rover Application!\n");

            DisplayRoverInfo();
            DisplayTerrainGrid(); // Display terrain grid if exists

            Console.WriteLine("Your possible commands are: ");
            Console.WriteLine("  G - Generate the planet terrain grid");
            Console.WriteLine("  D - Deploy the Rover in a specified position");
            Console.WriteLine("  M - Send Movement commands to the Rover!");
            Console.WriteLine("  S - Toggle Full Terrain display on Main Menu");
            Console.WriteLine("  Click any other key to exit application.\n");
        }

        private static string UserInput(string message = "") // Keyboard input
        {
            string? userInput;

            Console.Write(message);
            userInput = Console.ReadLine(); // Get written value

            if (userInput == null)
                return string.Empty;

            return userInput;
        }
        #endregion

    }
}
using System.Numerics;

namespace JupiterRoverApplication.Models;

public class Rover
{
    public Rover(Vector2 position, Direction direction)
    {
        Position = position;
        Direction = direction;
    }

    public Vector2 Position { get; private set; } // Current coordinates
    public Direction Direction { get; private set; } // Current facing direction

    public void Move(int direction)
    {
        var position = Position;

        // North and South work with the Y coordinates
        // East and West work with the X coordinates
        switch (Direction)
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

        if (!Program.ValidateCoordinates(position))
        {
            Console.WriteLine("Invalid Input. Rover would be out of bounds!");
            Console.WriteLine("Skipping...");
            return;
        }

        Position = position;
    }

    public void Rotate(int direction)
    {
        // Get maximum direction value
        int maxDirection = Enum.GetValues(typeof(Direction)).Length - 1; // Exclude value None

        // Get current direction
        int currentDirection = (int)Direction;

        // Make the move
        currentDirection += direction;

        if (currentDirection > maxDirection) // If wanted direction is more than max, restart cycle
            currentDirection = 1;
        if (currentDirection < 1) // If direction is less than min, go to last value
            currentDirection = maxDirection;

        Direction = (Direction)currentDirection; // Assign direction
    }
}
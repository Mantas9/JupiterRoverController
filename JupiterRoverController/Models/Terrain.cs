using System.Numerics;

namespace JupiterRoverApplication.Models;
public class Terrain
{
    public Terrain(int width, int height)
    {
        Width = width;
        Height = height;

        Grid = Generate(Width, Height);
    }

    public int Width { get; set; }
    public int Height { get; set; }

    public Vector2[,] Grid { get; private set; }

    private Vector2[,] Generate(int width, int height)
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
}

using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Linq;

public class MazeGenerator
{
    public static bool[,] GetMaze(int WightX, int WightY, int DeadEndRemovalCount, int ExtensionRoad)
    {
        bool[,] maze = new bool[WightX, WightY];

        for (int x = 0; x < WightX; x++)
        {
            for (int y = 0; y < WightY; y++)
            {
                maze[x, y] = true;
            }
        }

        RemoveWallsWithPrim(maze, WightX, WightY);
        RemoveDeadEnd(maze, WightX, WightY, DeadEndRemovalCount);
        AddExtensionRoad(maze, WightX, WightY, ExtensionRoad);
        
        return maze;
    }

    private static void RemoveWallsWithPrim(bool[,] maze, int WightX, int WightY)
    {
        int x = Random.Range(0, WightX / 2) *2 + 1;
        int y = Random.Range(0, WightY / 2) * 2 + 1;

        var direction = new List<Dict> { Dict.Up, Dict.Down, Dict.Left, Dict.Right };
        List<Dict> road = new List<Dict>();
        var check = new List<Point>();

        check.Add(new Point(x, y));

        while (check.Count > 0)
        {
            int index = Random.Range(0, check.Count);
            var point = check[index];
            x = point.X;
            y = point.Y;
            maze[x, y] = false;
            check.RemoveAt(index);

            road.AddRange(direction);

            while (road.Count() > 0)
            {
                int road_index = Random.Range(0, road.Count);
                switch (road[road_index])
                {
                    case Dict.Down:
                        if (y - 2 > 0 && !maze[x, y - 2])
                        {
                            maze[x, y - 1] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                    case Dict.Up:
                        if ((y + 2 < WightY) && !maze[x, y + 2])
                        {
                            maze[x, y + 1] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                    case Dict.Left:
                        if (x - 2 > 0 && !maze[x - 2, y])
                        {
                            maze[x - 1, y] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                    case Dict.Right:
                        if (x + 2 < WightX && !maze[x + 2, y])
                        {
                            maze[x + 1, y] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                }
            }

            var exist_top = check.Exists(c => c.X == x && c.Y == y + 2);
            var exist_bot = check.Exists(c => c.X == x && c.Y == y - 2);
            var exist_left = check.Exists(c => c.X == x - 2 && c.Y == y);
            var exist_right = check.Exists(c => c.X == x + 2 && c.Y == y);


            if (y - 2 > 0 && maze[x, y - 2] && !exist_bot)
            {
                check.Add(new Point(x, y - 2));
            }
            if ((y + 2 < WightY) && maze[x, y + 2] && !exist_top)
            {
                check.Add(new Point(x, y + 2));
            }
            if (x - 2 > 0 && maze[x - 2, y] && !exist_left)
            {
                check.Add(new Point(x - 2, y));
            }
            if (x + 2 < WightX && maze[x + 2, y] && !exist_right)
            {
                check.Add(new Point(x + 2, y));
            }
        }
    }
    private static void RemoveDeadEnd(bool[,] maze, int WightX, int WightY, int DeadEndRemovalCount)
    {
        for (int i = 0; i < DeadEndRemovalCount; i++)
        {
            var dead_ends = new List<Point>();

            for (int x = 0; x < WightX; x++)
            {
                for (int y = 0; y < WightY; y++)
                {
                    if (!maze[x, y])
                    {
                        int neighbors = 0;


                        if (y - 1 > 0 && !maze[x, y - 1])
                        {
                            neighbors++;
                        }
                        if (y + 1 < WightY && !maze[x, y + 1])
                        {
                            neighbors++;
                        }
                        if (x - 1 > 0 && !maze[x - 1, y])
                        {
                            neighbors++;
                        }
                        if (x + 1 < WightX && !maze[x + 1, y])
                        {
                            neighbors++;
                        }


                        if (neighbors == 1)
                        {
                            dead_ends.Add(new Point(x, y));
                        }
                    }
                }
            }
            foreach (var end in dead_ends)
            {
                maze[end.X, end.Y] = true;
            }
            dead_ends.Clear();
        }
    }
    private static void AddExtensionRoad(bool[,] maze, int WightX, int WightY, int ExtensionRoad)
    {
        for (int i = 0; i < ExtensionRoad; i++)
        {
            var new_cells = new List<Point>();

            for (int x = 0; x < WightX; x++)
            {
                for (int y = 0; y < WightY; y++)
                {
                    if (maze[x, y])
                    {
                        int neighbors = 0;


                        for (int a = -1; a < 2; a++)
                        {
                            for (int b = -1; b < 2; b++)
                            {
                                if (!(a == 0 && b == 0))
                                {
                                    int neighbor_x = x + a;
                                    int neighbor_y = y + b;


                                    if ((neighbor_x > 0) && (neighbor_x < WightX) && (neighbor_y > 0) && (neighbor_y < WightY))
                                    {
                                        if (!maze[neighbor_x, neighbor_y])
                                        {
                                            neighbors++;
                                        }
                                    }
                                }
                            }
                        }

                        if (neighbors > 3)
                        {
                            new_cells.Add(new Point(x, y));
                        }
                    }
                }
            }
            foreach (var space in new_cells)
            {
                maze[space.X, space.Y] = false;
            }
            new_cells.Clear();
        }
    }
}

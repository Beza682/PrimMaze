using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Linq;

public class MazeGenerator
{

    public int WightX = InputData.WightX;
    public int WightY = InputData.WightY;
    public bool[,] GenerateMaze()
    {
        bool[,] maze = new bool[WightX, WightY];

        for (int x = 0; x < WightX; x++)
        {
            for (int y = 0; y < WightY; y++)
            {
                maze[x, y] = true;
            }
        }

        RemoveWallsWithPrim(maze);
        RemoveDeadEnd(maze);
        //ExtensionRoad(maze);
        return maze;
    }

    private void RemoveWallsWithPrim(bool[,] maze)
    {
        int x = UnityEngine.Random.Range(0, WightX / 2) * 2 + 1;
        int y = UnityEngine.Random.Range(0, WightY / 2) * 2 + 1;

        var check = new List<Point>();

        check.Add(new Point(x, y));


        while (check.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, check.Count);
            var point = check[index];
            x = point.X;
            y = point.Y;
            maze[x, y] = false;
            check.RemoveAt(index);


            var road = new List<string>() { "top", "bot", "left", "right" };

            while (road.Count() > 0)
            {

                int road_index = UnityEngine.Random.Range(0, road.Count);
                var direction = road[road_index];
                switch (direction)
                {
                    case "top":
                        if (y - 2 >= 0 && (maze[x, y - 2] == false))
                        {
                            maze[x, y - 1] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                    case "bot":
                        if ((y + 2 < WightY) && (maze[x, y + 2] == false))
                        {
                            maze[x, y + 1] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                    case "left":
                        if (x - 2 >= 0 && (maze[x - 2, y] == false))
                        {
                            maze[x - 1, y] = false;
                            road.Clear();
                        }
                        else
                        {
                            road.RemoveAt(road_index);
                        }
                        break;
                    case "right":
                        if (x + 2 < WightX && (maze[x + 2, y] == false))
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


            if (y - 2 >= 0 && (maze[x, y - 2] == true) && (exist_bot == false))
            {
                check.Add(new Point(x, y - 2));
            }
            if ((y + 2 < WightY) && (maze[x, y + 2] == true) && (exist_top == false))
            {
                check.Add(new Point(x, y + 2));
            }
            if (x - 2 >= 0 && (maze[x - 2, y] == true) && (exist_left == false))
            {
                check.Add(new Point(x - 2, y));
            }
            if (x + 2 < WightX && (maze[x + 2, y] == true) && (exist_right == false))
            {
                check.Add(new Point(x + 2, y));
            }
        }
    }
    private void RemoveDeadEnd(bool[,] maze)
    {
        for (int i = 0; i < 2; i++)
        {
            int x;
            int y;

            var dead_ends = new List<Point>();

            for (x = 0; x < WightX; x++)
            {
                for (y = 0; y < WightY; y++)
                {
                    if (maze[x, y] == false)
                    {
                        int neighbors = 0;

                        if (y - 1 >= 0 && (maze[x, y - 1] == false))
                        {
                            neighbors++;
                        }
                        if (y + 1 < WightY && (maze[x, y + 1] == false))
                        {
                            neighbors++;
                        }
                        if (x - 1 >= 0 && (maze[x - 1, y] == false))
                        {
                            neighbors++;
                        }
                        if (x + 1 < WightX && (maze[x - 1, y] == false))
                        {
                            neighbors++;
                        }
                        if (neighbors <= 1)
                        {
                            dead_ends.Add(new Point(x, y));
                        }
                    }
                }
            }
            while (dead_ends.Count > 0)
            {
                int dead_index = UnityEngine.Random.Range(0, dead_ends.Count);
                var point = dead_ends[dead_index];
                x = point.X;
                y = point.Y;
                maze[x, y] = true;
                dead_ends.RemoveAt(dead_index);
            }
        }
    }
    //private void ExtensionRoad(bool[,] maze)
    //{
    //    for (int i = 0; i < 1; i++)
    //    {
    //        int x;
    //        int y;

    //        var new_cells = new List<Point>();
    //        for (x = 0; x < WightX; x++)
    //        {
    //            for (y = 0; y < WightY; y++)
    //            {
    //                if (maze[x, y] == true)
    //                {
    //                    int neighbors = 0;
    //                    for (int a = 0; a < 3; a++)
    //                    {
    //                        for (int b = 0; b < 3; b++)
    //                        {
    //                            int neighbor_x = x - a;
    //                            int neighbor_y = x - b;
    //                            if (neighbor_x >= 0 && neighbor_x < WightX && neighbor_y >= 0 && neighbor_y < WightY)
    //                            {
    //                                if (maze[neighbor_x, neighbor_y] == false)
    //                                {
    //                                    neighbors++;
    //                                }
    //                            }
    //                        }
    //                    }
    //                    if (neighbors >= 4)
    //                    {
    //                        new_cells.Add(new Point(x, y));
    //                    }
    //                }
    //            }
    //        }

    //        while (new_cells.Count > 0)
    //        {
    //            int new_index = UnityEngine.Random.Range(0, new_cells.Count);
    //            var point = new_cells[new_index];
    //            x = point.X;
    //            y = point.Y;
    //            maze[x, y] = false;
    //            new_cells.RemoveAt(new_index);
    //        }
    //    }
    //}
}

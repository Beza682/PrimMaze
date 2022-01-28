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
    public int Iteration_dead_ends = InputData.Iteration_dead_ends;
    public int Iteration_extension_road = InputData.Iteration_extension_road;


    InputData Data = InputData.GetInstance(); //Ссылаюсь на скрипт с данными


    public bool[,] GenerateMaze()
    {
        Debug.Log(Data.ExpZ); //Проверяю, что переменная пришла

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
        ExtensionRoad(maze);
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


            var road = new List<Dict>() { Dict.Up, Dict.Down, Dict.Left, Dict.Right };

            while (road.Count() > 0)
            {

                int road_index = UnityEngine.Random.Range(0, road.Count);
                var direction = road[road_index];
                switch (direction)
                {
                    case Dict.Down:
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
                    case Dict.Up:
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
                    case Dict.Left:
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
                    case Dict.Right:
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
        for (int i = 0; i < Iteration_dead_ends; i++)
        {
            var dead_ends = new List<Point>();

            for (int x = 0; x < WightX; x++)
            {
                for (int y = 0; y < WightY; y++)
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
                        if (x + 1 < WightX && (maze[x + 1, y] == false))
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
            foreach (var end in dead_ends)
            {
                maze[end.X, end.Y] = true;
            }
            dead_ends.Clear();
        }
    }
    private void ExtensionRoad(bool[,] maze)
    {
        for (int i = 0; i < Iteration_extension_road; i++)
        {

            var new_cells = new List<Point>();
            for (int x = 0; x < WightX; x++)
            {
                for (int y = 0; y < WightY; y++)
                {
                    if (maze[x, y] == true)
                    {
                        int neighbors = 0;
                        for (int a = -1; a <= 1; a++)
                        {
                            for (int b = -1; b <= 1; b++)
                            {
                                int neighbor_x = x + a;
                                int neighbor_y = y + b;
                                if ((neighbor_x >= 0) && (neighbor_x < WightX) && (neighbor_y >= 0) && (neighbor_y < WightY))
                                {
                                    if (maze[neighbor_x, neighbor_y] == false)
                                    {
                                        neighbors++;
                                    }
                                }
                            }
                        }
                        if (neighbors >= 4)
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

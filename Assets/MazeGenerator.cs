using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


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
                maze[x, y] = true  /*!((x % 2 != 0 && y % 2 != 0) && (x < WightX - 1 && y < WightY - 1))*/;
            }
        }

        RemoveWallsWithPrim(maze);
        return maze;
    }

    private void RemoveWallsWithPrim(bool[,] maze)
    {
        int x = UnityEngine.Random.Range(0, WightX / 2) * 2 + 1;
        int y = UnityEngine.Random.Range(0, WightY / 2) * 2 + 1;
        maze[x, y] = false;

        List<Tuple<int, int>> check = new List<Tuple<int, int>>();
        //List<bool> check = new List<bool>();
        if (y - 2 >= 0)
        {
            check.Add(new Tuple<int, int>(x, y - 2));
            //check.Add(maze[x, y - 2]);
        }
        if (y + 2 < WightY)
        {
            check.Add(new Tuple<int, int>(x, y + 2));
            //check.Add(maze[x, y + 2]);
        }
        if (x - 2 >= 0)
        {
            check.Add(new Tuple<int, int>(x - 2, y));
            //check.Add(maze[x - 2, y]);
        }
        if (x + 2 < WightX)
        {
            check.Add(new Tuple<int, int>(x + 2, y));
            //check.Add(maze[x + 2, y]);
        }
        do
        {
            int index = UnityEngine.Random.Range(0, check.Count);
            var point = check[index];
            x = point.Item1;
            y = point.Item2;
            maze[x, y] = false;
            check.RemoveAt(index);




            //if (y - 2 >= 0 && (maze[x, y - 2] = true))
            //{
            //    check.Add(new Tuple<int, int>(x, y - 2));
            //}
            //if (y + 2 < WightY && (maze[x, y + 2] = true))
            //{
            //    check.Add(new Tuple<int, int>(x, y + 2));
            //}
            //if (x - 2 >= 0 && (maze[x - 2, y] = true))
            //{
            //    check.Add(new Tuple<int, int>(x - 2, y));
            //}
            //if (x + 2 < WightX && (maze[x + 2, y] = true))
            //{
            //    check.Add(new Tuple<int, int>(x + 2, y));
            //}
        }
        while (check.Count > 0);
    }
}

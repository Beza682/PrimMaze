using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public bool[,] cells;
}
public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool Wall = true;

    public bool WallVertical = true;
    public bool WallHorizontal = true;

    public bool Visited = false;
}

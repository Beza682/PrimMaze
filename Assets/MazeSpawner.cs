using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject WallPrefab;
    public bool[,] maze;
    public void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int x = 0; x < generator.WightX; x++)
        {
            for (int y = 0; y < generator.WightY; y++)
            {

                Walls w = Instantiate(WallPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<Walls>();

                w.Wall.SetActive(maze[x, y]);
                //c.WallVertical.SetActive(maze[x, y].WallVertical);
                //c.WallHorizontal.SetActive(maze[x, y].WallHorizontal);
            }
        }
    }
}

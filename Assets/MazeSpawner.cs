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

        for (int x = 0; x < MazeGenerator.WightX; x++)
        {
            for (int y = 0; y < MazeGenerator.WightY; y++)
            {

                var walls = Instantiate(WallPrefab, new Vector2(x, y), Quaternion.identity);

                walls.SetActive(maze[x, y]);
            }
        }
    }
}

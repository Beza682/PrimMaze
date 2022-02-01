using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject WallPrefab;
    public InputData Data;

    public void Start()
    {
        if (Data.WightX > 2 && Data.WightX % 2 != 0 && Data.WightY > 2 && Data.WightY % 2 != 0 && Data.DeadEndRemovalCount > -1 && Data.ExtensionRoad > -1)
        {
            bool[,] maze = MazeGenerator.GetMaze(Data.WightX, Data.WightY, Data.DeadEndRemovalCount, Data.ExtensionRoad);
            for (int x = 0; x < Data.WightX; x++)
            {
                for (int y = 0; y < Data.WightY; y++)
                {
                    if (maze[x, y])
                    {
                        Instantiate(WallPrefab, new Vector2(x, y), Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            if (Data.WightX < 3)
            {
                Debug.Log("the value of the 'WightX' field must be greater than 3");
            }
            if (Data.WightX % 2 == 0)
            {
                Debug.Log("the value of the 'WightX' field must be odd");
            }
            if (Data.WightY < 3)
            {
                Debug.Log("the value of the 'WightY' field must be greater than 3");
            }
            if (Data.WightY % 2 == 0)
            {
                Debug.Log("the value of the 'WightY' field must be odd");
            }
            if (Data.DeadEndRemovalCount < 0)
            {
                Debug.Log("the field 'Dead End Removal Count' can only contain positive numbers");
            }
            if (Data.ExtensionRoad < 0)
            {
                Debug.Log("the field 'Extension Road' can only contain positive numbers");
            }
        }
    }
}

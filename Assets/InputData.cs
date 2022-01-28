using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData : MonoBehaviour
{
    public int WightX;
    public int WightY;
    public int DeadEndRemovalCount;
    public int ExtensionRoad;

    public void Start()
    {
        MazeGenerator.Data(WightX, WightY, DeadEndRemovalCount, ExtensionRoad);
    }
}

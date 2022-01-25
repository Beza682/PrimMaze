using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData : MonoBehaviour
{
    public int X;
    public int Y;

    public static int WightX;
    public static int WightY;

    public void Start()
    {
        var WX = X;
        WightX = WX;
        var WY = Y;
        WightY = WY;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {

        }
    }
}

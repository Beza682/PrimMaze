using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class InputData : MonoBehaviour
{
    //MazeGenerator generator;
    //[SerializeField]
    public int X;
    public int Y;

    public static int WightX;
    public static int WightY;


    public void Start()
    {
        //Debug.Log(X);

        //generator.WightX = X;
        //generator.WightY = Y;
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

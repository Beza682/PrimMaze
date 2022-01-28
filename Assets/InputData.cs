using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData : MonoBehaviour
{

    public int Z; //Переменная для тестирования передачи переменной в генератор

    //Начало синглтона
    private static readonly InputData instance = new InputData();
    public int ExpZ { get; private set; } 
    private InputData()
    {
        ExpZ = Z;
    }

    public static InputData GetInstance()
    {
        return instance;
    }
    //Конец 


    public int X;
    public int Y;
    public int DeadEndRemovalCount;
    public int ExtensionRoad;


    public static int WightX;
    public static int WightY;
    public static int Iteration_dead_ends;
    public static int Iteration_extension_road;

    


    public void Start()
    {
        Debug.Log(Z);  //Смотрю значение переменной из инспектора
        Debug.Log(ExpZ); //Смотри, чтобы сработало присваивание


        var WX = X;
        WightX = WX;
        var WY = Y;
        WightY = WY;
        var DE = DeadEndRemovalCount;
        Iteration_dead_ends = DE;
        var ER = ExtensionRoad;
        Iteration_extension_road = ER;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {

        }
    }
}

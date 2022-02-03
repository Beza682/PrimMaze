using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

static class ExtensionMethods
{
    public static T GetRandom<T> (this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}

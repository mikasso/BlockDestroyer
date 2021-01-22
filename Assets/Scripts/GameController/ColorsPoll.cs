using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsPoll : MonoBehaviour
{
    // Start is called before the first frame update
    Color[] colors;
    int size = 100;
    
    public static Color RandomColor()
    {
        return UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.75f, 1f);
    }
    void Awake()
    {
        colors = new Color[size];
        for (int i = 0; i < size; i++)
            colors[i] = RandomColor();
    }

    public Color GetRandomColor()
    {
        int i = Random.Range(0, size);
        return colors[i];
    }

    
}

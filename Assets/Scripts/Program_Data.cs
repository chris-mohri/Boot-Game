using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Program Data")]
public class Program_Data : ScriptableObject
{
    public Sprite sprite;

    public double cpu_multiplier;

    public double gpu_multiplier;

    public double ram_multiplier;

    public double cooling_multiplier;

    public double storage_multiplier;
}

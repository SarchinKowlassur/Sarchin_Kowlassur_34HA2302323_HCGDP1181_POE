using System;
using UnityEngine;

[Serializable]
public class Trait
{
    [SerializeField] private string name;
    [SerializeField] private float value;
    [SerializeField] private float maxValue;
    [SerializeField] [TextArea(3,10)] private string description;


    public Trait(string name, float value, string description)
    {
        this.name = name;
        this.value = value;
        this.maxValue = value;
        this.description = description;
    }
    
    public string Name { get { return name; } set { name = value; } }
    public float Value { get { return value; } set { this.value = value; } }
    public float MaxValue { get { return maxValue; } set { maxValue = value; } }
    public string Description { get { return description; } set { description = value; } }
}

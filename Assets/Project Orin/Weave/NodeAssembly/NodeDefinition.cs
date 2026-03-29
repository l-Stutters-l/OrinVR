using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Orin/Node")]
public class NodeDefinition : ScriptableObject
{
    public string NodeName;

    public List<string> Inputs = new();
    public List<string> Outputs = new();

    public Color NodeColor = Color.gray;
}
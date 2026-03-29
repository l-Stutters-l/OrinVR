using System.Collections.Generic;
using Project_Orin.Core.Interfaces;
using UnityEngine;

public class WorldElement : IWorldElement
{
    public string Name { get; set; }
    public IWorldElement Parent { get; set; }
    public List<IComponent> Components { get; private set; } = new List<IComponent>();

    public GameObject GameObject { get; private set; }

    public WorldElement(string name, GameObject go)
    {
        Name = name;
        GameObject = go;
    }

    // Call OnUpdate for all components
    public void Update(float deltaTime)
    {
        foreach (var comp in Components)
        {
            comp.OnUpdate(deltaTime);
        }
    }
}

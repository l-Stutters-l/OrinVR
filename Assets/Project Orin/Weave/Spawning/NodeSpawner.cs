using UnityEngine;
using System;

public class NodeSpawner : MonoBehaviour
{
    public GameObject NodePrefab;
    public NodeRegistry Registry;

    public void Spawn(Type type, Vector3 pos, Quaternion rot)
    {
        INode node = Registry.CreateInstance(type);

        GameObject go = Instantiate(NodePrefab, pos, rot);

        NodeVisual visual = go.GetComponent<NodeVisual>();
        visual.Initialize(node);
    }
}
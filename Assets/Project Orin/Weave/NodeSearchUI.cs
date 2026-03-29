using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class NodeSearchUI : MonoBehaviour
{
    public TMP_InputField InputField;
    public Transform ContentParent;
    public GameObject ResultPrefab;

    public NodeRegistry Registry;
    public NodeSelection Selection;

    void Start()
    {
        InputField.onValueChanged.AddListener(OnSearchChanged);

        // 🔥 Show everything at start
        ShowAllNodes();
    }

    void ShowAllNodes()
    {
        Clear();

        var allNodes = Registry.GetAll();

        foreach (var type in allNodes)
        {
            CreateResult(type);
        }
    }

    void Refresh(List<System.Type> nodes)
    {
        Clear();

        foreach (var type in nodes)
        {
            CreateResult(type);
        }
    }

    void Clear()
    {
        for (int i = ContentParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(ContentParent.GetChild(i).gameObject);
        }
    }

    void OnSearchChanged(string text)
    {
        var results = string.IsNullOrEmpty(text)
            ? Registry.GetAll()
            : Registry.Search(text);

        Refresh(results);
    }

    void CreateResult(System.Type type)
    {
        GameObject go = Instantiate(ResultPrefab, ContentParent);

        NodeDoubleClick item = go.GetComponent<NodeDoubleClick>();
        item.Initialize(type, Selection);
    }
}
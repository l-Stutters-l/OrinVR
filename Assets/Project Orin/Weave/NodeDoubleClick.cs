using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NodeDoubleClick : MonoBehaviour
{
    public TMP_Text Label; // ← use TMP if you're using TextMeshPro

    private Type _type;
    private NodeSelection _selection;

    private float _lastClickTime;
    private float _doubleClickThreshold = 0.3f;

    public void Initialize(Type type, NodeSelection selection)
    {
        _type = type;
        _selection = selection;

        INode node = (INode)Activator.CreateInstance(type);

        // 🔥 THIS is what sets the label
        if (Label != null)
            Label.text = node.Name;
        else
            Debug.LogError("Label not assigned on NodeDoubleClick!");

        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        float time = Time.time;

        if (time - _lastClickTime < _doubleClickThreshold)
        {
            _selection.Set(_type);
            Debug.Log("Selected: " + _type.Name);
        }

        _lastClickTime = time;
    }
}
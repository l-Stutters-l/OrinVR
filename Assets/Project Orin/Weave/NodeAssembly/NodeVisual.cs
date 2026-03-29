using UnityEngine;
using UnityEngine.UI;

public class NodeVisual : MonoBehaviour
{
    public Text NameText;
    public Image Background;

    public Transform InputParent;
    public Transform OutputParent;

    public GameObject PortPrefab;

    private INode _node;

    public void Initialize(INode node)
    {
        _node = node;

        if (NameText != null)
            NameText.text = node.Name;

        if (Background != null)
            Background.color = node.Color;

        GeneratePorts();
    }

    void GeneratePorts()
    {
        ClearPorts();

        foreach (var input in _node.Inputs)
            CreatePort(input, InputParent);

        foreach (var output in _node.Outputs)
            CreatePort(output, OutputParent);
    }

    void CreatePort(string portName, Transform parent)
    {
        GameObject portObj = Instantiate(PortPrefab, parent);

        PortVisual port = portObj.GetComponent<PortVisual>();

        if (port != null)
            port.Initialize(portName);
        else
            Debug.LogError("PortPrefab missing PortVisual!");
    }

    void ClearPorts()
    {
        foreach (Transform child in InputParent)
            Destroy(child.gameObject);

        foreach (Transform child in OutputParent)
            Destroy(child.gameObject);
    }
}
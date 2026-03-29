using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance;

    public PortVisual CurrentPort;
    public LineRenderer CurrentLine;

    public GameObject LinePrefab;

    void Awake()
    {
        Instance = this;
    }

    public void StartConnection(PortVisual port)
    {
        CurrentPort = port;

        GameObject lineObj = Instantiate(LinePrefab);
        CurrentLine = lineObj.GetComponent<LineRenderer>();
    }

    public void Update()
    {
        if (CurrentLine != null && CurrentPort != null)
        {
            CurrentLine.SetPosition(0, CurrentPort.transform.position);

            // follow mouse (TEMP, will switch to VR later)
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 2f;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            CurrentLine.SetPosition(1, worldPos);
        }
    }

    public void CompleteConnection(PortVisual target)
    {
        if (CurrentLine == null || CurrentPort == null)
            return;

        CurrentLine.SetPosition(1, target.transform.position);

        Debug.Log("Connected " + CurrentPort.name + " → " + target.name);

        CurrentPort = null;
        CurrentLine = null;
    }
}
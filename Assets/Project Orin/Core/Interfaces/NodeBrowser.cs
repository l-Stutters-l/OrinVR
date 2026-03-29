using UnityEngine;
using UnityEngine.XR;

public class NodeBrowser : MonoBehaviour
{
    public GameObject Canvas;
    public float distance = 1f;
    public float heightOffset = 0.9f;

    private bool _lastButtonState = false;

    public void NodeBrowserSpawner()
    {
        Transform cam = Camera.main.transform;
        Vector3 yOffset = new Vector3(0, heightOffset, 0);
        Vector3 targetPos = cam.position + (cam.forward - yOffset) * distance;

        Canvas.transform.position = targetPos;
        Canvas.transform.LookAt(cam);
    }

    void Update()
    {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        bool buttonPressed;

        if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed))
        {
            if (buttonPressed && !_lastButtonState)
            {
                Canvas.SetActive(!Canvas.activeSelf);
                NodeBrowserSpawner();
            }

            _lastButtonState = buttonPressed;
        }
    }
}
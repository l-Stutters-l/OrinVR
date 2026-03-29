using UnityEngine;
using UnityEngine.XR;

public class NodeSpawnFromController : MonoBehaviour
{
    public NodeSelection Selection;
    public NodeSpawner Spawner;
    public Transform SpawnPoint;

    private bool _lastState;

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool pressed;

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out pressed))
        {
            if (pressed && !_lastState)
            {
                Spawn();
            }

            _lastState = pressed;
        }
    }

    void Spawn()
    {
        if (Selection.SelectedNodeType == null)
        {
            Debug.Log("No node selected");
            return;
        }

        Spawner.Spawn(
            Selection.SelectedNodeType,
            SpawnPoint.position,
            SpawnPoint.rotation
        );
    }
}
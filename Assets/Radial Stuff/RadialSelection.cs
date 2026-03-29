using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.InputSystem;

public class RadialSelection : MonoBehaviour
{
    public InputActionReference spawnButton;
    public InputAction SecondaryButton;

    [Range(2, 10)]
    public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenRadialPart;
    public Transform handTransform;

    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (spawnButton.action.WasPressedThisFrame())
        {
            ToggleMenu();
        }

        if (isMenuOpen)
        {
            GetSelectedRadialPart();
        }

        if (spawnButton.action.WasPressedThisFrame())
        {
            Debug.Log("Menu button pressed");
        }
    }

    private bool isMenuOpen = false;

    void OnEnable()
    {
        spawnButton.action.Enable();
    }

    void OnDisable()
    {
        spawnButton.action.Disable();
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            SpawnRadialPart();
        }
        else
        {
            HideMenu();
        }
    }

    void HideMenu()
    {
        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);


        if (angle < 0)
            angle += 360;

        Debug.Log("ANGLE " + angle);

        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if(i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.cyan;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }

            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
            
    }

    
    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;
        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();
    
        for (int i = 0; i < numberOfRadialPart; i++)
        {

            float angle = -i * 360 / numberOfRadialPart + angleBetweenRadialPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position  = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;
            spawnedRadialPart.GetComponent<Image>().fillAmount = (1 / (float)numberOfRadialPart) - (angleBetweenRadialPart / 360);
            //GameObject radialPart = Instantiate(radialPartPrefab, transform);
            //radialPart.transform.localRotation = Quaternion.Euler(0, 0, (360 / numberOfRadialPart) * i);

            spawnedParts.Add(spawnedRadialPart);
        }

    }
    
}

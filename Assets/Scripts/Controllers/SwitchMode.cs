using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class SwitchMode : MonoBehaviour
{
    public GameObject model;
    public GameObject target;
    public Camera ARCamera;
    public Camera NCamera;
    [SerializeField] private GameObject trackerWarning;
    public GameObject overlayPanel;
    public GameObject modeButtonText;
    //public Canvas overlayCanvas; // Separate canvas for the overlay panel

    public bool isARMode = false;

    public float rotationSpeed = 10f;

    private bool rotating = false;

    public void Switcher()
    {
        if (isARMode)
        {
            SwitchTo3D();
            UpdateButtons("AR");
        }
        else
        {
            SwitchToAR();
            UpdateButtons("3D");
        }

        isARMode = !isARMode;
    }

    public void SwitchTo3D()
    {
        // Reset position and rotation
        model.transform.SetParent(null);
        model.transform.position = Vector3.zero;
        model.transform.rotation = Quaternion.identity;

        AdJustRenderersColliders(model, true);

        // Disable AR camera and Vuforia tracking
        ARCamera.gameObject.SetActive(false);
        var vuforiaBehaviour = ARCamera.GetComponent<VuforiaBehaviour>();
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = false; // Disable Tracking
        }

        // Enable 3D camera
        NCamera.gameObject.SetActive(true);

        UpdateLabelCameras(NCamera);
        UpdateRaycastingCamera(NCamera);

        // Update the overlay canvas for 3D mode
        //if (overlayCanvas != null)
        //{
        //    overlayCanvas.gameObject.SetActive(false); // Temporarily disable to refresh
        //    overlayCanvas.renderMode = RenderMode.ScreenSpaceOverlay; // Set to 2D mode
        //    overlayCanvas.worldCamera = null; // Remove world camera reference
        //    overlayCanvas.transform.localPosition = Vector3.zero; // Reset position
        //    overlayCanvas.transform.localScale = Vector3.one; // Reset scale
        //    overlayCanvas.gameObject.SetActive(true); // Re-enable canvas
        //}

        // Ensure the tracker warning is hidden in 3D mode
        if (trackerWarning != null)
        {
            trackerWarning.SetActive(false);
        }

        Debug.Log("Switched to 3D");

        rotating = true; // Enable rotation in 3D mode


    }

    public void SwitchToAR()
    {
        rotating = false; // Disable rotation in AR mode

        // Reset position and rotation
        model.transform.SetParent(target.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;


        // Enable AR camera and Vuforia tracking
        ARCamera.gameObject.SetActive(true);
        var vuforiaBehaviour = ARCamera.GetComponent<VuforiaBehaviour>();
        if (vuforiaBehaviour != null)
        {
            vuforiaBehaviour.enabled = true; // Enable Tracking
        }

        UpdateLabelCameras(ARCamera);
        UpdateRaycastingCamera(ARCamera);

        // Disable 3D camera
        NCamera.gameObject.SetActive(false);

        // Update the overlay canvas for AR mode
        //if (overlayCanvas != null)
        //{
        //    overlayCanvas.gameObject.SetActive(false); // Temporarily disable to refresh
        //    overlayCanvas.renderMode = RenderMode.WorldSpace; // Set to World Space
        //    overlayCanvas.worldCamera = ARCamera; // Assign the AR camera
        //    overlayCanvas.transform.position = target.transform.position + new Vector3(0, 0.2f, 0); // Adjust position
        //    overlayCanvas.transform.localScale = Vector3.one * 0.01f; // Adjust scale
        //    overlayCanvas.gameObject.SetActive(true); // Re-enable canvas
        //}

        // Ensure the tracker warning is hidden in AR mode
        //if (trackerWarning != null)
        //{
        //    trackerWarning.SetActive(false);
        //}

        Debug.Log("Switched to AR");
    }

    private void AdJustRenderersColliders(GameObject obj, bool value)
    {
        if (model == null) return;

        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = value;
        }

        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = value;
        }

        foreach (Transform child in obj.transform)
        {
            AdJustRenderersColliders(child.gameObject, value);
        }
    }

    private void UpdateLabelCameras(Camera targetCamera)
    {
        RotateTosCamera[] labelScripts = model.GetComponentsInChildren<RotateTosCamera>();

        foreach (RotateTosCamera script in labelScripts)
        {
            script.SetCamera(targetCamera);
        }
    }

    private void UpdateRaycastingCamera(Camera targetCamera)
    {
        ApplicationController appController = FindObjectOfType<ApplicationController>();
        if (appController != null)
        {
            appController.SetCamera(targetCamera);
        }
    }

    void Start()
    {
        if (trackerWarning != null)
        {
            trackerWarning.SetActive(false); // Ensure the tracker warning is hidden on startup
        }

        AdJustRenderersColliders(model, false);
        Application.targetFrameRate = 60;

        isARMode = false; // Ensure starting in 3D mode
        rotating = true; // Enable rotation in 3D mode
        SwitchTo3D(); // Initialize in 3D mode

        UpdateButtons("AR"); // Set button Text to AR initially
    }

    void Update()
    {
        if (rotating && !isARMode)
        {
            model.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    private void UpdateButtons(string text)
    {
        if (modeButtonText != null)
        {
            TextMeshProUGUI buttonText = modeButtonText.GetComponent<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = text;
            }
        }
    }

    
    public void OnTargetLost()
    {
        if (isARMode && trackerWarning != null)
        {
            trackerWarning.SetActive(true); // Show tracker warning only in AR mod
            Debug.Log("Target Lost");
        }
    }

    public void OnTargetFound()
    {
        if (trackerWarning != null)
        {
            trackerWarning.SetActive(false); // Hide tracker warning
            Debug.Log("Target Found");
        }
    }
}

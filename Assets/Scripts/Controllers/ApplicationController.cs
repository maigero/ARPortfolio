using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ApplicationController : MonoBehaviour
{
    [SerializeField] InputActionReference confirmAction; // Reference for clicks
    [SerializeField] InputActionReference positionAction; // Reference for pointer position
    [SerializeField] private Camera camera; // Raycasting-camera
    [SerializeField] GameObject menuPanel;
    [SerializeField] UISoundController UIsoundController;

    public void SetCamera(Camera newCamera)
    {
        camera = newCamera;
        Debug.Log($"Raycasting camera updated to: {camera.name}");
    }



    // Quit application
    public void Quit()
    {
        menuPanel.SetActive(false);
        Debug.Log($"Application Quit");
        Application.Quit();
    }
    // Reload scene
    public void Reset()
    {
        menuPanel.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


    // Update is called once per frame
    private void Update()
    {
        // When clicked
        if (confirmAction.action.triggered)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Pointer on UI. Ignoring click.");
                return;
            }

            Vector2 position = positionAction.action.ReadValue<Vector2>(); // Read click position
            Debug.Log($"clicked at position {position}");

            Ray ray = camera.ScreenPointToRay( position ); // Casts ray
            RaycastHit hit;


            if (Physics.Raycast( ray, out hit ) ) // if ray hit and object
            {
                Debug.Log("object hit: " + hit.transform.name);

                if (hit.transform.CompareTag("Center")) // if hit was on center, set all fields inactive
                {
                    ModelController.Instance.SetFieldActive(null, true);
                }

                else
                {
                    ModelController.Instance.SetFieldActive(hit.transform.gameObject, true); // if hit was on a field, set it active

                    UIsoundController.OnPointerClickOpen();

                }
            }
            else
            {
                Debug.Log("no object hit");
                ModelController.Instance.SetFieldActive(null, true); // if nothing was hit, reset the active field
            }
        }
    }
}

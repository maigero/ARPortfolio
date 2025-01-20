using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.UI.CanvasScaler;

public class ModelController : MonoBehaviour
{
    // References to the fields models
    [SerializeField] InfoController RollABall;
    [SerializeField] InfoController VRMaze;
    [SerializeField] InfoController TheDoor;
    [SerializeField] InfoController Bendy;
    [SerializeField] OverlayController overlayController;

    public static ModelController Instance; // Singleton instance
    private InfoController currentActive; // Currently active controller

    // Initialize singleton instance
    void Start()
    {
        Instance = this;
    }


    public void SetFieldActive(GameObject ob,bool value)
    {
     
        if (currentActive != null && (ob == null || currentActive.gameObject != ob)) // When switching to a different field
        {
            currentActive.SetSelected(false); // Deselect current field
            currentActive.Activate(true); // Reactivate rotation
            currentActive = null;

            overlayController.closePanel();
        }

        if(ob != null)
        {
            var infoController = ob.GetComponentInParent<InfoController>(); // Get the infoController for the current field
            if (infoController != null )
            {
                
                if(currentActive != infoController) // if field is a different field
                {
                    infoController.SetSelected(true); // Mark object as selected
                    infoController.Activate(false); // Disables rotation
                }      

                currentActive = infoController; // Sets field as active

                string ShortName = infoController.GetShortName();
                string details = infoController.GetDetails();
                VideoClip videoClip = infoController.GetVideoClip();

                if (overlayController != null)
                {
                    overlayController.DisplayInfo(ShortName, details, videoClip); // Update Panel
                }
            }
         
        }

    }

    public void DeselectCurrentField()
    {
        if (currentActive != null)
        {
            currentActive.SetSelected(false);
            currentActive.Activate(true);
            currentActive = null;
        }
    }

}

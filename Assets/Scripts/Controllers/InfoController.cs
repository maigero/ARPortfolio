using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class InfoController : MonoBehaviour
{
    [SerializeField] string ShortName; // short name of the field
    [SerializeField] string Details; // Description of the field
    [SerializeField] VideoClip videoClip;

    Rotator rotator; // Reference to the rotator on the object
    Animator animator; // Reference to the animator on the object


    // Start is called before the first frame update
    void Start()
    {
        rotator = transform.Find("Model").GetComponent<Rotator>(); // Finds the rotator on the object
        Transform label = transform.Find("Label/Text (TMP)"); // finds the texts objects

        animator = transform.Find("Model").GetComponent<Animator>(); // finds the animator on the object
        if(animator == null)
        {
            Debug.LogError("Animator component missing on " + gameObject.name);
        }

        if(animator != null)
        {
            animator.SetBool("isSelected", false); // sets animation to default-state
        }
    }

    // Activate and deactivate rotation based on state
    public void Activate(bool value)
    {
        if (rotator != null)
        {
            rotator.enabled = value;
        }
        
    }
    // Sets an object as selected and triggers animation
    public void SetSelected(bool selected)
    {
        if (animator != null)
        {
            animator.SetBool("isSelected", selected);
        }
    }

    // Returns the field description
    public string GetDetails()
    {
        return Details;
    }
    // Returns the fields short name
    public string GetShortName()
    {
        return ShortName;
    }

    public VideoClip GetVideoClip()
    {
        return videoClip;
    }

}

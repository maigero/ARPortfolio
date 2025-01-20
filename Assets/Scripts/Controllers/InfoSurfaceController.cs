using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoSurfaceController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI infoText;

    /// <summary>
    /// Enables or disables text on the canvas based on the fields active state 
    /// </summary>
    /// <param name="s"></param>

    public void setText (string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            gameObject.SetActive (false); // Hide text
        }

        else
        {
            infoText.text = s;
            gameObject.SetActive (true); // Display text
        }
    }
}

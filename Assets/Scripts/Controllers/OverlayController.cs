using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class OverlayController : MonoBehaviour
{
    [SerializeField] GameObject overlayPanel;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI detailsText;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject videoPanel;
    [SerializeField] VideoController videoController;

    public void DisplayInfo(string shortName, string details, VideoClip videoClip)
    {
        overlayPanel.SetActive(true);

        titleText.text = shortName;
        detailsText.text = details;


        if(videoClip != null)
        {
            videoController.ResetVideoPanel();
            videoPlayer.clip = videoClip;
            videoPlayer.Pause();
            videoPlayer.time = 0;

            //videoController.ResetSlider();
        }
    }



    public void closePanel()
    {
        overlayPanel.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
        
        if (ModelController.Instance != null)
        {
            ModelController.Instance.DeselectCurrentField();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

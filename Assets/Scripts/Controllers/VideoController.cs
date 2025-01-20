using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer; // Reference to the VideoPlayer
    [SerializeField] Button toggleButton;     // Single button for Play/Pause
    [SerializeField] Slider videoSlider;      // Slider for video progress
    [SerializeField] Image buttonIcon;
    [SerializeField] Sprite playIcon;
    [SerializeField] Sprite pauseIcon;

    private bool usingSlider = false;

    void Start()
    {
        // Ensure the video is paused at the start
        videoPlayer.Pause();

        // Set button interaction
        toggleButton.onClick.AddListener(TogglePlayPause);

        // Set slider event listeners
        videoSlider.onValueChanged.AddListener(SliderValueChanged);
        videoSlider.minValue = 0;
        videoSlider.maxValue = 1;
        videoSlider.value = 0;

        UpdateButtonText(); // Update button text to reflect the initial state
    }

    void Update()
    {
        // Update slider only when not using it and video is playing
        if (!usingSlider && videoPlayer.isPlaying)
        {
            videoSlider.value = (float)(videoPlayer.time / videoPlayer.length);
        }
    }

    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }

        UpdateButtonText(); // Update button text/icon
    }

    public void SliderValueChanged(float value)
    {
        if (usingSlider)
        {
            double newTime = value * videoPlayer.length;
            videoPlayer.time = newTime;
        }
    }

    public void OnSliderDragStart()
    {
        usingSlider = true;
    }

    public void OnSliderDragEnd()
    {
        usingSlider = false;
        // Ensure the video seeks to the correct position when dragging ends
        SliderValueChanged(videoSlider.value);
    }

    private void UpdateButtonText()
    {
        if (videoPlayer.isPlaying)
        {
            buttonIcon.sprite = pauseIcon;
        }
        else
        {
            buttonIcon.sprite = playIcon;
        }
    }

    public void ResetVideoPanel()
    {
        if(videoSlider != null)
        {
            videoPlayer.Stop();
            videoSlider.value = 0;
            UpdateButtonText();
        
        }   
    }


}

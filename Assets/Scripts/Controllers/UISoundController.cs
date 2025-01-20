using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip clickedSound;
    public AudioClip closeSound;
    private AudioSource audioSource;
    public Sprite mutedImage;
    public Image muteButtonImage;
    public Sprite unmutedImage;

    private bool isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter()
    {
        if(hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerClickOpen()
    {
        if(clickedSound != null)
        {
            audioSource.PlayOneShot(clickedSound);
            Debug.Log("Click Sound played!");
        }
    }

    public void OnPointerClickClose()
    {
        if(closeSound != null)
        {
            audioSource.PlayOneShot(closeSound);
        }
    }

    public void ToggleMute()
    {
        if(audioSource != null)
        {
            isMuted = !isMuted;
            audioSource.volume = isMuted ? 0 : 1;

            // Update the Button
            UpdateMuteButtonImage();
        }
    }

    public void UpdateMuteButtonImage()
    {
        if(muteButtonImage != null)
        {           
            muteButtonImage.sprite = isMuted ? mutedImage : unmutedImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

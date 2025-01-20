using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using CUAS;
using System.IO;


/// <summary>
/// Using a file as videosource
/// </summary>
public class VideoSourceFile : MonoBehaviour, CUAS.IVideoSource
{

    [SerializeField]
    RawImage videoSurface; //display

    [SerializeField]
    string deviceName; //not used right now

    [SerializeField]
    Vector2 focalLenghts;

    [SerializeField]
    Vector2 principalPoint;

    [SerializeField]
    Vector4 distortion;


    VideoPlayer vp; //video source

    RenderTexture rt = null;

    Texture2D tex = null; //texture for the image

    public GameObject grabButton;
    public GameObject playPauseButton;

    bool grab = false;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {

        vp = GetComponent<VideoPlayer>();

        playPauseButton.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        if (vp.isPlaying)
        {

            if (rt == null)
            {

                rt = new RenderTexture((int)vp.width, (int)vp.height, 32);
                vp.targetTexture = rt;

            }

            if (tex == null || tex.width != rt.width || tex.height != rt.height)
            {

                Debug.Log("VideoSource: new texture: " + rt.width + ", " + rt.height);
                tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

                videoSurface.enabled = true;
                videoSurface.texture = tex;

                videoSurface.rectTransform.sizeDelta = new Vector2(rt.width, rt.height);

            }

            RenderTexture.active = rt;
            tex.ReadPixels(new UnityEngine.Rect(0, 0, rt.width, rt.height), 0, 0, false); //read to cpu (slow, but ok for this prototype)

            tex.Apply(); //upload to gpu for rendering

            if (grab)
            {

                Debug.Log("grabbing...");

                //write...

                byte[] bytes = tex.EncodeToJPG();

                // Write the returned byte array to a file in the project folder
                File.WriteAllBytes(Application.dataPath + "/../frame_" + index + ".jpg", bytes);

                index++;

                grab = false;

            }

            //grabButton.SetActive(true);

        }

    } //update


    public Texture2D GetFrame()
    {
        return tex;

    }


    public Vector2 GetFocalLengths()
    {
        return focalLenghts;
    }

    public Vector2 GetPrincipalPoint()
    {

        return principalPoint;

    }

    public Vector4 GetDistortion()
    {

        return distortion;

    }


    public void OnButtonPauseResume()
    {

        if (vp.isPlaying)
        {

            vp.Pause();

        }
        else
        {

            vp.Play();

        }

    }


    public void OnButtonGrab()
    {

        if (!grab)
        {
            
            grab = true;

        }


    }

}

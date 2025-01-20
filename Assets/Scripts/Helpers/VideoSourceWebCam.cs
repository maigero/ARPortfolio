using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using CUAS;
using System.IO;

/// <summary>
/// Using the webcam as videosource
/// </summary>
public class VideoSourceWebCam : MonoBehaviour, CUAS.IVideoSource
{

    [SerializeField]
    RawImage videoSurface; //display

    [SerializeField]
    string deviceName;

    [SerializeField]
    Vector2 focalLenghts;

    [SerializeField]
    Vector2 principalPoint;

    [SerializeField]
    Vector4 distortion;


    Texture2D tex = null; //texture for the image


    [SerializeField]
    Vector2 resolution = new Vector2(1920, 1080);

    WebCamTexture webcamTexture = null;

    bool grab = false;

    int index = 0;


    public GameObject grabButton;


    // Start is called before the first frame update
    void Start()
    {
        //list devices
        WebCamDevice[] devices = WebCamTexture.devices;

        int idx = -1;
        for (int i = 0; i < devices.Length; i++)
        {

            Debug.Log("device: " + devices[i].name);

            if (devices[i].name == deviceName || deviceName.Length == 0)
            {

                idx = i;

                Debug.Log("found at index: " + idx);
            }

        }


        if (idx >= 0)
        {

            videoSurface.enabled = true;

            float a = resolution[0] / (float)resolution[1];

            //videoSurface.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height * a, Screen.height);

            videoSurface.rectTransform.sizeDelta = new Vector2(resolution[0], resolution[1]);

            webcamTexture = new WebCamTexture(deviceName, (int)resolution.x, (int)resolution.y);

            videoSurface.texture = webcamTexture;

            webcamTexture.Play();

            //grabButton.SetActive(true);

        }



    }

    // Update is called once per frame
    void Update()
    {


        if (webcamTexture != null && webcamTexture.isPlaying)
        {

            if (tex == null || tex.width != webcamTexture.width || tex.height != webcamTexture.height)
            {

                tex = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
                //tex = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
            }

            tex.SetPixels32(webcamTexture.GetPixels32());

            tex.Apply();

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


    public void OnButtonGrab()
    {
        if (!grab)
        {

            grab = true;

        }


    }

}

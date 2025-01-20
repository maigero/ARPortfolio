using UnityEngine;

public class RotateTosCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private Transform textTransform;

    void Start()
    {
        if (camera == null)
            camera = Camera.main;

        // Reference the Text (TMP) child to apply rotation only to it
        textTransform = transform.GetChild(0).transform;
    }

    void Update()
    {
        // Rotate the text to face the camera while keeping local position
        Vector3 direction = camera.transform.position - textTransform.position;
        direction.y = 0; // Keep text upright
        textTransform.rotation = Quaternion.LookRotation(-direction);
    }

    public void SetCamera(Camera newCamera)
    {
        camera = newCamera;
    }
}

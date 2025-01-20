using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePanel : MonoBehaviour
{
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = camera.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}

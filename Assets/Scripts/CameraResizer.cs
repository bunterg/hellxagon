using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    public float ScrollForce = 1f;
    public float movementForce = 1f;
    public float CameraSizeMin = 1f;
    public float CameraSizeMax = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cameraSize = Camera.main.orthographicSize + Input.mouseScrollDelta.y * ScrollForce;
        if(CameraSizeMin < cameraSize && CameraSizeMax > cameraSize)
            Camera.main.orthographicSize = cameraSize;

        Vector3 cameraMovement = new Vector3();

        if (Input.GetKey(KeyCode.Mouse2))
        {
            cameraMovement.x = Input.GetAxis("Mouse X");
            cameraMovement.y = Input.GetAxis("Mouse Y");
        }
        else
        {
            if (Input.GetKey("up"))
            {
                cameraMovement += Vector3.up;
            }

            if (Input.GetKey("down"))
            {
                cameraMovement += Vector3.down;
            }

            if (Input.GetKey("left"))
            {
                cameraMovement += Vector3.left;
            }

            if (Input.GetKey("right"))
            {
                cameraMovement += Vector3.right;
            }
        }

        Camera.main.transform.position += cameraMovement.normalized * movementForce;
    }
}

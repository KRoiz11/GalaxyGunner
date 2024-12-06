using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // sensitivites for horizontal and vertical mouse movement
    public float SensX;
    public float SensY;
    //orientation of the player model that follows the camera
    public Transform orientation;
    //X and Y rotation of the camera
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//keeps the cursor at the center of the screen
        Cursor.visible = false;//makes the cursor invisible
    }

    // Update is called once per frame
    private void Update()
    {
        // to get mouse input
        float mouseY =  Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;
        float mouseX =  Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;

        //update rotation based on mouse input
        yRotation += mouseX;
        xRotation -= mouseY;

        // to rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        //clamps vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
    }
}
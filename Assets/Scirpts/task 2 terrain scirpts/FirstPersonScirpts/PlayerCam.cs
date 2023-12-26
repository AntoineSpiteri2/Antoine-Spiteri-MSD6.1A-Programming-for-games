using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;


    public Transform orientation;

    float xRotation;
    float yRotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mousex;

        xRotation -= mousey;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}

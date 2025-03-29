using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float xRotation ;
    public float yRotation ;
    public Transform Player;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
 

        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity * Time.deltaTime;
    

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * inputX);


        }
}

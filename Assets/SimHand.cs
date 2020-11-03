using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHand : MonoBehaviour
{
    public float mouseSensitivity;
    public float moveSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // If the mouse has moved left or right
        var mouseHorizontalDelta = Input.GetAxis("Mouse X");
        if (mouseHorizontalDelta != 0)
        {
            // Rotate the hand about the X axis by how much the mouse has moved left or right and based on how much time has passed
            transform.Rotate(Vector3.up, mouseHorizontalDelta * mouseSensitivity * Time.deltaTime, Space.World);
        }

        // If the mouse has moved up or down
        var mouseVericalDelta = Input.GetAxis("Mouse Y");
        if (mouseVericalDelta != 0)
        {
            // Rotate the hand about the Y axis by how much the mouse has moved up or down and based on how much time has passed
            transform.Rotate(Vector3.left, mouseVericalDelta * mouseSensitivity * Time.deltaTime);
        }

        // If the W key is pressed
        if(Input.GetKey(KeyCode.W))
        {
            // Move forwards
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }

        // If the S key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            // Move backwards
            transform.Translate(transform.forward * -moveSpeed * Time.deltaTime, Space.World);
        }
    }
}

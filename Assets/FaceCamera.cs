using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        // Face this object's transform towards the camera (useful for making sure the UI is always visible)
        transform.LookAt(Camera.main.transform);
    }
}

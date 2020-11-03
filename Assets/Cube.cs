using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Color highlightedColour;
    private Color initialColour;

    void Start()
    {
        initialColour = GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collider is the hand
        if(other.gameObject.tag == "Hand")
        {
            // Change the color of the cube
            GetComponent<MeshRenderer>().material.color = highlightedColour;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the collider is the hand
        if (other.gameObject.tag == "Hand")
        {
            // Restore the color of the cube
            GetComponent<MeshRenderer>().material.color = initialColour;
        }
    }
}

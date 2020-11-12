using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTurn : MonoBehaviour
{
    public string horizontalAxisName;
    private bool justTurned;
    public Transform player;
    public float degreesPerTurn;

    void Update()
    {
        // If the player pushes the thumbstick to the left
        if(!justTurned && Input.GetAxis(horizontalAxisName) < -0.5f)
        {
            // Rotate the player a certain amount to the left
            player.Rotate(0, -degreesPerTurn, 0);

            // Make sure that we don't keep rotating
            justTurned = true;
        }

        // If the player pushes the thumbstick to the right
        if (!justTurned && Input.GetAxis(horizontalAxisName) > 0.5f)
        {
            // Rotate the player a certain amount to the right
            player.Rotate(0, degreesPerTurn, 0);

            // Make sure that we don't keep rotating
            justTurned = true;
        }

        // If the player has let go of the thumbstick
        if(Input.GetAxis(horizontalAxisName) > -0.5f && Input.GetAxis(horizontalAxisName) < 0.5f)
        {
            // Allow them to snap turn again
            justTurned = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string triggerButtonName;
    private LineRenderer beam;
    public float teleportRange;
    private bool isTriggerHeld;

    void Start()
    {
        beam = GetComponent<LineRenderer>();

        // Hide the beam at first
        beam.enabled = false;
    }

    void Update()
    {
        // If the trigger is pressed
        if (Input.GetButtonDown(triggerButtonName))
        {
            isTriggerHeld = true;

            // Update the beam
            UpdateBeam();

            // Show the beam
            beam.enabled = true;
        }

        // If the trigger is released
        if (Input.GetButtonUp(triggerButtonName))
        {
            isTriggerHeld = false;

            // Hide the beam
            beam.enabled = false;

            // TODO: Actually teleport the player
        }

        // If the trigger is being held
        if(isTriggerHeld)
        {
            // Update the beam
            UpdateBeam();
        }
    }

    private void UpdateBeam()
    {
        // Check if the beam hit anything
        if(Physics.Raycast(transform.position, transform.forward, out var hit, teleportRange))
        {
            // Set the start and end positions of the beam
            beam.SetPosition(0, transform.position);
            beam.SetPosition(1, hit.point);
        }
        else
        {
            // Hide the beam
            beam.enabled = false;
        }
    }
}

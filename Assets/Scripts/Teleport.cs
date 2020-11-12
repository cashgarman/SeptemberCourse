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
    private Vector3 teleportPosition;
    private bool isTeleportValid;
    public Transform teleportIndicator;
    public Color validColour;
    public Color invalidColour;
    public Transform player;

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

            // If we have a valid teleport position
            if(isTeleportValid)
            {
                // Teleport the player to the teleport target
                player.position = teleportPosition;

                // We no longer have a valid teleport
                isTeleportValid = false;

                // Hide the teleport indicator
                teleportIndicator.gameObject.SetActive(false);
            }
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

            // Show the beam
            beam.enabled = true;

            // If the target is a valid place to teleport to
            if(IsValidTeleportTarget(hit.collider))
            {
                // Store the valid teleport target
                teleportPosition = hit.point;
                isTeleportValid = true;

                // Show and position the teleport indicator
                teleportIndicator.position = teleportPosition + Vector3.up * 0.01f;
                teleportIndicator.gameObject.SetActive(true);

                // Change the colour of the beam to the valid colour
                beam.material.color = validColour;
            }
            // If the target is an invalid place to teleport to
            else
            {
                // Flag the target position as invalid
                isTeleportValid = false;

                // Hide the teleport indicator
                teleportIndicator.gameObject.SetActive(false);

                // Change the colour of the beam to the invalid colour
                beam.material.color = invalidColour;
            }
        }
        else
        {
            // Hide the beam
            beam.enabled = false;

            // Flag the target position as invalid
            isTeleportValid = false;
        }
    }

    private bool IsValidTeleportTarget(Collider target)
    {
        return target.gameObject.layer != LayerMask.NameToLayer("CantTeleportHere");
    }
}

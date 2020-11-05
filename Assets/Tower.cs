using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject rangeIndicator;

    private List<ZombieController> zombiesInRange = new List<ZombieController>();
    private ZombieController currentTarget;
    private float timeSinceLastFired;
    public float reloadTime;
    public float damagePerShot;

    void Update()
    {
        // If we have a current target
        if(currentTarget != null)
        {
            // If the target is dead
            if(currentTarget.dead)
            {
                // Remove it from the list of zombies in range
                zombiesInRange.Remove(currentTarget);

                // Find a new target
                FindNewTarget();
            }

            // If enough time has passed since we last fired
            if(Time.time - timeSinceLastFired > reloadTime && currentTarget != null)
            {
                // Do damage to the current target
                currentTarget.OnDamage(damagePerShot);

                // Update the last time we fired
                timeSinceLastFired = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a zombie has entered the range of the tower
        var zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            // Add the zombie to the list of zombies in range
            zombiesInRange.Add(zombie);

            Debug.Log($"Zombies in range: {zombiesInRange.Count}");

            // If the tower doesn't currently have a target
            if(currentTarget == null)
            {
                // Find a new target
                FindNewTarget();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If a zombie has left the range of the tower
        var zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            // Remove the zombie from the list of zombies in range
            zombiesInRange.Remove(zombie);

            Debug.Log($"Zombies in range: {zombiesInRange.Count}");

            // If the zombie that just left the tower's range was the current target
            if(zombie == currentTarget)
            {
                // Find a new target
                FindNewTarget();
            }
        }
    }

    private void FindNewTarget()
    {
        // Clear out any existing target
        currentTarget = null;

        // If there are any zombies in range
        if(zombiesInRange.Count > 0)
        {
            // Make the first zombie in the list the new current target
            currentTarget = zombiesInRange[0];

            Debug.Log($"New zombie target is {currentTarget.name}");
        }
        else
        {
            Debug.Log("No more zombies to target");
        }
    }

    internal void HideRangeIdicator()
    {
        rangeIndicator.SetActive(false);
    }
}

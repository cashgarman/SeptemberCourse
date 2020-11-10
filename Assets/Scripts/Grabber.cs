using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public string grabButtonName;
    private Animator animator;
    private Grabbable highlightedObject;
    private Grabbable grabbedObject;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the grip trigger button was pressed
        if(Input.GetButtonDown(grabButtonName))
        {
            // Play the gripping animation on the hand
            animator.SetBool("Gripped", true);

            // If we have a highlighted object
            if(highlightedObject != null)
            {
                // Grab the object
                GrabObject(highlightedObject);
            }
        }

        // If the grip trigger button was released
        if (Input.GetButtonUp(grabButtonName))
        {
            // Play the ungripping animation on the hand
            animator.SetBool("Gripped", false);

            // If we have a grabbed object
            if (grabbedObject != null)
            {
                // Drop the object
                DropObject();
            }
        }
    }

    private void GrabObject(Grabbable obj)
    {
        // Store the object that the hand is grabbing
        grabbedObject = obj;

        // Parent the object to the hand
        grabbedObject.transform.SetParent(transform);

        // Disable gravity on the object and make the object kinematic
        var rigidBody = grabbedObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
    }

    private void DropObject()
    {
        // Unparent the grabbed object
        grabbedObject.transform.SetParent(null);

        // Enable gravity and make the object non-kinematic
        var rigidBody = grabbedObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;

        // Clear the grabbed object
        grabbedObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we just touched is a grabbable object
        var grabbable = other.GetComponent<Grabbable>();
        if(grabbable != null)
        {
            // Highlight the grabbable object
            grabbable.OnHighlight();

            // Store the highlighted object
            highlightedObject = grabbable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object we stopped touching is a grabbable object
        var grabbable = other.GetComponent<Grabbable>();
        if (grabbable != null)
        {
            // Unhighlight the grabbable object
            grabbable.OnUnhighlight();

            // TODO: Modify this to handle multiple objects in range of the hand at once

            highlightedObject = null;
        }
    }
}

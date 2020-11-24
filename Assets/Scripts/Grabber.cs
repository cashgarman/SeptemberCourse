using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public string grabButtonName;
    public string triggerButtonName;
    private Animator animator;
    private Grabbable highlightedObject;
    private Grabbable grabbedObject;
    public Grabbable grabOnStart;

    void Start()
    {
        animator = GetComponent<Animator>();

        // If we should grab an object at the start
        if(grabOnStart != null)
        {
            GrabObject(grabOnStart);
        }
    }

    void Update()
    {
        // If the grip trigger button was pressed
        if (Input.GetButtonDown(grabButtonName))
        {
            // Play the gripping animation on the hand
            animator.SetBool("Gripped", true);

            // If we have a highlighted object
            if (highlightedObject != null)
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

            // If we have a grabbed object (and it can be dropped)
            if (grabbedObject != null && !grabbedObject.cantBeDropped)
            {
                // Drop the object
                DropObject();
            }
        }

        // If the trigger button was pressed
        if (Input.GetButtonDown(triggerButtonName))
        {
            // If we're grabbing an object
            if(grabbedObject)
            {
                // Let the object know a trigger has started being pressed
                grabbedObject.OnTriggerStart();
            }
        }

        // If the trigger button was rele4ased
        if (Input.GetButtonUp(triggerButtonName))
        {
            // If we're grabbing an object
            if (grabbedObject)
            {
                // Let the object know a trigger has stopped being pressed
                grabbedObject.OnTriggerEnd();
            }
        }
    }

    private void GrabObject(Grabbable obj)
    {
        // Store the object that the hand is grabbing
        grabbedObject = obj;

        // Parent the object to the hand
        //grabbedObject.transform.SetParent(transform);

        // Disable gravity on the object and make the object kinematic
        //var rigidBody = grabbedObject.GetComponent<Rigidbody>();
        //rigidBody.useGravity = false;
        //rigidBody.isKinematic = true;

        // Attach the object to the hand using a fixed joint
        var fixedJoint = grabbedObject.gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = GetComponent<Rigidbody>();

        // Let the grabbed object know it has been grabbed
        grabbedObject.OnGrabbed();
    }

    private void DropObject()
    {
        // Unparent the grabbed object
        //grabbedObject.transform.SetParent(null);

        // Enable gravity and make the object non-kinematic
        //var rigidBody = grabbedObject.GetComponent<Rigidbody>();
        //rigidBody.useGravity = true;
        //rigidBody.isKinematic = false;

        Destroy(grabbedObject.GetComponent<FixedJoint>());

        // Let the grabbed object know it has been dropped
        grabbedObject.OnDropped();

        // Clear the grabbed object
        grabbedObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we just touched is a grabbable object
        var grabbable = other.GetComponent<Grabbable>();
        if(grabbable != null)
        {
            // If we're already grabbing something
            if (grabbedObject != null)
            {
                // Let the grabbed object know that another object was highlighted
                grabbedObject.OnAnotherObjectHighlighted(grabbable);
            }
            // Otherwise, we're not currently grabbing something
            else
            {
                // Highlight the grabbable object
                grabbable.OnHighlight();

                // Store the highlighted object
                highlightedObject = grabbable;
            }
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

using System;
using UnityEngine;

public class Spraycan : Grabbable
{
    private bool spraying;
    private AudioSource sprayingSound;
    private int currentColourIndex;
    private Color currentColour;
    public Transform top;
    public Transform nozzle;
    public int range;
    public Transform splatPrefab;
    private float lastSplatTime;
    public float timeBetweenSplats;
    public string cycleColourButtonName;
    public Color[] colours;
    private Material topMaterial;

    void Start()
    {
        sprayingSound = GetComponent<AudioSource>();
        currentColourIndex = 0;
        currentColour = colours[currentColourIndex];
        topMaterial = top.GetComponent<MeshRenderer>().material;
        UpdateColour();
    }

    public override void OnHighlight()
    {
    }

    public override void OnUnhighlight()
    {
    }

    void Update()
    {
        // If we're spraying
        if(spraying)
        {
            // If enough time has past since the last splat
            if(Time.time - lastSplatTime >= timeBetweenSplats)
            {
                // Update the time of the last splat
                lastSplatTime = Time.time;

                // Create a splat
                PlaceSplat();
            }
        }

        // If the change colour button was pressed
        if (Input.GetButtonDown(cycleColourButtonName))
        {
            // Cycle the colour
            currentColourIndex++;
            if (currentColourIndex == colours.Length)
            {
                currentColourIndex = 0;
            }

            currentColour = colours[currentColourIndex];
            UpdateColour();
        }
    }

    private void UpdateColour()
    {
        // Change the colour of the top of the spraycan to match the current colour
        topMaterial.color = currentColour;
    }

    public override void OnTriggerStart()
    {
        base.OnTriggerStart();

        // Start spraying
        spraying = true;

        // Place the first splat
        PlaceSplat();

        // Start the spraying sound
        sprayingSound.Play();

        // Start the spray particles
    }

    private void PlaceSplat()
    {
        // If there is something to spray in range of the spray
        if (Physics.Raycast(nozzle.position, nozzle.forward, out var hit, range))
        {
            // Create the splat
            var splat = Instantiate(splatPrefab);

            // Position and orientate it
            splat.position = hit.point;

            // Set the splat's colour
            splat.GetComponent<MeshRenderer>().material.color = currentColour;

            // Scale the splat based on how far from the spraycan it is

            // Adjust the transparency of the splat based on the distance from the spraycan
        }
    }

    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();

        // Stop spraying
        spraying = false;

        // Stop the spraying sound
        sprayingSound.Stop();

        // Stop the spray particles
    }
}

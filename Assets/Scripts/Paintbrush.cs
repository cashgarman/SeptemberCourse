using System;
using System.Collections.Generic;
using UnityEngine;

public class Paintbrush : Grabbable
{
    private SphereCollider collider;
    private float initialSize;
    private Color currentColour;
    public LineRenderer paintStrokePrefab;
    private LineRenderer currentStroke;
    private bool painting;
    private List<Vector3> segmentPositions = new List<Vector3>();
    private Vector3 lastSegmentPosition;
    public float segmentLength = 0.01f;
    public PhysicalSlider slider;
    private float brushWidth;
    private float initialBrushWidth;

    protected override void Start()
    {
        base.Start();

        collider = GetComponent<SphereCollider>();
        initialSize = transform.localScale.magnitude;
        brushWidth = initialBrushWidth = paintStrokePrefab.startWidth;
    }

    void Update()
    {
        // If we're painting
        if(painting)
        {
            // Has the paintbrush moved enough from the last sgement position to warrent a new segment
            if(Vector3.Distance(transform.position, lastSegmentPosition) > segmentLength)
            {
                // Add a new segment
                segmentPositions.Add(transform.position);

                // Update the line renderer
                UpdateStroke();

                // Update the last sgement position
                lastSegmentPosition = transform.position;
            }
        }
    }

    public override void OnTriggerStart()
    {
        base.OnTriggerStart();

        // Create an instance of the paint stroke prefab
        currentStroke = Instantiate(paintStrokePrefab);

        // Set the colour of the paint stroke to match the paintbrush's current colour
        currentStroke.material.color = currentColour;

        // Set the width of the brush stroke
        currentStroke.startWidth = currentStroke.endWidth = brushWidth;

        // Flag the paintbrush as painting
        painting = true;

        // Clear any existing segments from the last paint stroke
        segmentPositions.Clear();

        // Add the paintbrush's position as the start of the first segment
        segmentPositions.Add(transform.position);

        // Update the line renderer
        UpdateStroke();

        // Update the last sgement position
        lastSegmentPosition = transform.position;
    }

    private void UpdateStroke()
    {
        // Update the line renderers actual positions based on our list of segment positions
        currentStroke.positionCount = segmentPositions.Count;
        currentStroke.SetPositions(segmentPositions.ToArray());
    }

    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();

        // Flag the paintbrush as not painting
        painting = false;
    }

    public override void OnGrabbed()
    {
        base.OnGrabbed();

        collider.isTrigger = true;
    }

    public override void OnDropped()
    {
        base.OnGrabbed();

        collider.isTrigger = false;
    }

    public override void OnAnotherObjectHighlighted(Grabbable otherObject)
    {
        base.OnAnotherObjectHighlighted(otherObject);

        var paintPot = otherObject.GetComponent<PaintPot>();
        if(paintPot != null)
        {
            // Change the current colour of the paintbrush to match the highlighted paint pot
            currentColour = paintPot.colour;
            GetComponent<MeshRenderer>().material.color = currentColour;
        }
    }

    public void SetBrushSize()
    {
        // Set the size of the paintbrush object
        var percent = slider.currentSliderValue;
        transform.localScale = Vector3.one * initialSize * percent;

        // Set the width of the brush stroke
        brushWidth = initialBrushWidth * percent;
    }
}

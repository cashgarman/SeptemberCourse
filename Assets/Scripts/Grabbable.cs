using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Material material;
    private Color initialColor;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        initialColor = material.color;
    }

    internal void OnHighlight()
    {
        material.color = Color.yellow;
    }

    internal void OnUnhighlight()
    {
        material.color = initialColor;
    }
}

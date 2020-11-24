using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Material material;
    private Color initialColor;
    public bool cantBeDropped;

    protected virtual void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        initialColor = material.color;
    }

    public virtual void OnHighlight()
    {
        material.color = Color.yellow;
    }

    public virtual void OnUnhighlight()
    {
        material.color = initialColor;
    }

    public virtual void OnGrabbed()
    {
    }
    
    public virtual void OnDropped()
    {
    }

    public virtual void OnTriggerStart()
    {
    }

    public virtual void OnTriggerEnd()
    {
    }

    public virtual void OnAnotherObjectHighlighted(Grabbable otherObject)
    {
    }
}

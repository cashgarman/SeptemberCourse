using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour
{
    private ConfigurableJoint button;
    private Material material;
    public Transform frame;
    public float pressedThreshold = 0.01f;
    public float minDistance = 1000f;
    public Color releasedColour;
    public Color pressedColour;
    private bool pushed;

    public UnityEvent onPressed;
    public UnityEvent onReleased;

    void Start()
    {
        button = GetComponentInChildren<ConfigurableJoint>();
        material = button.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        // Calculate the distance between the button and the button frame
        var distance = Vector3.Distance(button.transform.position, frame.position);

        //if(distance < minDistance)
        //{
        //    minDistance = distance;
        //}

        // Has the button bee pressed enough
        if(!pushed && distance < pressedThreshold)
        {
            pushed = true;
            material.color = pressedColour;
            onPressed.Invoke();
        }

        // Has the button been released
        if (pushed && distance > pressedThreshold)
        {
            pushed = false;
            material.color = releasedColour;
            onReleased.Invoke();
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

public class PhysicalSlider : MonoBehaviour
{
	private ConfigurableJoint joint;
	public Transform handle;
	public float currentSliderValue;
	public bool invert;
	public UnityEvent OnValueChanged;

	private void Start()
	{
		joint = handle.GetComponent<ConfigurableJoint>();
	}

	private void Update()
	{
		// Get the new value of the slider's handle
		var sliderValue = (handle.localPosition.x + joint.linearLimit.limit) / (joint.linearLimit.limit * 2);

		// Invert the value if necessary
		if (invert)
		{
			sliderValue = 1f - sliderValue;
		}

		if (sliderValue != currentSliderValue)
		{
			currentSliderValue = sliderValue;
			OnValueChanged.Invoke();
		}
	}
}

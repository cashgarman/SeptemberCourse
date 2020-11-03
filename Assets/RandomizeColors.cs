using UnityEngine;

public class RandomizeColors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RandomizeColor", .5f, .5f);
    }

    private void RandomizeColor()
    {
        GetComponent<MeshRenderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }
}

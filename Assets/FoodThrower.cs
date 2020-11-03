using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrower : MonoBehaviour
{
    public string triggerInputName;
    private bool triggerHeld = false;
    private GameObject currentFood;
    public GameObject[] foodstuffs;
    public float throwForce;

    void Update()
    {
        // If the trigger was just pressed
        if(Input.GetAxis(triggerInputName) > 0 && !triggerHeld)
        {
            triggerHeld = true;

            SpawnFood();
        }

        // If the trigger was released
        if(Input.GetAxis(triggerInputName) == 0 && triggerHeld)
        {
            triggerHeld = false;

            LaunchFood();
        }
    }

    private void SpawnFood()
    {
        var randomFood = foodstuffs[UnityEngine.Random.Range(0, foodstuffs.Length - 1)];
        currentFood = Instantiate(randomFood, transform);

        currentFood.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void LaunchFood()
    {
        var rigidBody = currentFood.GetComponent<Rigidbody>();

        // Setting the food free!
        rigidBody.isKinematic = false;
        currentFood.transform.SetParent(null);

        rigidBody.AddForce(transform.forward * throwForce);
    }
}

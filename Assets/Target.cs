using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Game game;
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        // Spawn an explosion at the position of this target
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 3f);

        Destroy(transform.parent.gameObject, 0.2f);

        game.OnTargetHit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnCollisionEnter(null);
        }
    }
}

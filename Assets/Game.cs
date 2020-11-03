using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public BoxCollider spawnArea;
    public GameObject targetPrefab;
    private int score;
    public TMP_Text scoreText;
    public TMP_Text countdownText;
    public int secondsLeft;
    private bool gameOver;

    void Start()
    {
        UpdateUI();

        StartCoroutine(StartCountdown());

        // Spawn a new target
        SpawnTarget();
    }

    private IEnumerator StartCountdown()
    {
        while(secondsLeft > 0)
        {
            yield return new WaitForSeconds(1f);

            secondsLeft -= 1;

            UpdateUI();
        }

        gameOver = true;
    }

    private void SpawnTarget()
    {
        // Calculate a random position for the new target, somwhere inside the spawn area
        var xPosition = UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        var yPosition = UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        var zPosition = UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        // Create a new target object at that random position
        var targetObject = Instantiate(targetPrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);

        // Let the new target know about the game object
        targetObject.GetComponentInChildren<Target>().game = this;
    }

    internal void OnTargetHit()
    {
        score += 1;

        UpdateUI();

        if(!gameOver)
        {
            SpawnTarget();
        }
    }

    private void UpdateUI()
    {
        scoreText.text = $"SCORE: {score}";
        countdownText.text = $"{secondsLeft} SECONDS LEFT";
    }
}

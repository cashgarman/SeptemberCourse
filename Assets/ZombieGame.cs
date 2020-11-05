using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ZombieGame : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public Castle castle;
    public GameObject towerPrefab;
    public Transform towerPlacement;

    public int goldPerZombie;

    private int gold;
    public int startingGold;
    public TMP_Text goldText;
    public int costPerTower;
    public float delayBetweenZombieSpawns;
    private int currentWaveSize;
    public int startingWaveSize;
    private int zombiesKilledThisWave;
    public float delayBeforeWaveStarts;
    public TMP_Text waveText;
    public TMP_Text gameOverText;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;

            goldText.text = $"{gold} x ";
        }
    }

    internal void OnCastleDestroyed()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;

        // TODO: Add some UI to allow the player to try again
        // TODO: Maybe some high scoreboard stuff?
    }

    private void Start()
    {
        Gold = startingGold;
    }

    public void StartGame()
    {
        // Set the starting wave size
        currentWaveSize = startingWaveSize;

        // Spawn the first wave of zombies
        StartCoroutine(SpawnWave(currentWaveSize));
    }

    private IEnumerator SpawnWave(int numZombies)
    {
        // Delay a moment before the wave starts
        yield return new WaitForSeconds(delayBeforeWaveStarts);

        // Display which wave the player is currently on
        waveText.gameObject.SetActive(true);
        waveText.text = $"WAVE {currentWaveSize}";

        // Wait a moment for the player to see the wave message
        yield return new WaitForSeconds(3f);
        waveText.gameObject.SetActive(false);

        // Reset the number of zombies killed in the this wave
        zombiesKilledThisWave = 0;

        // Spawn all the zombies in the wave
        for (int i = 0; i < numZombies; i++)
        {
            // Spawn a zombie
            SpawnZombie();

            // Wait a bit before spawning the next zombie
            yield return new WaitForSeconds(delayBetweenZombieSpawns);
        }

        // Now wait until all the zombies have been killed
        while(true)
        {
            if(zombiesKilledThisWave == currentWaveSize)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        // Let the game know this wave of zombies has been defeated
        OnWaveDefeated();
    }

    private void OnWaveDefeated()
    {
        // Increase the size of the next wave
        currentWaveSize++;

        // Spawn ther next wave
        StartCoroutine(SpawnWave(currentWaveSize));
    }

    public void SpawnZombie()
    {
        // Spawn a zombie at the spawn point
        var zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

        // Let the zombie know that castle target it is going to walk towards
        zombie.GetComponent<ZombieController>().castle = castle;
        zombie.GetComponent<ZombieController>().game = this;
    }

    public void PlaceTower()
    {
        if(Gold >= costPerTower)
        {
            // Spawn a tower at the tower placement indicator
            var tower = Instantiate(towerPrefab, towerPlacement.position, towerPlacement.rotation);

            // Hide the tower's range indicator
            tower.GetComponent<Tower>().HideRangeIdicator();

            // Spend the gold to place the tower
            Gold -= costPerTower;
        }
    }

    internal void OnZombieKilled(ZombieController zombie)
    {
        // Award the player some gold
        Gold += goldPerZombie;

        // Increment the number of zombies killed this wave
        zombiesKilledThisWave++;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnZombie();
        }
    }
}

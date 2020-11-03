using UnityEngine;

public class ZombieGame : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public Castle castle;
    public GameObject towerPrefab;
    public Transform towerPlacement;

    public void SpawnZombie()
    {
        // Spawn a zombie at the spawn point
        var zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

        // Let the zombie know that castle target it is going to walk towards
        zombie.GetComponent<ZombieController>().castle = castle;
    }

    public void PlaceTower()
    {
        // Spawn a tower at the tower placement indicator
        var tower = Instantiate(towerPrefab, towerPlacement.position, towerPlacement.rotation);

        // Hide the tower's range indicator
        tower.GetComponent<Tower>().HideRangeIdicator();
    }
}

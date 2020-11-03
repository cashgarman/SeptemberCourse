using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    
    public Slider healthBar;
    public float startingHealth;
    public float returnDamage;

    // Castle health
    private float health;
    private float Health
    {
        get
        {
            return health;
        }
        set
        {
            // Set the new health of the castle
            health = value;

            // Update the castle's health bar (as a percentage of the starting health)
            healthBar.value = health / startingHealth;

            // TODO: Handle when the castle runs out of health, game over
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the castle's starting health
        Health = startingHealth;
    }

    // This is triggered when the castle takes damage
    internal void OnDamage(float amount, ZombieController zombie)
    {
        // Apply the damage to the castle
        Health -= amount;

        // Do some damage back to the zombie who attacked the castle
        zombie.OnDamage(returnDamage);
    }
}

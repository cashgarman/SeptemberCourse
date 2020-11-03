using UnityEngine;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public Castle castle;
    private Animator animator;
    public float movementSpeed;
    public float damageAmount;
    public Slider healthBar;
    public float startingHealth;

    // Is the zombie currently walking towards the castle?
    private bool walking;

    // Is the zombie dead? (like more than usual)
    private bool dead;

    // Zombie health
    private float health;
    private float Health
    {
        get
        {
            return health;
        }
        set
        {
            // Update the underlying health field
            health = value;

            // Update the zombie's health bar (as a percentage of the starting health)
            healthBar.value = health / startingHealth;

            // If the zombie has died
            if(health <= 0)
            {
                // Flag the zombie as dead
                dead = true;

                // Play the death animation
                animator.SetTrigger("Dead");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cache the animator component for future use
        animator = GetComponent<Animator>();

        // Play the walking forward animation
        animator.SetFloat("MoveSpeed", 1f);

        // Set the zombie as walking
        walking = true;

        // Set the starting health of the zombie (which also updates the health bar UI)
        Health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If the zombie is still walking towards the castle
        if(walking)
        {
            // Face towards the target
            transform.LookAt(castle.transform, castle.transform.up);

            // Move forwards
            transform.Translate(transform.forward * movementSpeed * Time.deltaTime, Space.World);
        }
    }

    // This is triggered when the zombie takes damage
    internal void OnDamage(float amount)
    {
        // Apply the damage to the zombie using the Health property
        Health -= amount;
    }

    // This is triggered when the zombie hits a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the trigger the zombie just entered belongs to the castle
        if(other.GetComponent<Castle>() != null)
        {
            // Stop walking
            walking = false;

            // Play the attack animation
            animator.SetTrigger("Attack");

            // Set the MoveSpeed parameter in the animation controller to zero so the zombie will default to the idle animation
            animator.SetFloat("MoveSpeed", 0f);
        }
    }

    // This is triggered by the animation event in the zombie attack animation
    public void OnAttackAnimationFinished()
    {
        // If the zombie is still alive (ish)
        if(!dead)
        {
            // Let the castle know it has been attacked by this zombie
            castle.OnDamage(damageAmount, this);

            // Play the attack animation again
            animator.SetTrigger("Attack");
        }
    }

    // This is triggered by the animation event in the zombie death animation
    public void OnDeadAnimationFinished()
    {
        // Destroy the zombie object after a few seconds to prevent them cluttering up the battlefield
        Destroy(gameObject, 3f);
    }
}

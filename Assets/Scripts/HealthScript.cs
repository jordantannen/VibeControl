using UnityEngine;

/**
 * Basic script to manage Health of an object.
 */
public class HealthScript : MonoBehaviour
{
    
    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;
    [SerializeField] private int currHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /**
     * Set game objects current health to max.
     */
    void Start()
    {
        currHealth = maxHealth;
    }

    /**
     * Deal damageAmount damage to gameObjects current health.
     */
    public void Damage(int damageAmount)
    {
        currHealth -= damageAmount;
        if (currHealth <= minHealth)
        {
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth;
    public HealthBar healthBar;
    public string enemyTag;
    public Spell spell;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currHealth);
    }
    
    void TakeDamage(int damageAmount)
    {
        currHealth -= damageAmount;
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy && enemy.fedUp)
        {
            TakeDamage(enemy.damageAmount);
        }
    }
}

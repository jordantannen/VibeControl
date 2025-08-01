using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Filter for collisions. Should be used to exclude colliding with other enemies.
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    public int damageAmount = 10;
    public int health = 10;
    public SpellType spellType;

    // Ticks per action by the enemy. Could potentially be desired to change how quickly enemy
    // does actions instead of just movement speed.
    // not sure yet
    [SerializeField] private int tickSpeed;
    [SerializeField] private int speed;
    [SerializeField] private Transform player;
    [SerializeField] private string playerTag;
    [SerializeField] private string enemyTag;
    [SerializeField] private string spellTag;
    [SerializeField] private float collisionOffset = 0.05f;
    
    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag).transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TODO determine how often/quickly enemies move and perform actions
        // this.PerformAction();
        Move();
        
    }
    
    /**
     * Move the game object based on its speed + direction.
     */
    private void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag(spellTag))
        {
            Spell spell = other.GetComponent<Spell>();
            if (spell && spell.spellType == spellType)
            {
                TakeDamage(spell.damage);
            }
        }
    }
    
    void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        // Maybe add healthbars and dont one-shot enemies
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

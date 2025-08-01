using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Filter for collisions. Should be used to exclude colliding with other enemies.
    public ContactFilter2D movementFilter;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    [SerializeField] private float collisionOffset = 0.05f;
    // Enemies spawn in one row (or column depending on how design goes) and move in it in a straight line.
    private int _row;
    
    [SerializeField] private int speed;
    public int damageAmount = 10;
    // Ticks per action by the enemy. Could potentially be desired to change how quickly enemy
    // does actions instead of just movement speed.
    // not sure yet
    [SerializeField] private int tickSpeed;

    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private string playerTag;
    [SerializeField] private string enemyTag;
    
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
        // Vector2 moveVector = this.speed * Time.fixedDeltaTime * this.direction;
        //
        // rb.MovePosition(rb.position + moveVector);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
    }

    //
    // void PerformAction()
    // {
    //     int count = rb.Cast(
    //         this.direction,
    //         this.movementFilter,
    //         this.castCollisions,
    //         this.speed * Time.fixedDeltaTime + this.collisionOffset);
    //
    //     if (count == 0)
    //     {
    //         this.Move();
    //     }
    //     else
    //     {
    //         GameObject collidedInstrument = this.castCollisions[0].collider.gameObject;
    //         this.Attack(collidedInstrument);
    //     }
    // }
}

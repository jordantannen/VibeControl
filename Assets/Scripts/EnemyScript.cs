using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Filter for collisions. Should be used to exclude colliding with other enemies.
    public ContactFilter2D movementFilter;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    [SerializeField] private float collisionOffset = 0.05f;
    // Enemies spawn in one row (or column depending on how design goes) and move in it in a straight line.
    private int _row;
    // Speed enemy moves at.
    [SerializeField] private int speed;
    // How much damage enemy does to instrument per tick it encounters.
    [SerializeField] private int damage;
    // Ticks per action by the enemy. Could potentially be desired to change how quickly enemy
    // does actions instead of just movement speed.
    // not sure yet
    [SerializeField] private int tickSpeed;
    // Enemy collision detection thing to tell when its on an instrument.
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.left;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TODO determine how often/quickly enemies move and perform actions
        this.PerformAction();
    }

    void PerformAction()
    {
        int count = rb.Cast(
            this.direction,
            this.movementFilter,
            this.castCollisions,
            this.speed * Time.fixedDeltaTime + this.collisionOffset);

        if (count == 0)
        {
            this.Move();
        }
        else
        {
            GameObject collidedInstrument = this.castCollisions[0].collider.gameObject;
            this.Attack(collidedInstrument);
        }
    }

    /**
     * Move the game object based on its speed + direction.
     */
    private void Move()
    {
        Vector2 moveVector = this.speed * Time.fixedDeltaTime * this.direction;
        
        rb.MovePosition(rb.position + moveVector);
    }

    /**
     * Attack the provided game object.
     */
    public void Attack(GameObject target)
    {
        HealthScript healthScript = target.GetComponent<HealthScript>();
        if (healthScript)
        {
            healthScript.Damage(this.damage);
        }
    }
}

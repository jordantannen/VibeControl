using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    // Filter for collisions. Should be used to exclude colliding with other enemies.
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    public int damageAmount = 10;
    public int health = 10;
    public SpellType spellType;

    public int maxPatience = 500;
    public int currPatience;
    public PatienceBar patienceBar;

    public int minRange = 3;
    public int maxRange = 5;

    // Ticks per action by the enemy. Could potentially be desired to change how quickly enemy
    // does actions instead of just movement speed.
    // not sure yet
    [SerializeField] private int tickSpeed;
    [SerializeField] private int speed;
    [SerializeField] private int fedUpSpeed = 1;
    
    [SerializeField] private Transform player;
    [SerializeField] private string playerTag;
    [SerializeField] private string enemyTag;
    [SerializeField] private string spellTag;
    [SerializeField] private float collisionOffset = 0.05f;
    [SerializeField] private TextMeshProUGUI bubbleText;
    
    private Rigidbody2D rb;
    private int stallDistance;
    public bool fedUp = false;
    
    [SerializeField] private int wanderSpeed = 1;
    private Vector2 wanderDirection;
    [SerializeField] private float wanderTimer;
    [SerializeField] private float wanderDirectionInterval = 3;
    
    private PatternManager patternManager;
    private Pattern desiredPattern;
    public float topOffset = 256f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag).transform;
        }
        // stallDistance = Random.Range(minRange, maxRange);

        currPatience = maxPatience;
        patienceBar.SetMaxPatience(maxPatience);
        patienceBar.SetPatience(currPatience);

        wanderTimer = wanderDirectionInterval;
        
        patternManager = player.GetComponentInChildren<PatternManager>();
        desiredPattern = patternManager.GetRandomPattern();

        bubbleText.text = desiredPattern.name;

        // foreach (var bar in desiredPattern.pattern)
        // {
        //     Debug.Log(desiredPattern.pattern);
        //     // foreach (var i in bar)
        //     // {
        //     //     Debug.Log(i);
        //     // }
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    
    /**
     * Move the game object based on its speed + direction.
     */
    private void Move()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (!fedUp) //&& distance < stallDistance
        {
            // rb.linearVelocity = Vector2.zero;
            Wander();
            fedUp = IsFedup(fedUpSpeed);
        }
        else
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
    }

    private void Wander()
    {
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= wanderDirectionInterval)
        {
            wanderDirection = new Vector2(Random.Range(-2f, 2f), Random.Range(-1f, 1f)).normalized;
            wanderTimer = 0;
            rb.linearVelocity = wanderDirection * wanderSpeed;
        }
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        if (screenPosition.y + topOffset > Screen.height)
        {
            rb.linearVelocity = Vector2.down * wanderSpeed;
            return;
        }
        if((screenPosition.y + topOffset > Screen.height) || (screenPosition.y < 0f) || (screenPosition.x > Screen.width) || (screenPosition.x <0f))
        {   
            screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, Screen.width);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, Screen.height);
            Vector3 newWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            transform.position = new Vector2(newWorldPosition.x, newWorldPosition.y);
            rb.linearVelocity *= -1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(spellTag))
        {
            Spell spell = other.GetComponent<Spell>();
            if (spell)
            {
                var song = spell.song;
                if (desiredPattern.SongContainsPattern(song))
                {
                    TakeDamage(spell.damage);
                }
            }
            // if (spell && spell.spellType == spellType)
            // {
            //     TakeDamage(spell.damage);
            // }
        }
        else if (other.gameObject.CompareTag(playerTag))
        {
            if (fedUp)
            {
                Destroy(gameObject);
            }
            else
            {
                rb.linearVelocity *= -1f;
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
    
    bool IsFedup(int patienceLost)
    {
        currPatience -= patienceLost;
        patienceBar.SetPatience(currPatience);
        if (currPatience <= 0)
        {
            return true;
        }
        return false;
    }
    
}

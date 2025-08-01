using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject entityToSpawn;
    public float spawnRate = 1.0f;
    public string spawnEntityTag;
    
    private float timer = 0;
    private bool hasEntity = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }
    
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        Debug.Log("Spawn Hit");
        if (!hasEntity)
        {
            Instantiate(entityToSpawn, transform.position, transform.rotation);
            hasEntity = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(spawnEntityTag))
        {
            hasEntity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.CompareTag(spawnEntityTag))
        {
            
            hasEntity = false;
        }
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject entityToSpawn;
    public float spawnRate = 1.0f;
    public string spawnEntityTag;
    public bool randomizeSpawnPosition = false;
    public Vector2 randomOffsetRange = new Vector2(1f, 1f);
    
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
        if (!hasEntity)
        {
            Vector3 spawnPos = transform.position;
            if (randomizeSpawnPosition)
            {
                float dx = Random.Range(-randomOffsetRange.x, randomOffsetRange.x);
                float dy = Random.Range(-randomOffsetRange.y, randomOffsetRange.y);
                spawnPos += new Vector3(dx, dy, 0f);
            }
            Instantiate(entityToSpawn, spawnPos, transform.rotation);
            // hasEntity = true;
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
        if (other.CompareTag(spawnEntityTag))
        {
            hasEntity = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(spawnEntityTag))
        {
            hasEntity = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(spawnEntityTag))
        {
            hasEntity = false;
        }
    }
}

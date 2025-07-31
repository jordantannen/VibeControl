using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the Tile GameObject.");
        }
        spriteRenderer.color = Color.red; 
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


public class TileMap : MonoBehaviour
{
    Tilemap tileMap;
    Camera m_camera;
    private TilemapCollider2D collider2D;
    public TileBase newTile;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        tileMap = GetComponent<Tilemap>();
        m_camera = Camera.main;
        collider2D = new TilemapCollider2D();
    }

    // Update is called once per frame
    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = m_camera.ScreenToWorldPoint(mouse.position.ReadValue());
            Vector3Int cellPos = tileMap.WorldToCell(mousePosition);
            tileMap.SetTile(cellPos, newTile);
        }
    }
}

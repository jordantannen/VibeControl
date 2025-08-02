using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DraggableObject : MonoBehaviour
{
    public Tilemap tilemap;
    public float maxSnapDist = 0.5f;
    public SnappableTiles snappableTiles;
    
    private Vector3 offset;
    private float zCoord;
    
    private TileManager tileManager;
    
    void Start()
    {
        if (tilemap == null)
        {
            tilemap = FindObjectOfType<Tilemap>();
        }
        tileManager = tilemap.GetComponent<TileManager>();
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        tileManager.RemoveInstrument(transform.position);
    }
    
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        Vector3Int cellPos = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
        TileBase currTile = tilemap.GetTile(cellPos);

        if (snappableTiles.tiles.Contains(currTile) && Vector3.Distance(transform.position, cellCenterPos) <= maxSnapDist)
        {
            transform.position = cellCenterPos;

            Debug.Log(gameObject.name + " snapped to " + cellCenterPos);
            tileManager.SetInstrument(cellCenterPos, gameObject);
            gameObject.GetComponent<InstrumentScript>().SetHasBeenPlaced(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    
}

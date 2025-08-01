using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSnapper : MonoBehaviour
{
    public Tilemap tilemap;
    public float maxSnapDist = 0.5f;
    public SnappableTiles snappableTiles;

    void Start()
    {
        if (tilemap == null)
        {
            tilemap = FindObjectOfType<Tilemap>();
        }
    }
    void Update()
    {
        Vector3Int cellPos = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
        TileBase currTile = tilemap.GetTile(cellPos);

        if (snappableTiles.tiles.Contains(currTile) && Vector3.Distance(transform.position, cellCenterPos) <= maxSnapDist)
        {
            transform.position = cellCenterPos;
        }
    }
}

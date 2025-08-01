using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public TileBase inactiveTile;
    public TileBase activeTile;
    
    private Tilemap tilemap;
    private int[] columnsIdxs;
    private int currIdx;
    private int prevIdx;

    private BoundsInt bounds;
    
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        populateColumns();
        
    }

    private void populateColumns()
    {
        HashSet<int> columns = new HashSet<int>();
        foreach (var position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                columns.Add(position.x);
            }
        }

        columnsIdxs = columns.ToArray();
        currIdx = columnsIdxs[0];
        prevIdx = columnsIdxs.Last();

    }

    private void OnEnable()
    {
        BPMController.OnBeat += HandleBeat;
    }

    private void OnDisable()
    {
        BPMController.OnBeat -= HandleBeat;
    }

    private void HandleBeat()
    {
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            Vector3Int cellPos = new Vector3Int(currIdx, y, 0);
            Vector3Int prevPos = new Vector3Int(prevIdx, y, 0);
    
            if (tilemap.GetTile(prevPos) == activeTile)
            {
                tilemap.SetTile(prevPos, inactiveTile);
            }
            if (tilemap.GetTile(cellPos) == inactiveTile)
            {
                tilemap.SetTile(cellPos, activeTile);
            }
            
        }
    
        prevIdx = currIdx;
        currIdx = (currIdx < (columnsIdxs.Length - 1) / 2) ? currIdx + 1 : columnsIdxs[0];
    }
}

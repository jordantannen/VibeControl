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
    private int[] rowIdxs;
    private int currIdx;
    private int prevIdx;

    private GameObject[,] gridArray;
    private HashSet<InstrumentTypes.InstrumentType>[] song;

    private BoundsInt bounds;
    
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        PopulateColumns();
        PopulateGrids();
    }

    private void PopulateGrids()
    {
        gridArray = new GameObject[columnsIdxs.Length, rowIdxs.Length];
        song = new HashSet<InstrumentTypes.InstrumentType>[columnsIdxs.Length];
        
        for (int i = 0; i < columnsIdxs.Length; i++)
        {
            song[i] = new HashSet<InstrumentTypes.InstrumentType>();
        }
    }
    
    private void PopulateColumns()
    {
        HashSet<int> columns = new HashSet<int>();
        HashSet<int> rows = new HashSet<int>();
        foreach (var position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                columns.Add(position.x - bounds.xMin);
                rows.Add(position.y - bounds.yMin);
            }
        }

        columnsIdxs = columns.ToArray();
        rowIdxs = rows.ToArray();
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
        song[currIdx].Clear();
        
        foreach (int y in rowIdxs)
        {
            Vector3Int cellPos = new Vector3Int(currIdx + bounds.xMin, y + bounds.yMin, 0);
            Vector3Int prevPos = new Vector3Int(prevIdx + bounds.xMin, y + bounds.yMin, 0);
            
            if (tilemap.GetTile(prevPos) == activeTile)
            {
                tilemap.SetTile(prevPos, inactiveTile);
            }
            if (tilemap.GetTile(cellPos) == inactiveTile)
            {
                tilemap.SetTile(cellPos, activeTile);
            }
            GameObject go = gridArray[currIdx, y];
            if (go)
            {
                InstrumentScript instrument = go.GetComponent<InstrumentScript>();
                if (instrument)
                {
                    song[currIdx].Add(instrument.instrumentType);
                }
            }
            
        }
        
        prevIdx = currIdx;
        if (currIdx == columnsIdxs.Length - 1)
        {
            Debug.Log("Song Played: ");
            for (int i = 0; i < song.Length; i++)
            {
                foreach (var type in song[i])
                {
                    Debug.Log(type);
                }
            }
        }
        currIdx = currIdx < (columnsIdxs.Length - 1) ? currIdx + 1 : columnsIdxs[0];
    }

    public void SetInstrument(Vector3 worldPosition, GameObject go)
    {
        Vector2Int gridPosition = GetXYPosition(worldPosition);
        if (gridArray[gridPosition.x, gridPosition.y])
        {
            Destroy(gridArray[gridPosition.x, gridPosition.y]);
        }
        gridArray[gridPosition.x, gridPosition.y] = go;
    }

    /**
     * Get world position from integer grid coordinates.
     */
    private Vector3 GetCellPosition(int x, int y)
    {
        return tilemap.CellToWorld(new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0));
    }

    /**
     * Convert world position into integer grid coordinates.
     */
    private Vector2Int GetXYPosition(Vector3 worldPosition)
    {
        Vector3Int pos = tilemap.WorldToCell(worldPosition) - bounds.min;
        return new Vector2Int(pos.x, pos.y);
    }
}

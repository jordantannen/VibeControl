using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class InstrumentScript : MonoBehaviour
{

    // Sound instrument makes.
    public AudioClip audioClip;
    public TileBase activeTile;
    public Tilemap tilemap;
    public InstrumentTypes.InstrumentType instrumentType;
    
    private int xPosition;
    private int yPosition;
    private bool hasBeenPlayed = false;
    private bool hasBeenPlaced = false;

    private SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tilemap == null)
        {
            tilemap = FindObjectOfType<Tilemap>();
        }
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentTile();
    }

    public void SetHasBeenPlaced(bool hasBeenPlaced)
    {
        this.hasBeenPlaced = hasBeenPlaced;
    }

    private void CheckCurrentTile()
    {
        
        Vector3Int currentPos = tilemap.WorldToCell(transform.position);
        TileBase currTile = tilemap.GetTile(currentPos);
        if (hasBeenPlaced && currTile == activeTile)
        {
            spriteRenderer.color = Color.mediumSpringGreen;
            if (!hasBeenPlayed)
            {
                Play();
                hasBeenPlayed = true;
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
            hasBeenPlayed = false;
        }
    }

    /**
     * Extensible method to implement instrument attack/effect.
     */
    protected abstract void Play();
}

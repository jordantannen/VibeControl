using UnityEngine;

public abstract class InstrumentScript : MonoBehaviour
{

    private int xPosition;

    private int yPosition;
    
    // Sound instrument makes.
    [SerializeField] AudioClip audioClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Extensible method to implement instrument attack/effect.
     */
    public abstract void Play();
}

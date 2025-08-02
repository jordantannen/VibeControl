using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float growthRate = 30f;
    public int damage = 10;
    public SpellType spellType;
    public float duration = 1f;

    public HashSet<InstrumentTypes.InstrumentType>[] song;
    private float timer = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            transform.localScale += new Vector3(1, 1, 1) * (growthRate * Time.deltaTime);
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

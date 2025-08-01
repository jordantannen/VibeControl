using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells", menuName = "Scriptable Objects/Spells")]
public class Spells : ScriptableObject
{
    [Serializable]
    public class SpellPrefab
    {
        public GameObject prefab;
        public SpellType spellType;
    }
    
    private Dictionary<SpellType, GameObject> spells;

    public SpellPrefab[] prefabs;

    public GameObject this[SpellType spellType]
    {
        get
        {
            Init();
            return spells[spellType];
        }
    }

    private void Init()
    {
        spells = new Dictionary<SpellType, GameObject>();
        foreach (var prefab in prefabs)
        {
            spells[prefab.spellType] = prefab.prefab;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class PatternManager : MonoBehaviour
{
    private PatternList patterns;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patterns = JsonConvert.DeserializeObject<PatternList>(Resources.Load<TextAsset>("Patterns/InstrumentPatterns").text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Check if provided pattern list exists in the pattern mapping.
     */
    public SpellType MatchesPattern(HashSet<InstrumentTypes.InstrumentType>[] pattern)
    {
        // Trim empty sets off start + end of list
        int startOffset = 0;
        int endOffset = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i].Count != 0)
            {
                startOffset = i;
                break;
            }
        }
        for (int i = pattern.Length - 1; i >= 0; i--)
        {
            if (pattern[i].Count != 0)
            {
                endOffset = i + 1;
                break;
            }
        }

        pattern = pattern[startOffset..endOffset];
        foreach (var p in patterns.patterns)
        {
            // if (p.Equals(new Pattern() { pattern = pattern }))
            // {
            //     return p.spell;
            // }
        }
        return SpellType.None;
    }

    public Pattern GetRandomPattern()
    {
        return patterns.patterns[UnityEngine.Random.Range(0, patterns.patterns.Count)];
    }
}

[Serializable]
public class PatternList
{
    public List<Pattern> patterns;
}

[Serializable]
public class Pattern
{
    public HashSet<InstrumentTypes.InstrumentType>[] pattern;
    
    public String spell;
    
    public InstrumentTypes.InstrumentType instrument;

    public bool SongContainsPattern(HashSet<InstrumentTypes.InstrumentType>[] song)
    {
        if (Equals(new Pattern() { pattern = song }))
        {
            return true;
        }

        if (pattern.Length > song.Length)
        {
            return false;
        }

        for (int i = 0; i < pattern.Length; i++)
        {
            if (!pattern[i].All(s => song[i].Contains(s)))
            {
                return false;
            }
        }

        return true;
    }
    /**
     * Override equals method to compare contents of hashsets within the pattern.
     */
    override public bool Equals(object obj)
    {
        var item = obj as Pattern;

        if (item == null)
        {
            return false;
        }
        
        if (item.pattern.Length != pattern.Length)
        {
            return false;
        }

        for (int i = 0; i < pattern.Length; i++)
        {
            if (!item.pattern[i].SetEquals(pattern[i]))
            {
                return false;
            }
        }

        return true;
    }
}
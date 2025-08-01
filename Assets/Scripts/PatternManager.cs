using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PatternManager : MonoBehaviour
{
    private PatternList patterns;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patterns = JsonConvert.DeserializeObject<PatternList>(Resources.Load<TextAsset>("Patterns/Patterns").text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Check if provided pattern list exists in the pattern mapping.
     */
    public bool MatchesPattern(HashSet<InstrumentTypes.InstrumentType>[] pattern)
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
        
        if (patterns.patterns.Contains(new Pattern() { pattern = pattern }))
        {
            return true;
        }
        return false;
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
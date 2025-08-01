using System;
using System.Collections;
using UnityEngine;

public class BPMController : MonoBehaviour
{
    public float bpm = 120f;
    public static event Action OnBeat;

    private float _beatInterval;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _beatInterval = 60.0f / bpm;
        StartCoroutine(BPM());
    }

    private IEnumerator BPM()
    {
        while (true)
        {
            yield return new WaitForSeconds(_beatInterval);
            OnBeat?.Invoke();
        }
    }
}
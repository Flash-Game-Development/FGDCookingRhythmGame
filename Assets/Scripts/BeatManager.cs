using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public static BeatManager instance {get; private set;}
    public double bpm = 120;
    public event Action Whole, Half, Quarter, Eigth;

    private double nextWhole, nextHalf, nextQuarter, nextEigth;
    private double wholeInterval, halfInterval, quarterInterval, eigthInterval;

    private void Awake()
    {

        if(instance != null){
            Debug.LogError("Found more than one Beat Manager in the scene");
        }
        instance = this;

        wholeInterval = 60.0f / (bpm/4);
        nextWhole = AudioSettings.dspTime + wholeInterval;
        halfInterval = 60.0f / (bpm/2);
        nextHalf = AudioSettings.dspTime + halfInterval;
        quarterInterval = 60.0f / (bpm);
        nextQuarter = AudioSettings.dspTime + quarterInterval;
        eigthInterval = 60.0f / (bpm*2);
        nextEigth = AudioSettings.dspTime + eigthInterval;
    }

    private void Update()
    {
        double dsp = AudioSettings.dspTime;

        // Only fire beats when DSP clock reaches that time
        if (dsp >= nextWhole)
        {
            Whole?.Invoke();

            // Schedule *next* beat
            nextWhole += wholeInterval;
        }
        if (dsp >= nextHalf)
        {
            Half?.Invoke();

            nextHalf += halfInterval;
        }
        if (dsp >= nextQuarter)
        {
            Quarter?.Invoke();

            nextQuarter += quarterInterval;
        }
        if (dsp >= nextEigth)
        {
            Eigth?.Invoke();

            nextEigth += eigthInterval;
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Beat : MonoBehaviour
{
    public enum Timing { Measure = 16, Half = 8, Quarter = 4, Eighth = 2, Sixteenth };
    public double bpm = 120.0;

    private int tickCounter;
    private double nextTick = 0.0;

    void Start()
    {
        tickCounter = 16;
        double time = AudioSettings.dspTime;
        nextTick = time + (60.0f / bpm) * 4.0f; // pre-roll 4 beats
    }

    void Update()
    {
        double time = AudioSettings.dspTime;
        if (time > nextTick)
        {
            nextTick = time + ((60.0 / bpm) / 4); // sixteenths
            if (++tickCounter > 16)
                tickCounter = 1;
        }
    }

    public void Sync(System.Action callback, Beat.Timing timing = Beat.Timing.Measure)
    {
        StartCoroutine(YieldForSync(callback, timing));
    }

    IEnumerator YieldForSync(System.Action callback, Beat.Timing timing)
    {
        int startCount = tickCounter;
        bool isStartTick = true;
        bool waiting = true;
        while (waiting)
        {
            isStartTick = (isStartTick && startCount == tickCounter);
            if (isStartTick)
                yield return false;
            isStartTick = false;
            if (timing == Timing.Sixteenth || tickCounter % (int)timing == 1)
                waiting = false;
            else
                yield return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour {

    public int AverageFPS { get; private set; }
    public int HighestFPS { get; private set; }
    public int LowestFPS { get; private set; }
    public int fpsRange;
    int[] fpsBuffer;
    int fpsBufferIndex;

    private void Update()
    {
        if (fpsBuffer == null || fpsBuffer.Length != fpsRange)
        {
            InitializeFPSBuffer();
        }
        UpdateFPSBuffer();
        CalculateFPS();

    }
    void InitializeFPSBuffer()
    {
        if (fpsRange <= 0)
            fpsRange = 1;

        fpsBuffer = new int[fpsRange];
        fpsBufferIndex = 0;
    }
    void UpdateFPSBuffer()
    {
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (fpsBufferIndex >= fpsRange)
            fpsBufferIndex = 0;

    }
    void CalculateFPS()
    {
        int sum = 0;
        int highest = 0;
        int lowest = int.MaxValue;
        for (int i = 0; i < fpsBuffer.Length; i++)
        {
            int fps = fpsBuffer[i];
            sum += fps;
            if (fps >= highest)
                highest = fps;
            if (fps <= lowest)
                lowest = fps;
        }
        AverageFPS = sum / fpsRange;
        HighestFPS = highest;
        LowestFPS = lowest;
    }
}

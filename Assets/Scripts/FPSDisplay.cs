using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour { 

    public Text highestFPSLabel,
                averageFPSLabel,
                lowestFPSLabel,
                timeScaleLabel;
    FPSCounter counter;

    [System.Serializable] private struct FPSColor
    {
        public Color color;
        public int minimumFps;
    }

    [SerializeField] FPSColor[] coloring;

    float[] timeScalers = { 1, 0.5f, 0.2f, 0.1f, 0.05f, 0.01f };
    float[] timeSteppings = { 0.02f, 0.01f, 0.005f, 0.001f, 0.001f, 0.0001f };
    int scaleIndex;

    private void Awake()
    {
        counter = GetComponent<FPSCounter>();
        scaleIndex = 1;
    }
    private void Update()
    {
        DisplayLabel(averageFPSLabel, counter.AverageFPS, "A");
        DisplayLabel(highestFPSLabel, counter.HighestFPS, "H");
        DisplayLabel(lowestFPSLabel, counter.LowestFPS, "L");
        ChangeTimeScale();

        if (Input.GetButtonUp("Cancel"))
        {
            Application.Quit();
        }
        
    }
    void DisplayLabel(Text label, int fps, string name)
    {
        label.text = name + ": " + Mathf.Clamp(fps, 0, 999);
        for (int i = 0; i < coloring.Length; i++)
        {
            if (fps >= coloring[i].minimumFps)
            {
                label.color = coloring[i].color;
                break;
            }
        }
    }

    void ChangeTimeScale()
    {
        
        if (Input.GetButtonDown("Switch"))
        {
            Debug.Log(Time.fixedDeltaTime);
            if (scaleIndex>= timeScalers.Length)
                    scaleIndex = 0;
            Time.timeScale = timeScalers[scaleIndex];
            Time.fixedDeltaTime = timeSteppings[scaleIndex];
            timeScaleLabel.text = "TS: " + Time.timeScale;
            timeScaleLabel.color = coloring[scaleIndex].color;
            scaleIndex++;
        }
    }
}

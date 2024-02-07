using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DebugType
{
    FrameRate,
    GPU,
    Memory,
    CPU,
}
public class S_DebugInfos : MonoBehaviour
{
    [SerializeField] DebugType type;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI dataText;

    private float deltaTime = 0f;

    private void Start()
    {
        string textToDisplay ="";

        switch(type)
        {
            case DebugType.FrameRate:
                textToDisplay = "Frame Rate (FPS)";
                break;
            case DebugType.GPU:
                textToDisplay = "GPU Used (%)";
                break;
            case DebugType.Memory:
                textToDisplay = "Memory Used (%)";
                break;
            case DebugType.CPU:
                textToDisplay = "CPU Used (%)";
                break;
        }
        titleText.text = textToDisplay;


    }

    private void Update()
    {
        string textToDisplay = "";
        switch (type) 
        {
            case DebugType.FrameRate:
                textToDisplay = GetFrameRate();
                break;
            case DebugType.GPU:
                break;
            case DebugType.Memory:
                break;
            case DebugType.CPU:
                break;

        }
        dataText.text = textToDisplay;
    }

    string GetFrameRate()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        return ((int)(1f / deltaTime)).ToString();
    }

    string GetGPUUse()
    {
        return "";
    }

    string GetMemoryUsed()
    {
        long totalMemory = System.GC.GetTotalMemory(false);
        long usedMemory = totalMemory - System.GC.GetTotalMemory(true);

        float usedMemoryPercentage = (float)usedMemory / totalMemory * 100f;
        return usedMemoryPercentage.ToString();
    }

    string GetCPUUsed()
    {
        return "";
    }


}

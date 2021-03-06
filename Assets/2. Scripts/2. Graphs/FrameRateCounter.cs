﻿using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display = default;

    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1f;

    [SerializeField]
    public DisplayMode mode = DisplayMode.FPS;

    public enum DisplayMode { FPS, MS }

    int frames;
    float duration;
    float bestDuration = float.MaxValue;
    float worstDuration = 0f;

    void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;

        if (frameDuration < bestDuration)
            bestDuration = frameDuration;

        if (frameDuration > worstDuration)
            worstDuration = frameDuration;


        if (duration > sampleDuration)
        {
            display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}",
                    1f / bestDuration,
                    frames / duration,
                    1f / worstDuration
                );
            frames = 0;
            duration = 0f;
            bestDuration = float.MaxValue;
            worstDuration = 0f;
        }
    }
}
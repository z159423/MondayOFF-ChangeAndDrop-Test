using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    float DeltaTime = 0.0f;

    void Update()
    {
        if (Time.timeScale > 0)
            DeltaTime += (Time.deltaTime - DeltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float msec = DeltaTime * 1000.0f;
        float fps = 1.0f / DeltaTime;
        string text = string.Format("{0:0.0}ms, FPS: {1:0.}", msec, fps);

        textMeshProUGUI.text = text;
    }
}

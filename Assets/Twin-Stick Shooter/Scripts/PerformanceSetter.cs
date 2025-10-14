using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        QualitySettings.vSyncCount = 0;
        
        RefreshRate rate = new RefreshRate() { numerator = 60, denominator = 1 };

        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow, rate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

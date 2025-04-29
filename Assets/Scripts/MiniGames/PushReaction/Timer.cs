using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float currentTime { get; private set; }
    public bool isRunning { get; private set; }

    public void StartTimer()
    {
        currentTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("F3") + "s";
    }
}

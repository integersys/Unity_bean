using UnityEngine;
using TMPro;

public class Taimeris : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public bool IsRunning { get; private set; }

    float elapsed;

    void Update()
    {
        if (!IsRunning) return;

        elapsed += Time.deltaTime;

        if (timerText != null)
            timerText.text = FormatTime(elapsed);
    }

    public void StartTimer()
    {
        elapsed = 0f;
        IsRunning = true;

        if (timerText != null)
            timerText.text = FormatTime(elapsed);
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public float GetSeconds() => elapsed;

    string FormatTime(float seconds)
    {
        int total = Mathf.FloorToInt(seconds);
        int min = total / 60;
        int sec = total % 60;
        return $"{min:00}:{sec:00}";
    }
}

using UnityEngine;

public class PlayTimer : MonoBehaviour
{
    [SerializeField] private float playTime = 0f;
    public bool isTimerRunning { get; private set; } = false;

    public void StartPlayTimer()
    {
        playTime = 0f;
        isTimerRunning = true;
    }
    void Update()
    {
        if (isTimerRunning)
        {
            playTime += Time.deltaTime;
        }
    }
    public float GetPlayTime()
    {
        return playTime;
    }
    public void StopPlayTimer()
    {
        isTimerRunning = false;
    }
    public void ResetPlayTimer()
    {
        playTime = 0f;
        StopPlayTimer();
    }
}

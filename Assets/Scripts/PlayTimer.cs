using UnityEngine;

public class PlayTimer : MonoBehaviour
{
    [SerializeField] private float playTime = 0f;
    public bool isTimerRunning { get; private set; } = false;

    public void StartPlayTimer(GameEvents.OpenFirstCell openFirstCellEvent)
    {
        playTime = 0f;
        isTimerRunning = true;
    }
    public void StartPlayTimerWithoutResetTime()
    {
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
    public void StopPlayTimer(GameEvents.Lost gameLost)
    {
        isTimerRunning = false;
    }
    public void StopPlayTimer(GameEvents.Win gameWin)
    {
        isTimerRunning = false;
    }
    public void ResetPlayTimer(GameEvents.Restart restartGame)
    {
        playTime = 0f;
        isTimerRunning = false;
    }
}

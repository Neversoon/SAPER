using UnityEngine;

public class PlayTimerController : MonoBehaviour
{
    [SerializeField] PlayTimer playTimer;
    [SerializeField] PlayTimeUI playTimeUI;

    public void Awake()
    {
        EventBus.Subscribe<GameEvents.OpenFirstCell>(playTimer.StartPlayTimer);
        EventBus.Subscribe<GameEvents.Lost>(playTimer.StopPlayTimer);
        EventBus.Subscribe<GameEvents.Win>(playTimer.StopPlayTimer);
        EventBus.Subscribe<GameEvents.Restart>(playTimer.ResetPlayTimer);
    }
    public void Update()
    {
        if (playTimer.isTimerRunning)
        {
            playTimeUI.UpdateTimeText(playTimer.GetPlayTime());
        }
    }
    public void OnDestroy()
    {
        EventBus.Unsubscribe<GameEvents.OpenFirstCell>(playTimer.StartPlayTimer);
        EventBus.Unsubscribe<GameEvents.Lost>(playTimer.StopPlayTimer);
        EventBus.Unsubscribe<GameEvents.Win>(playTimer.StopPlayTimer);
        EventBus.Unsubscribe<GameEvents.Restart>(playTimer.ResetPlayTimer);
    }
}

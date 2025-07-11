using UnityEngine;

public class PlayTimerController : MonoBehaviour
{
    [SerializeField] PlayTimer playTimer;
    [SerializeField] PlayTimeUI playTimeUI;

    public void Awake()
    {
        EventBus.Subscribe<GameEvents.Started>(playTimeUI.ResetTimeText);
        EventBus.Subscribe<GameEvents.Restart>(playTimeUI.ResetTimeText);

        EventBus.Subscribe<GameEvents.Started>(playTimer.ResetPlayTimer);
        EventBus.Subscribe<GameEvents.FirstInteraction>(playTimer.StartPlayTimer);
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
        EventBus.Unsubscribe<GameEvents.Started>(playTimeUI.ResetTimeText);
        EventBus.Unsubscribe<GameEvents.Restart>(playTimeUI.ResetTimeText);

        EventBus.Unsubscribe<GameEvents.Started>(playTimer.ResetPlayTimer);
        EventBus.Unsubscribe<GameEvents.FirstInteraction>(playTimer.StartPlayTimer);
        EventBus.Unsubscribe<GameEvents.Lost>(playTimer.StopPlayTimer);
        EventBus.Unsubscribe<GameEvents.Win>(playTimer.StopPlayTimer);
        EventBus.Unsubscribe<GameEvents.Restart>(playTimer.ResetPlayTimer);
    }
}

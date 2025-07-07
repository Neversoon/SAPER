using UnityEngine;

public class PlayTimerController : MonoBehaviour
{
    [SerializeField] PlayTimer playTimer;
    [SerializeField] PlayTimeUI playTimeUI;

    public void Awake()
    {
        GameEvents.Instance.userOpenFirstCell += playTimeUI.ResetTimeText;
        GameEvents.Instance.userOpenFirstCell += playTimer.StartPlayTimer;

        GameEvents.Instance.lostGame += playTimer.StopPlayTimer;
        GameEvents.Instance.winGame += playTimer.StopPlayTimer;

        GameEvents.Instance.restartGame += playTimer.ResetPlayTimer;
        
        GameEvents.Instance.restartGame += playTimeUI.ResetTimeText;
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
        GameEvents.Instance.userOpenFirstCell -= playTimeUI.ResetTimeText;
        GameEvents.Instance.userOpenFirstCell -= playTimer.StartPlayTimer;

        GameEvents.Instance.lostGame -= playTimer.StopPlayTimer;
        GameEvents.Instance.winGame -= playTimer.StopPlayTimer;

        GameEvents.Instance.restartGame -= playTimer.ResetPlayTimer;

        GameEvents.Instance.restartGame -= playTimeUI.ResetTimeText;
    }
}

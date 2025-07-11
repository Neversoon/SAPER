using UnityEngine;

public class PlayTimeUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI timeText;

    public void Start()
    {
        ResetTimeText();
    }

    public void UpdateTimeText(float playTime)
    {
        int seconds = Mathf.FloorToInt(playTime);
        timeText.text = seconds.ToString("D3");
    }

    public void ResetTimeText()
    {
        timeText.text = "000";
    }
    public void ResetTimeText(GameEvents.Restart restartEvent)
    {
        timeText.text = "000";
    }
    public void ResetTimeText(GameEvents.Started startedEvent)
    {
        timeText.text = "000";
    }
}


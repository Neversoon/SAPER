using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    [SerializeField] GameObject eventUIContainer;
    [SerializeField] TextMeshProUGUI eventText;
    [SerializeField] Image targetImage;
    [SerializeField] Sprite winSprite;
    [SerializeField] Sprite loseSprite;

    void Awake()
    {
        EventBus.Subscribe<GameEvents.Win>(ShowWinUI);
        EventBus.Subscribe<GameEvents.Started>(HideUI);
        EventBus.Subscribe<GameEvents.Lost>(ShowLoseUI);

        eventUIContainer.SetActive(false);
    }

    void ShowWinUI(GameEvents.Win winEvent)
    {
        eventText.text = $"You win!";
        eventUIContainer.SetActive(true);
        targetImage.sprite = winSprite;
    }
    void ShowLoseUI(GameEvents.Lost lostEvent)
    {
        eventText.text = $"You Lose!";
        eventUIContainer.SetActive(true);
        targetImage.sprite = loseSprite;
    }
    void HideUI(GameEvents.Started startedEvent)
    {
        eventUIContainer.SetActive(false);
    }
}

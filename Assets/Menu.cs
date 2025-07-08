using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject bottomPanelUI;

    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        bottomPanelUI.SetActive(false);
        EventBus.Publish(new GameEvents.Pause());
    }

    public void ClosePauseMenu()
    {
        pauseMenuUI.SetActive(false);
        bottomPanelUI.SetActive(true);
        EventBus.Publish(new GameEvents.Resume());
    }
}

using UnityEngine;

public class FlagCounter : MonoBehaviour
{
    [SerializeField] FlagCountUI flagCountUI;
    delegate void FlagCountChanged(int flagCount);

    void Awake()
    {
        EventBus.Subscribe<GameEvents.FlagCountChanged>(UpdateFlagCount);
    }
    private void UpdateFlagCount(GameEvents.FlagCountChanged flagCountEvent)
    {
        flagCountUI.UpdateFlagCountText(flagCountEvent.NewCount);
    }
    void OnDestroy()
    {
        EventBus.Unsubscribe<GameEvents.FlagCountChanged>(UpdateFlagCount);
    }
}

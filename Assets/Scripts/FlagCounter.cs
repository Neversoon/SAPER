using UnityEngine;

public class FlagCounter : MonoBehaviour
{
    [SerializeField] FlagCountUI flagCountUI;

    private void OnEnable()
    {
        GameEvents.Instance.userFlagCountChanged += UpdateFlagCount;
    }

    private void OnDisable()
    {
        GameEvents.Instance.userFlagCountChanged -= UpdateFlagCount;
    }

    private void UpdateFlagCount(int flagCount)
    {
        flagCountUI.UpdateFlagCountText(flagCount);
    }
}

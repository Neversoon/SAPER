using UnityEngine;

public class FlagCountUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI flagCountText;

    public void UpdateFlagCountText(int flagCount)
    {
        flagCountText.text = flagCount.ToString("D3");
    }

    public void ResetFlagCountText()
    {
        flagCountText.text = "000";
    }
}

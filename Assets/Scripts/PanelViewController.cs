using System.Collections;
using UnityEngine;

public class PanelViewController : MonoBehaviour
{
    [SerializeField] RectTransform panel;
    [SerializeField] RectTransform containerRoot;
    [SerializeField] float openTime = 0.5f;
    [SerializeField] float closeTime = 0.5f;
    bool isPanelOpen = true;

    public void TogglePanel()
    {
        StartCoroutine(ShowPanel());
    }

    public IEnumerator ShowPanel()
    {
        float height = panel.rect.height;

        Vector3 startPosition = containerRoot.anchoredPosition;
        float time = 0.0f;

        if (isPanelOpen)
        {
            while (time < openTime)
            {
                time += Time.deltaTime;
                containerRoot.anchoredPosition = Vector2.Lerp(startPosition, new Vector2(startPosition.x, -height), time / openTime);
                yield return null;
            }
            containerRoot.anchoredPosition = Vector2.Lerp(startPosition, new Vector2(startPosition.x, -height), 1.0f);
            isPanelOpen = false;
        }
        else
        {
            while (time < closeTime)
            {
                time += Time.deltaTime;
                containerRoot.anchoredPosition = Vector2.Lerp(startPosition, new Vector2(startPosition.x, startPosition.y + height), time / closeTime);
                yield return null;
            }
            containerRoot.anchoredPosition = Vector2.Lerp(startPosition, Vector2.zero, 1.0f);
            isPanelOpen = true;
        }
        yield return null;
    }

}

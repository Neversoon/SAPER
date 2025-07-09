using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    [SerializeField] Image currentImage;
    [SerializeField] Sprite imageUP;
    [SerializeField] Sprite imageDown;
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
        SFXAudioPlayer.Instance.PlaySFX(SFXAudioPlayer.Instance.audioClips.uiTap);
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

            currentImage.sprite = imageUP;
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

            currentImage.sprite = imageDown;
            isPanelOpen = true;
        }
        yield return null;
    }

}

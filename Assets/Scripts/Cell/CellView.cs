using System;
using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite emptyCellSprite;
    [SerializeField] GameObject bombObject;
    [SerializeField] GameObject flagObject;
    [SerializeField] TextMeshProUGUI bombCount;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetEmptyCell()
    {
        spriteRenderer.sprite = Instantiate(emptyCellSprite);
    }
    public void SetRedCell()
    {
        spriteRenderer.color = Color.red;
    }
    public void SetBomb()
    {
        SetEmptyCell();
        bombObject.gameObject.SetActive(true);
    }
    public void Disableflag()
    {
        bombObject.gameObject.SetActive(false);
    }

    public void ChangeFlagView(bool setFlag)
    {
        flagObject.gameObject.SetActive(setFlag);
    }

    public void ChangeBombCountText(int count)
    {
        if (count == 0)
        {
            bombCount.gameObject.SetActive(false);

            bombCount.text = "";
            return;
        }

        bombCount.gameObject.SetActive(true);

        bombCount.text = $"{count}";
    }

    internal void SetText(Vector2Int cellIndex)
    {
        bombCount.gameObject.SetActive(true);

        bombCount.text = $"{cellIndex.y} {cellIndex.x}";
    }
}

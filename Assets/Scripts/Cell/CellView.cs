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
        flagObject.gameObject.SetActive(false);
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
        ChangeColorTextByCount(count);
        
        bombCount.gameObject.SetActive(true);

        bombCount.text = $"{count}";
    }
    void ChangeColorTextByCount(int count)
    {
        switch (count)
        {
            case 1:
                bombCount.color = Color.blue;
                break;
            case 2:
                bombCount.color = Color.green;
                break;
            case 3:
                bombCount.color = Color.red;
                break;
            case 4:
                bombCount.color = new Color(0.5f, 0, 130f / 255f);
                break;
            case 5:
                bombCount.color = new Color(129f / 255f, 1f / 255f, 2f / 255f);
                break;
            case 6:
                bombCount.color = new Color(0.0f, 128f / 255f, 129 / 255f);
                break;
            case 7: 
                bombCount.color = Color.black;
                break;
            case 8:
                bombCount.color = new Color(128f / 255f, 128f / 255f, 128f / 255f);
                break;
            default:
                bombCount.color = Color.black;
                break;
        }
    }
}

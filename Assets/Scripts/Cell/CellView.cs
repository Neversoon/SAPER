using UnityEngine;

public class CellView : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite emptyCellSprite;
    [SerializeField] GameObject bombObject;
    [SerializeField] GameObject flagObject;
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
    public void Setflag()
    {
        flagObject.gameObject.SetActive(true);
    }
    public void Disableflag()
    {
        bombObject.gameObject.SetActive(false);
    }
}

using UnityEngine.EventSystems;

public class BombCell : Cell
{
    void Awake()
    {
        cellView = GetComponent<CellView>();
        cellData.id = 1;
        cellView.SetBomb();
    }

    public override void Tap()
    {
    }

}
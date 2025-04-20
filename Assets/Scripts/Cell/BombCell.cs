using UnityEngine.EventSystems;

public class BombCell : ClosedCell
{
    public BombCell(CellView cellView, ICellStateChanger cellStateChanger) : base(cellView, cellStateChanger)
    {
    }

    public override void Tap()
    {
        if (setFlag)
        {
            return;
        }
    }
}
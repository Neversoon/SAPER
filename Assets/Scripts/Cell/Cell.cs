using UnityEngine;

public abstract class Cell
{
    CellView cellView;
    protected bool setFlag = false;
    ICellStateChanger cellStateChanger;

    public Cell(CellView cellView, ICellStateChanger cellStateChanger)
    {
        this.cellView = cellView;
        this.cellStateChanger = cellStateChanger;
    }
    public abstract void Tap();
    public virtual void SetFlag()
    {
        setFlag = !setFlag;
    }
}

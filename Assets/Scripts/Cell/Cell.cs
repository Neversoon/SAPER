using UnityEngine;

public abstract class Cell
{
    public CellView cellView { get; private set; }
    public bool setFlag { get; private set; } = false;
    protected ICellStateChanger cellStateChanger;
    public CellData cellData { get; private set; } = new CellData();

    public Cell(CellView cellView, ICellStateChanger cellStateChanger)
    {
        this.cellView = cellView;
        this.cellStateChanger = cellStateChanger;
    }
    public abstract void Tap();
    public virtual void SetFlag()
    {
        setFlag = !setFlag;
        cellView.ChangeFlagView(setFlag);
    }
    public virtual void SetFlag(bool flag)
    {
        setFlag = flag;
        cellView.ChangeFlagView(setFlag);
    }
}

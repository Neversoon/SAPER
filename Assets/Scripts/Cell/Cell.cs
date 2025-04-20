using UnityEngine;

public abstract class Cell
{
    public CellView cellView { get; private set; } 
    protected bool setFlag = false;
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
}

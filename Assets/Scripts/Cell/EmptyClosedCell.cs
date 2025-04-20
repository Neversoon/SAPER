public class EmptyClosedCell : ClosedCell
{
    public EmptyClosedCell(CellView cellView, ICellStateChanger cellStateChanger) : base(cellView, cellStateChanger)
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
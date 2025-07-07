public class NumericCell : Cell
{
    public NumericCell(CellView cellView, ICellStateChanger cellStateChanger) : base(cellView, cellStateChanger)
    {
        cellData.id = 3;
    }
    public override void SetFlag()
    {
    }
    public override void Tap()
    {

    }
}
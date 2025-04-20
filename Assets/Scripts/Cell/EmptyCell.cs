
public class EmptyCell : Cell
{
    public EmptyCell(CellView cellView, ICellStateChanger cellStateChanger) : base(cellView, cellStateChanger)
    {
        cellData.id = 0;
    }

    public override void Tap()
    {
        
    }
}

public class EmptyCell : Cell
{
    void Awake()
    {
        cellView = GetComponent<CellView>();
        cellData.id = 0;
        cellView.Setflag();
    }
    public override void Tap()
    {
    }
}
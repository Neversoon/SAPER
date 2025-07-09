public class EmptyClosedCell : ClosedCell
{
    public EmptyClosedCell(CellView cellView, ICellStateChanger cellStateChanger) : base(cellView, cellStateChanger)
    {
        cellData.id = 1;
    }

    public override void Tap()
    {
        if (setFlag)
        {
            return;
        }

        SFXAudioPlayer.Instance.PlaySFX(SFXAudioPlayer.Instance.audioClips.openCell);
        cellView.SetEmptyCell();
        cellStateChanger.ChangeState<EmptyCell>();
    }
    public override void SetFlag()
    {
        base.SetFlag();
    }
}
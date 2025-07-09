public class BombCell : ClosedCell
{
    public BombCell(CellView cellView, ICellStateChanger cellStateChanger) : base(cellView, cellStateChanger)
    {
        cellData.id = 2;
    }

    public override void Tap()
    {
        if (setFlag)
        {
            return;
        }
        
        cellView.SetEmptyCell();
        cellView.SetBomb();
        cellView.SetRedCell();
        
        SFXAudioPlayer.Instance.PlaySFX(SFXAudioPlayer.Instance.audioClips.mineExplosion);
        EventBus.Publish(new GameEvents.Lost());
    }
    public override void SetFlag()
    {
        base.SetFlag();
    }
}
public interface ICellStateChanger
{
    void ChangeState<T>() where T : Cell;
}

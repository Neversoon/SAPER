public static class GameEvents
{
    public struct Lost { }
    public struct Started { }
    public struct OpenFirstCell { }
    public struct Win { }
    public struct Restart { }
    public struct Pause { }
    public struct Resume { }
    public struct FlagCountChanged
    {
        public int NewCount;
        public FlagCountChanged(int newCount) => NewCount = newCount;
    }

}
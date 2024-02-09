public interface IPlayerTargetSearcher
{
    bool IsTargetFounded { get; }
    ITargetable FoundedTarget { get; }
}
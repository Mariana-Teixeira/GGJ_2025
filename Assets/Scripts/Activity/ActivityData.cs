public enum Loser {Player1, Player2, Both }
public struct ActivityData
{
    public readonly Loser Target;
    public ActivityData(Loser target)
    {
        Target = target;
    }
}
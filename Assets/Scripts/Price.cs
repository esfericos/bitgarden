public class Price
{
    public int Gold { get; }
    // public int Iron { get; }
    // public int Coal { get; }
    // public int Steel { get; }

    public Price(int gold = 0)
    {
        Gold = gold;
    }

    public override string ToString()
    {
        return $"Gold: {Gold}";
    }
}

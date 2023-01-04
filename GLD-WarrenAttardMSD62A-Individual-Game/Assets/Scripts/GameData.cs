public static class GameData
{
    private static int _money;

    public static int Money
    {
        set { _money = value; }
        get { return _money; }
    }

    private static bool _NPCFound;

    public static bool NPCFound
    {
        set { _NPCFound = value; }
        get { return _NPCFound; }
    }

    private static int _passageToCityCost;

    public static int PassageToCityCost
    {
        set { _passageToCityCost = value; }
        get { return _passageToCityCost; }
    }

    private static bool _accessToCity;

    public static bool AccessToCity
    {
        set { _accessToCity = value; }
        get { return _accessToCity; }
    }
}

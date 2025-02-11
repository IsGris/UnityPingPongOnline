using Unity.Netcode;

public static class GameConstants
{
    public static float TimeBetweenTicks { get; private set; }
    
    static GameConstants()
    {
        TimeBetweenTicks = 1f / NetworkManager.Singleton.NetworkTickSystem.TickRate;
    }
}

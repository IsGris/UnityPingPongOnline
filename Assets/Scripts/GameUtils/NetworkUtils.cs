using Unity.Netcode;

public enum NetworkState
{
    NotConnected,
    Client,
    Host,
    Server
}

public static class NetworkUtils
{
    // Checks does NetworkManager started Host || Server || Client
    public static bool IsRunningNetwork() => 
        (NetworkManager.Singleton?.IsClient == true || NetworkManager.Singleton?.IsServer == true || NetworkManager.Singleton?.IsHost == true) && 
        NetworkManager.Singleton?.ShutdownInProgress == false;
    // Get current network state(Client, Host...)
    public static NetworkState GetCurrentNetworkState()
    {
        if (NetworkManager.Singleton?.ShutdownInProgress == true) return NetworkState.NotConnected;
        if (NetworkManager.Singleton?.IsHost == true) return NetworkState.Host;
        if (NetworkManager.Singleton?.IsServer == true) return NetworkState.Server;
        if (NetworkManager.Singleton?.IsClient == true) return NetworkState.Client;
        return NetworkState.NotConnected;
    }
}

using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class DebugCheats : MonoBehaviour
{
    [SerializeField] protected GameObject DebugCanvas;
    // Text, that displays current network state(Host, Server, Client)
    [SerializeField] protected Text CurrentNetworkStateText;

    private void Start()
    {
        if (DebugCanvas == null) { Debug.LogErrorFormat("{0} in {1} is not defined", nameof(DebugCanvas), nameof(DebugCheats)); return; }
#if UNITY_EDITOR
        DebugCanvas.SetActive(true);
#else
        DebugCanvas.SetActive(false);
#endif
    }

#if UNITY_EDITOR
    /// <summary>
    /// Toggle NetworkManager current host enabled state
    /// </summary>
    public void ToggleHost()
    {
        if (NetworkManager.Singleton == null) return;

        if (NetworkUtils.IsRunningNetwork()) NetworkManager.Singleton.Shutdown();
        else NetworkManager.Singleton.StartHost();

        UpdateNetworkStateText();
    }

    /// <summary>
    /// Toggle NetworkManager current server enabled state
    /// </summary>
    public void ToggleServer()
    {
        if (NetworkManager.Singleton == null) return;

        if (NetworkUtils.IsRunningNetwork()) NetworkManager.Singleton.Shutdown();
        else NetworkManager.Singleton.StartServer();

        UpdateNetworkStateText();
    }

    /// <summary>
    /// Toggle NetworkManager current client enabled state
    /// </summary>
    public void ToggleClient()
    {
        if (NetworkManager.Singleton == null) return;

        if (NetworkUtils.IsRunningNetwork()) NetworkManager.Singleton.Shutdown();
        else NetworkManager.Singleton.StartClient();

        UpdateNetworkStateText();
    }

    protected void UpdateNetworkStateText()
    {
        if (CurrentNetworkStateText == null) return;
        
        CurrentNetworkStateText.text = "Connect state: " + NetworkUtils.GetCurrentNetworkState().ToString();
    }
#endif
}

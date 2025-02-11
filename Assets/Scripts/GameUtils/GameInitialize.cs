using UnityEngine;

public class GameInitialize : MonoBehaviour
{
    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }
}

using UnityEngine;

public class ReloaderGame : MonoBehaviour
{
    [SerializeField] private BootStrap bootStrap;

    public void ReloadGame()
    {
        bootStrap.EndGame();
        bootStrap.StartGame();
    }
}

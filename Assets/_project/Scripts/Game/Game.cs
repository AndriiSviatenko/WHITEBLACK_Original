using _Project.Scripts._Infrastructure_.Patterns.Observers;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private WinController winController;
    [SerializeField] private LoseController loseController;
    [SerializeField] private ReloaderGame reloaderGame;

    [Space(10f)]
    [Header("Observers")]
    [SerializeField] private Observer characterDead;
    [SerializeField] private Observer characterDetectFinishPoint;
    
    public void Init()
    {
        characterDetectFinishPoint.Subscribe(Win);
        characterDead.Subscribe(Lose);
    }
    public void Off()
    {
        characterDetectFinishPoint.Unsubscribe(Win);
        characterDead.Unsubscribe(Lose);
    }

    public void Win()
    {
        winController.Win();
        ReloadGame();
    }

    public void Lose()
    {
        loseController.Lose();
        ReloadGame();
    }

    private void ReloadGame() => 
        reloaderGame.ReloadGame();
}
using _Project.Scripts._Infrastructure_.Patterns.Observers;
using _Project.Scripts.Services.Boundary;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private Game game;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private BaseBoundary boundary;

    [Space(10f)]
    [Header("Character Settings")]
    [SerializeField] private Character prefab;
    [SerializeField] private Transform startPoint;

    [Space(10f)]
    [Header("Observers")]
    [SerializeField] private Observer characterDead;
    [SerializeField] private Observer characterDetectFinishPoint;

    private CharacterFactory _characterFactory = new();
    private Character _instance;

    private void Awake() => 
        StartGame();

    public void StartGame()
    {
        _instance = CreateCharacter();
        SubscibeCharacterObservers(_instance);
        boundary.AddTarget(_instance.transform);

        _instance.SetInput(new GameInput());
        _instance.Play();

        enemySpawner.StartSpawn();

        game.Init();
    }
    public void EndGame()
    {
        UnSubscibeCharacterObservers(_instance);
        enemySpawner.Cleanup();
        boundary.RemoveTarget(_instance.transform);
        Destroy(_instance.gameObject);
        game.Off();
    }

    private void SubscibeCharacterObservers(Character character)
    {
        character.DeadEvent += CharacterDead;
        character.FinishEvent += CharacterDetectFinish;
    }
    private void UnSubscibeCharacterObservers(Character character)
    {
        character.DeadEvent -= CharacterDead;
        character.FinishEvent -= CharacterDetectFinish;
    }

    private Character CreateCharacter()
    {
        return _characterFactory.Spawn(prefab,
                    startPoint.position,
                    Quaternion.identity,
                    null);
    }

    private void CharacterDetectFinish() => 
        characterDetectFinishPoint.Callback();

    private void CharacterDead() => 
        characterDead.Callback();
}


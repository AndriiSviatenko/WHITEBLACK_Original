using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TriggerHandler triggerObserver;

    public void Init() => 
        triggerObserver.TriggerEnter += OnEnter;

    public void Off() => 
        triggerObserver.TriggerEnter -= OnEnter;

    private void OnEnter(Collider2D collider)
    {
        if(collider.TryGetComponent(out LightArea lightArea))
            character.Dead();

        if (collider.TryGetComponent(out FinishPoint finishPoint))
            character.Finish();
    }
}
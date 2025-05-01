using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public event Action DeadEvent;
    public event Action FinishEvent;

    [Header("Components")]
    [SerializeField] private Mover mover;
    [SerializeField] private Detector detector;

    private GameInput _gameInput;
    public void SetInput(GameInput gameInput) => 
        _gameInput = gameInput;

    public void Play()
    {
        EnableInput();
        SubscribeInput();

        detector.Init();
    }
    public void Dead()
    {
        DisableInput();
        UnSubscribeInput();
        DeadEvent?.Invoke();
    }
    public void Finish()
    {
        mover.Stop();
        DisableInput();
        UnSubscribeInput();
        FinishEvent?.Invoke();
    }

    private void EnableInput() => 
        _gameInput.GamePlay.Enable();

    private void DisableInput() => 
        _gameInput.GamePlay.Disable();

    private void SubscribeInput()
    {
        _gameInput.GamePlay.Move.started += StartMove;
        _gameInput.GamePlay.Move.performed += Move;
        _gameInput.GamePlay.Move.canceled += StopMove;
    }
    private void UnSubscribeInput()
    {
        _gameInput.GamePlay.Move.started -= StartMove;
        _gameInput.GamePlay.Move.performed -= Move;
        _gameInput.GamePlay.Move.canceled -= StopMove;
    }

    private void StartMove(InputAction.CallbackContext context) => 
        mover.Play();

    private void Move(InputAction.CallbackContext context)
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePos - transform.position).normalized;
        mover.Move(direction);
    }

    private void StopMove(InputAction.CallbackContext context) => 
        mover.Stop();
}

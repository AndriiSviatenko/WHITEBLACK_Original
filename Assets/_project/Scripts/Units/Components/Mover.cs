using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private const float SUCCESS_DISTANCE = 0.1f;

    public event Action MovedToPointSuccess;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private bool _isStop;
    public void Play()
    {
        _isStop = false;
        rb.simulated = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void Stop()
    {
        _isStop = true;
        rb.simulated = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void Move(Vector3 direction)
    {
        if (_isStop) 
            return;

        rb.linearVelocity = direction * speed;
    }
    public void MoveToPoint(Vector3 point)
    {
        if (_isStop)
            return;

        var direction = (point - transform.position).normalized;

        rb.linearVelocity = direction * speed;

        if (CheckDistance(point))
            return;
    }
    private bool CheckDistance(Vector3 point)
    {
        if (Vector3.Distance(transform.position, point) < SUCCESS_DISTANCE)
        {
            MovedToPointSuccess?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}

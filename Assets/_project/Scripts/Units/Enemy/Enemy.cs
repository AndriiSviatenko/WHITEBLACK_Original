using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Mover mover;
    [SerializeField] private Rotator rotator;

    private List<Transform> _points;

    private Transform _prevPoint;
    private Transform _currentPoint;

    private Coroutine _seachPoint;
    private bool _isStop;

    public void SetPoints(List<Transform> points) => 
        _points = new(points);

    public void Play()
    {
        _isStop = false;
        mover.MovedToPointSuccess += GetPoint;
        _seachPoint = StartCoroutine(SearchPointToMove());
    }

    public void Stop()
    {
        _isStop = true;
        mover.MovedToPointSuccess -= GetPoint;

        if (_seachPoint != null)
            StopCoroutine(_seachPoint);
    }

    private void Update()
    {
        if (_isStop)
            return;

        if (_currentPoint == null)
            return;

        rotator.Rotate(_currentPoint.position);
    }
    private void FixedUpdate()
    {
        if (_isStop)
            return;

        if (_currentPoint == null)
            return;

        mover.MoveToPoint(_currentPoint.position);
    }
    private void GetPoint()
    {
        if (_seachPoint != null)
            StopCoroutine(_seachPoint);

        _seachPoint = StartCoroutine(SearchPointToMove());
    }
    private IEnumerator SearchPointToMove()
    {
        while (!_isStop)
        {
            var rndPoint = _points[Random.Range(0, _points.Count)];
            _prevPoint = rndPoint;

            if (_currentPoint != _prevPoint)
            {
                _currentPoint = rndPoint;
                yield break;
            }
            else
            {
                _currentPoint = null;
            }

            yield return null;
        }
    }
}

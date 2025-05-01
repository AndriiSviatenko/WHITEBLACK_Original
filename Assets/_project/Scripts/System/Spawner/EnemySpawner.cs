using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private List<Transform> points;

    [Space(10f)]
    [Header("Enemy Settings")]
    [SerializeField] private Enemy prefab;
    [SerializeField] private Transform spawnPoint;

    [Space(10f)]
    [Header("Settings")]
    [SerializeField] private int count;

    private EnemyFactory _factory = new();
    private List<Enemy> _instances = new();

    public void StartSpawn()
    {
        Cleanup();

        for (int i = 0; i < count; i++)
        {
            var instance = _factory.Spawn
                (
                    prefab,
                    spawnPoint.position,
                    Quaternion.identity,
                    null
                );

            instance.SetPoints(points);
            instance.Play();
            _instances.Add(instance);
        }
    }

    public void Cleanup()
    {
        foreach (var enemy in _instances)
            Destroy(enemy.gameObject);

        _instances.Clear();
    }
}
using UnityEngine;

namespace _Project.Scripts._Infrastructure_.Patterns.Factory.Core
{
    public interface IFactory<T>
    {
        T Spawn(T prefab);
        T Spawn(T prefab, Vector2 position, Quaternion rotation, Transform parent = null);
    }
}
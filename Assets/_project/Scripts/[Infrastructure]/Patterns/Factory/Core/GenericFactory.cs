using System;
using UnityEngine;

namespace _Project.Scripts._Infrastructure_.Patterns.Factory.Core
{
    public abstract class GenericFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        public event Action<T> SpawnEvent;

        public virtual T Spawn(T prefab)
        {
            var instance = UnityEngine.Object.Instantiate(prefab);
            SpawnEvent?.Invoke(instance);
            return instance;
        }

        public virtual T Spawn(T prefab,Vector2 position, Quaternion rotation, Transform parent = null)
        {
            var instance = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
            SpawnEvent?.Invoke(instance);
            return instance;
        }
    }
}
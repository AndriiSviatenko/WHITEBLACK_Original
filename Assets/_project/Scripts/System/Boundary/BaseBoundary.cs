using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.Services.Boundary
{
    public class BaseBoundary : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D coll;
        private List<Transform> _targets = new();

        public void SetupCollider(Vector2 size, Vector2 offset, bool isTrigger)
        {
            coll.size = size;
            coll.offset = offset;
            coll.isTrigger = isTrigger;
        }

        public void AddTarget(Transform target)
        {
            if (!_targets.Contains(target))
                _targets.Add(target);
        }

        public void RemoveTarget(Transform target)
        {
            if (_targets.Contains(target))
                _targets.Remove(target);
        }

        private void LateUpdate()
        {
            if (coll == null || _targets.Count == 0) 
                return;

            Vector2 minBounds = coll.bounds.min;
            Vector2 maxBounds = coll.bounds.max;

            foreach (var target in _targets)
            {
                if (target == null) continue;

                Vector3 position = target.position;
                position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
                position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

                target.position = position;
            }
        }
    }
}


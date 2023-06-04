using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.InstancePooling
{
    /// <summary>
    /// InstancePooler that automatically records new prefabs into their own pools.
    /// </summary>
    public class ObjectPooler
    {
        private static Dictionary<Transform, InstancePool> instances = new Dictionary<Transform, InstancePool>();
        private static Dictionary<Transform, InstancePool> _poolParent = new Dictionary<Transform, InstancePool>();
        /// <summary>
        /// Creates a new Transform, used as replacement for Instantiate(). Activates the GameObject
        /// </summary>
        /// <param name="transform">Transform of the prefab</param>
        /// <param name="parent">Transform of the parent</param>
        /// <param name="position">Position of created object</param>
        /// <param name="rotation">Rotation of created object</param>
        /// <returns>Transform of the object</returns>
        public static Transform Instantiate(Transform transform, Transform parent, Vector3 position, Quaternion rotation)
        {
            InstancePool pool;
            if (!instances.TryGetValue(transform, out pool))
            {
                pool = new InstancePool(transform, parent);
                instances.Add(transform, pool);
            }
            Transform newTransform = pool.InstantiateFromPool(position, rotation);
            _poolParent.Add(newTransform, pool);
            return newTransform;
        }

        /// <summary>
        /// "Destroys" the transform. Actually just returns the object into the pool.
        /// </summary>
        /// <param name="transform">Transform of the object</param>
        public static void Destroy(Transform transform)
        {
            InstancePool pool;
            if (_poolParent.TryGetValue(transform, out pool))
            {
                _poolParent.Remove(transform);
                pool.DestroyToPool(transform);
            }
        }
    }
}

using DKP.InstancePooling;
using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SpawnSystem
{
    /// <summary>
    /// Represents a spawner that will go through a queue of grouped objects. Automatically starts and stops with no delay when the queue is updated.
    /// </summary>
    [Serializable, Obsolete("Use FlowedSpawner and InternalData instead")]
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private SpawnQueue spawnQueueData;
        private Coroutine spawnCoroutine = null;

        [SerializeField]
        private Directions direction;
        public enum Directions
        {
            Right,
            Left
        }

        [SerializeField, Tooltip("Leave empty to set this as its parent")]
        Transform overrideTransformParent;

        private Queue<GroupedSpawn> spawnGroups { get; } = new Queue<GroupedSpawn>();

        private void Start()
        {
            if (spawnQueueData != null)
            {
                SetQueue(spawnQueueData.GetQueue());
            }
        }

        /// <summary>
        /// Adds the spawn group to the end of the queue
        /// </summary>
        /// <param name="group"> Qroup to be queued</param>
        public void Enqueue(GroupedSpawn group)
        {
            spawnGroups.Enqueue(group);
            if (spawnGroups.Count != 1)
            {
                return;
            }
            if (spawnCoroutine != null)
            {
                return;
            }
            spawnCoroutine = StartCoroutine(SpawnNext(0f));
        }

        /// <summary>
        /// Adds the spawn group to the end of the queue
        /// </summary>
        /// <param name="queue">Queue list to be queued</param>
        /// <remarks>will clear queue</remarks>
        public void Enqueue(Queue<GroupedSpawn> queue)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                Enqueue(queue.Dequeue());
            }
        }

        /// <summary>
        /// Removes the spawn group from the beginning of the queue
        /// </summary>
        /// <returns>Queue Object</returns>
        public GroupedSpawn Dequeue()
        {
            if (spawnGroups.Count == 1)
            {
                if (spawnCoroutine != null)
                {
                    StopCoroutine(spawnCoroutine);
                }
            }
            return spawnGroups.Dequeue();
        }

        /// <summary>
        /// Clears the current queue
        /// </summary>
        public void Clear()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            spawnGroups.Clear();
        }

        /// <summary>
        /// Sets the queue data to a new queue
        /// </summary>
        /// <remarks>will clear queue</remarks>
        public void SetQueue(Queue<GroupedSpawn> queue)
        {
            Clear();
            Enqueue(queue);
        }

        private IEnumerator SpawnNext(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (spawnGroups.Count > 0)
            {
                GroupedSpawn enemyGroup = spawnGroups.Dequeue();
                for (int i = 0; i < enemyGroup.SpawnCount; i++)
                {
                    Transform newTransform = ObjectPooler.Instantiate(enemyGroup.Prefab, overrideTransformParent ? overrideTransformParent : transform, transform.position, transform.rotation);
                    IUnit newUnit = newTransform.GetComponent<IUnit>();
                    switch (direction)
                    {
                        case Directions.Right:
                            break;
                        case Directions.Left:
                            newUnit.Model.localScale = new Vector3(-Mathf.Abs(newUnit.Model.localScale.x), newUnit.Model.localScale.y, newUnit.Model.localScale.z);
                            break;
                        default:
                            break;
                    }
                    if (newUnit != null)
                    {
                        newUnit.Spawn();
                    }
                    yield return new WaitForSeconds(enemyGroup.DelayBetweenSpawn);
                }
                StartCoroutine(SpawnNext(enemyGroup.DelayToNextSpawn));
            } else
            {
                StartCoroutine(SpawnNext(1f));
            }
        }
    }
}
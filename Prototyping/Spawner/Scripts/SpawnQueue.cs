using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SpawnSystem
{
    [CreateAssetMenu(fileName = "SpawnQueue", menuName = "SO/SpawnQueue")]
    public class SpawnQueue : ScriptableObject
    {
        [SerializeField]
        private GroupedSpawn[] spawnQueue;

        public Queue<GroupedSpawn> GetQueue()
        {
            return new Queue<GroupedSpawn>(spawnQueue);
        }
    }
    /// <summary>
    /// A grouped spawn queue, e.g. 10 villagers, 5 zombies with delay between spawning and after spawning.
    /// </summary>
    /// <remarks>Used for spawner</remarks>
    [Serializable]
    public class GroupedSpawn
    {
        public Transform Prefab;
        [Tooltip("Number of objects spawned in a group")]
        public int SpawnCount;
        [Tooltip("Wait time after group finished spawning")]
        public float DelayToNextSpawn;
        [Tooltip("Wait time after spawning one object in a group. No use if SpawnCount is 0")]
        public float DelayBetweenSpawn;
        /// <summary>
        /// Creates a new grouped spawn object data
        /// </summary>
        /// <param name="prefab">Prefab to be instantiated</param>
        /// <param name="spawnCount">Count of instances to be created in total</param>
        /// <param name="delayToNextSpawn">Delay before spawning another instance after one is spawned</param>
        /// <param name="delayBetweenSpawn">Delay after the whole group is finished being spawned</param>
        public GroupedSpawn(Transform prefab, int spawnCount = 1, float delayToNextSpawn = 0f, float delayBetweenSpawn = 0f)
        {
            Prefab = prefab;
            SpawnCount = spawnCount;
            DelayToNextSpawn = delayToNextSpawn;
            DelayBetweenSpawn = delayBetweenSpawn;
        }
    }
}

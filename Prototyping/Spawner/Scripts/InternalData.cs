using DKP.InstancePooling;
using DKP.UnitSystem;
using System;
using UnityEngine;

namespace DKP.SpawnSystem
{
    [CreateAssetMenu(fileName = "InternalData", menuName = "SO/InternalData")]
    public class InternalData : ScriptableObject
    {
        [SerializeField]
        private UnitData[] units;
        [SerializeField]
        private SpawnerData[] spawners;

        [Serializable]
        public class UnitData
        {
            public string name;
            public Transform prefab;
        }
        
        [Serializable]
        public class SpawnerData
        {
            public string name;
            public Vector3 position;
            public Directions facing;
        }

        public enum Directions
        {
            Right,
            Left
        }

        public bool ContainsUnit(string unitName)
        {
            return FindUnit(unitName) != null;
        }

        public UnitData FindUnit(string unitName)
        {
            for (int i = 0; i < units.Length; i++)
            {
                if (unitName == units[i].name)
                    return units[i];
            }
            return null;
        }

        public bool ContainsSpawner(string spawnerName)
        {
            return FindSpawner(spawnerName) != null;
        }

        public SpawnerData FindSpawner(string spawnerName)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                if (spawnerName == spawners[i].name)
                    return spawners[i];
            }
            return null;
        }

        /// <summary>
        /// Spawns a unit
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="unit"></param>
        /// <param name="spawner"></param>
        /// <returns>IUnit or null</returns>
        public IUnit Spawn(Transform parent, string unit, string spawner)
        {
            UnitData unitData = FindUnit(unit);
            if (unitData == null)
            {
                Debug.LogWarning($"Unit not found! {unit}");
                return null;
            }
            SpawnerData spawnerData = FindSpawner(spawner);
            if (spawnerData == null)
            {
                Debug.LogWarning($"Spawner not found! {spawner}");
                return null;
            }
            return Spawn(parent, unitData, spawnerData);
        }

        /// <summary>
        /// Spawns a unit
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="unit"></param>
        /// <param name="spawner"></param>
        /// <returns>IUnit or null</returns>
        public IUnit Spawn(Transform parent, UnitData unit, SpawnerData spawner)
        {
            Transform newTransform = ObjectPooler.Instantiate(unit.prefab, parent, spawner.position, Quaternion.identity);
            newTransform.name = unit.name;
            IUnit newUnit = newTransform.GetComponent<IUnit>();
            switch (spawner.facing)
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
            return newUnit;
        }
    }
}

using DKP.InstancePooling;
using DKP.UnitSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SpawnSystem
{
    public class SpawnUnit : MonoBehaviour
    {
        [SerializeField] CreateUnit unit;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Directions direction;
        public enum Directions
        {
            Right,
            Left
        }
        float Cooldown;
        Transform prefab;

        float spawnCooldown;

        private void Start()
        {
            Cooldown = unit.Cooldown;
            spawnCooldown = Cooldown;
            prefab = unit.Prefab;
        }

        private void Update()
        {
            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown <= 0)
            {
                Transform newTransform = ObjectPooler.Instantiate(prefab, spawnPoint ? spawnPoint : transform, transform.position, transform.rotation);
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
                spawnCooldown = Cooldown;
            }
        }
    }
}
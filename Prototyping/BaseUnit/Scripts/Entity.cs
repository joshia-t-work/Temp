using DKP.StateMachineSystem;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [AddComponentMenu(menuName: "Game Units/Entity")]
    public abstract class Entity : MonoBehaviour, IDamagable
    {
        public bool Debug;
        /// <summary>
        /// Orders the entity to take damage from source.
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        /// <param name="damageType">Type of damage</param>
        /// <param name="source">Source of damage, not always from another unit</param>
        public abstract void TakeDamage(float damage, IDamagable.DamageTypes damageType, object source);

        /// <remarks>Does not set health</remarks>
        public abstract void SetMaxHealth(float maxHealth);
        public abstract void SetHealth(float amount);
    }
}

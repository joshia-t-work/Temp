using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    public interface IDamagable
    {
        public void TakeDamage(float damage, DamageTypes damageType, object source);
        public void SetMaxHealth(float maxHealth);
        public void SetHealth(float amount);
        public enum DamageTypes
        {
            None,
            Physical,
            Pure,
        }
    }
}
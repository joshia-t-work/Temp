using DKP.SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "UnitStat", menuName = "SO/UnitStat")]
    public class BaseUnitStats : ScriptableObject
    {
        [SerializeField]
        private Stat hp;
        public Stat HP => hp;

        [SerializeField]
        private Stat attack;
        public Stat AttackDamage => attack;

        [SerializeField, Tooltip("In seconds")]
        private Stat attackSpeed;
        public Stat AttackSpeed => attackSpeed;

        [SerializeField]
        private Stat defense;
        public Stat Defense => defense;

        [SerializeField]
        private Stat attackRange;
        public Stat AttackRange => attackRange;

        [SerializeField]
        private Stat barrier;
        public Stat Barrier => barrier;

        [SerializeField]
        private AttackTypes attackType;
        public AttackTypes AttackType => attackType;
        public enum AttackTypes
        {
            Melee,
            Ranged
        }

        [SerializeField]
        private MoveSpeeds moveSpeed;
        public enum MoveSpeeds
        {
            Slow,
            Medium,
            Fast,
            Immobile,
        }
        public Stat MoveSpeed
        {
            get
            {
                switch (moveSpeed)
                {
                    case MoveSpeeds.Slow:
                        return new Stat(1f);
                    case MoveSpeeds.Medium:
                        return new Stat(2f);
                    case MoveSpeeds.Fast:
                        return new Stat(3f);
                    case MoveSpeeds.Immobile:
                        return new Stat(0f);
                    default:
                        return new Stat(0f);
                }
            }
        }

        [SerializeField]
        private SkillBase[] skills;
        public SkillBase[] Skills => skills;

        [SerializeField]
        private IDamagable.DamageTypes weakness;
        public IDamagable.DamageTypes Weakness => weakness;
    }
}
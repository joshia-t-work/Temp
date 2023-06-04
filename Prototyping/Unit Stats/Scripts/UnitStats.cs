using DKP.SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DKP.UnitSystem
{
    [Serializable]
    public class UnitStats
    {
        public Stat MaxHP { get; private set; }
        public Stat HP { get; private set; }
        public Stat AttackDamage { get; private set; }
        public Stat AttackSpeed { get; private set; }
        public Stat Defense { get; private set; }
        public Stat AttackRange { get; private set; }
        public Stat Barrier { get; private set; }
        public BaseUnitStats.AttackTypes AttackType;
        public Stat MoveSpeed { get; private set; }
        public List<SkillBase> Skills { get; private set; }
        public IDamagable.DamageTypes Weakness;

#if UNITY_EDITOR
        [SerializeField, ReadOnly]
        private float _maxHP;
        [SerializeField, ReadOnly]
        private float _hp;
        [SerializeField, ReadOnly]
        private float _attackDamage;
        [SerializeField, ReadOnly]
        private float _attackSpeed;
        [SerializeField, ReadOnly]
        private float _defense;
        [SerializeField, ReadOnly]
        private float _attackRange;
        [SerializeField, ReadOnly]
        private float _barrier;
        [SerializeField, ReadOnly]
        private float _moveSpeed;

        public void UpdateDebugData()
        {
            _maxHP = MaxHP.Value;
            _hp = HP.Value;
            _attackDamage = AttackDamage.Value;
            _attackSpeed = AttackSpeed.Value;
            _defense = Defense.Value;
            _attackRange = AttackRange.Value;
            _barrier = Barrier.Value;
            _moveSpeed = MoveSpeed.Value;
        }
#endif

        public UnitStats(BaseUnitStats baseUnitStats)
        {
            MaxHP = baseUnitStats.HP.Copy();
            HP = baseUnitStats.HP.Copy();
            AttackDamage = baseUnitStats.AttackDamage.Copy();
            AttackSpeed = baseUnitStats.AttackSpeed.Copy();
            Defense = baseUnitStats.Defense.Copy();
            AttackRange = baseUnitStats.AttackRange.Copy();
            Barrier = baseUnitStats.Barrier.Copy();
            AttackType = baseUnitStats.AttackType;
            MoveSpeed = baseUnitStats.MoveSpeed.Copy();
            Skills = new List<SkillBase>(baseUnitStats.Skills);
            Weakness = baseUnitStats.Weakness;
        }
    }
}

using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SkillSystem
{
    [CreateAssetMenu(fileName = "DedHedsWrathSkill", menuName = "SO/Skills/DedHedsWrath")]
    public class DedHedsWrathSkill : SkillBase
    {
        public float Duration;
        public override Type SkillData => typeof(DedHedsWrathSkillData);

        public override async Task ExecuteAs(ISkillable executor)
        {
            DedHedsWrathSkillData data = (DedHedsWrathSkillData)executor.GetSkillData(this);
            if (data.Cooldown > 0)
            {
                return;
            }
            data.Cooldown = Cooldown;
            StatModifier newModifier = new StatModifier(0.5f, StatModifierType.PercentAdd, executor);
            data.AttackStat.AddModifier(newModifier);
            await Task.Delay(Mathf.RoundToInt(Duration * 1000f));
            data.AttackStat.RemoveModifier(newModifier);
        }
    }

    /// <summary>
    /// Handles skill unique data for each skill such as cooldown or skills with "deals damage based on the amount of kills you have"
    /// </summary>
    public class DedHedsWrathSkillData : SkillData, ISkillDataCooldown
    {
        public float Cooldown { get; set; }
        public Stat AttackStat { get; set; }
        public DedHedsWrathSkillData(SkilledUnit unit)
        {
            AttackStat = unit.UnitStats.AttackDamage;
        }
    }
}
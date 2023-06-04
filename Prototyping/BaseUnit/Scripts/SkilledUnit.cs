using DKP.SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.UnitSystem
{
    /// <summary>
    /// Default template for a skilled unit
    /// </summary>
    public class SkilledUnit : Unit, ISkillable
    {
        /// <inheritdoc/>
        public override void Spawn()
        {
            base.Spawn();
            for (int i = 0; i < UnitStats.Skills.Count; i++)
            {
                SkillBase skill = UnitStats.Skills[i];
                SkillData data = Activator.CreateInstance(skill.SkillData, new object[] { this }) as SkillData;
                skillData.Add(skill, data);
            }
        }

        public override void Update()
        {
            base.Update();
            foreach (SkillData skillData in skillData.Values)
            {
                ((ISkillDataCooldown)skillData).Cooldown -= Time.deltaTime;
            }
        }

        /// <summary>
        /// The skillable object needs a data container for each skill
        /// </summary>
        Dictionary<SkillBase, SkillData> skillData = new Dictionary<SkillBase, SkillData>();

        /// <summary>
        /// For calls from canvas.
        /// </summary>
        public virtual void UIExecuteSkill(SkillBase skill)
        {
            ExecuteSkill(skill);
        }

        /// <summary>
        /// Default ExecuteSkill method calls skill execution on the skill, can have conditionals to check if the character is stunned or etc.
        /// </summary>
        public virtual void ExecuteSkillByID(int index)
        {
            try
            {
                ExecuteSkill(UnitStats.Skills[index]);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Default ExecuteSkill method calls skill execution on the skill, can have conditionals to check if the character is stunned or etc.
        /// </summary>
        public virtual Task ExecuteSkill(SkillBase skill)
        {
            return skill.ExecuteAs(this);
        }

        /// <summary>
        /// Default GetSkillData, tries to get the value and creates a new SkillData of the skill if not found
        /// </summary>
        public virtual SkillData GetSkillData(SkillBase skill)
        {
            SkillData data;
            if (!skillData.TryGetValue(skill, out data))
            {
                // Basically
                // data = new DemoDebugLogSkillData()
                data = Activator.CreateInstance(skill.SkillData, new object[] { this }) as SkillData;
                skillData.Add(skill, data);
            }
            return data;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SkillSystem
{
    /// <summary>
    /// Represents an object that can use skills
    /// </summary>
    public interface ISkillable
    {
        Task ExecuteSkill(SkillBase skill);
        SkillData GetSkillData(SkillBase skill);
    }
}
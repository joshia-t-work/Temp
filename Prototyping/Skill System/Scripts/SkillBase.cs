using System;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SkillSystem
{
    /// <summary>
    /// Represents a generic skill to be inherited in class and later instanstiated as ScriptableObject.
    /// </summary>
    public abstract class SkillBase : ScriptableObject, ISkill
    {
        public string DisplayName;
        public string Description;
        public float Cooldown;
        /// <summary>
        /// The type of SkillData the skill should have
        /// </summary>
        /// <remarks>This will later be casted to the class SkillData</remarks>
        public abstract Type SkillData { get; }
        /// <summary>
        /// Executes the skill and returns true if the execution is successful
        /// </summary>
        /// <param name="executor">The object to execute the skill</param>
        public abstract Task ExecuteAs(ISkillable executor);
    }
}
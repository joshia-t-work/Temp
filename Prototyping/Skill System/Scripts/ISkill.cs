using System;
using System.Threading.Tasks;

namespace DKP.SkillSystem
{
    /// <summary>
    /// Marks a class as a skill
    /// </summary>
    public interface ISkill
    {
        public abstract Type SkillData { get; }
        public abstract Task ExecuteAs(ISkillable executor);
    }
}
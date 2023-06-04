using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SkillSystem
{
    /// <summary>
    /// Represents an object that can use skills from monobehaviour
    /// </summary>
    public abstract class Skillable : MonoBehaviour, ISkillable
    {
        public abstract Task ExecuteSkill(SkillBase skill);
        public abstract SkillData GetSkillData(SkillBase skill);
    }
}
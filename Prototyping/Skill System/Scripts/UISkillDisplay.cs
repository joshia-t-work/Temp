using DKP.ObserverSystem;
using DKP.UnitSystem;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.SkillSystem
{
    public class UISkillDisplay : MonoBehaviour
    {
        [SerializeField, InitializationField]
        SkillAccessTypes SkillAccessType;

        [SerializeField, SerializeReference, InitializationField, ConditionalField(nameof(SkillAccessType), false, SkillAccessTypes.Skill, order = 999)]
        Skillable SkilledObject;
        [SerializeField, InitializationField, ConditionalField(nameof(SkillAccessType), false, SkillAccessTypes.Skill, order = 999)]
        SkilledUnit OptionalSkilledUnit;
        [SerializeField, InitializationField, ConditionalField(nameof(SkillAccessType), false, SkillAccessTypes.Index, order = 999)]
        SkilledUnit SkilledUnit;

        [SerializeField, InitializationField, ConditionalField(nameof(SkillAccessType), false, SkillAccessTypes.Skill, order = 999)]
        SkillBase Skill;
        [SerializeField, InitializationField, ConditionalField(nameof(SkillAccessType), false, SkillAccessTypes.Index, order = 999)]
        int SkillIndex;

        [Header("Optionals")]
        [SerializeField, InitializationField]
        TMP_Text CooldownText;

        [SerializeField, InitializationField]
        Slider CooldownSlider;

        ISkillDataCooldown SkillDataCooldown;
        Observable<float> Cooldown = new Observable<float>(0f);

        private enum SkillAccessTypes
        {
            Index,
            Skill
        }
        private void Start()
        {
            Cooldown.AddObserver(CooldownObserver);
            switch (SkillAccessType)
            {
                case SkillAccessTypes.Index:
                    Skill = SkilledUnit.UnitStats.Skills[SkillIndex];
                    SkillDataCooldown = (ISkillDataCooldown)SkilledUnit.GetSkillData(Skill);
                    break;
                case SkillAccessTypes.Skill:
                    if (OptionalSkilledUnit != null)
                    {
                        SkillDataCooldown = (ISkillDataCooldown)OptionalSkilledUnit.GetSkillData(Skill);
                    } else
                    {
                        SkillDataCooldown = (ISkillDataCooldown)SkilledObject.GetSkillData(Skill);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            Cooldown.Value = SkillDataCooldown.Cooldown;
        }
        private void CooldownObserver(float val)
        {
            if (CooldownText != null)
            {
                if (val <= 0)
                {
                    CooldownText.text = "Ready";
                } else
                {
                    CooldownText.text = $"{Mathf.Round(val * 10f) / 10f}s";
                }
            }
            if (CooldownSlider != null)
                CooldownSlider.value = val / Skill.Cooldown;
        }
    }
}

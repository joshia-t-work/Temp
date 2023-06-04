using DKP.CutsceneSystem;
using DKP.FlowSystem;
using DKP.InstancePooling;
using DKP.ObserverSystem;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.UnitSystem
{
    [AddComponentMenu(menuName: "Game Units/Unit")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Unit : Entity, IUnit
    {
        [SerializeField, ReadOnly, ConditionalField("Debug", order = 999)]
        private Slider slider;

        [SerializeField, ReadOnly, ConditionalField("Debug", order = 999)]
        protected Rigidbody2D rb;

        private Observable<float> sliderValue = new Observable<float>(0f);
        public virtual Transform Model => transform;

        [SerializeField, InitializationField]
        private BaseUnitStats unitStats;

        [SerializeField, InitializationField]
        protected string unitTeam;
        public string UnitTeam => unitTeam;

        public UnitStats UnitStats { get; private set; }

        private Coroutine _lateDestroy = null;

#if UNITY_EDITOR
        [SerializeField, ReadOnly, ConditionalField("Debug", order = 999)]
        private UnitStats _unitStats;
#endif

        [Header("Set this to true")]
        [SerializeField, InitializationField]
        private bool spawnedFromInspector;

#if UNITY_EDITOR
        [ButtonMethod]
        public virtual void updateReferences()
        {
            slider = GetComponentInChildren<Slider>();
            rb = GetComponent<Rigidbody2D>();
        }
#endif

        /// <summary>
        /// Will not be called when spawned from pool.
        /// </summary>
        public virtual void Awake()
        {
            sliderValue.AddObserver(SetSlider);
            if (spawnedFromInspector)
            {
                Spawn();
            }
        }

        /// <summary>
        /// Called when spawned from pool.
        /// </summary>
        public virtual void Spawn()
        {
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            UnitStats = new UnitStats(unitStats);
            UpdateSliderValue();
#if UNITY_EDITOR
            _unitStats = UnitStats;
#endif
        }

        public virtual void Update()
        {
#if UNITY_EDITOR
            _unitStats.UpdateDebugData();
#endif
        }

        public virtual void OnDestroy()
        {
            sliderValue.RemoveObserver(SetSlider);
        }

        private void SetSlider(float value)
        {
            if (slider != null)
            {
                slider.value = Mathf.Clamp01(value);
            }
            if (value <= 0)
            {
                // When two units strike each other, they should kill each other
                // this delays the unit from dying on the frame it is landing its attack
                if (_lateDestroy == null)
                {
                    _lateDestroy = StartCoroutine(LateDestroy());
                }
            }
        }

        /// <summary>
        /// Called when the object reaches 0 health.
        /// </summary>
        public virtual IEnumerator LateDestroy()
        {
            yield return new WaitForFixedUpdate();
            _ = CutsceneEvents.I.InvokeUnitDied(Flow.ct, this);
            ObjectPooler.Destroy(transform);
            _lateDestroy = null;
        }

        private void UpdateSliderValue()
        {
            sliderValue.Value = UnitStats.HP.Value / UnitStats.MaxHP.Value;
        }

        public override void TakeDamage(float damage, IDamagable.DamageTypes damageType, object source)
        {
            float percentDamage = damage / UnitStats.HP.Value;
            UnitStats.HP.BaseValue *= (1 - percentDamage);
            if (UnitStats.HP.Value <= 0)
            {
                Flow.UnitKill(source, this);
            }
            UpdateSliderValue();
        }

        public override void SetMaxHealth(float value)
        {
            UnitStats.MaxHP.BaseValue = value;
            UpdateSliderValue();
        }

        public override void SetHealth(float health)
        {
            TakeDamage(UnitStats.HP.Value - health, IDamagable.DamageTypes.Pure, null);
        }
    }
}
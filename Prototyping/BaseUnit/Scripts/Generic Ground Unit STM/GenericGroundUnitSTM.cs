using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.UnitSystem
{
    /// <summary>
    /// Represents State for a GenericGroundUnit that can move and attack
    /// </summary>
    [RequireComponent(typeof(GenericGroundUnit))]
    public class GenericGroundUnitSTM : StateMachineMono<GenericGroundUnit>
    {
        public const string ATTACK_STATE = "Attack";
        public const string MOVE_STATE = "Move";
        public const string DAMAGED_STATE = "Damaged";
        private Observable<float> _health;
        private float _prevHealth;
        public override void Awake()
        {
            unit = GetComponent<GenericGroundUnit>();
            base.Awake();
        }

        public override void Start() {
            _health = new Observable<float>(unit.UnitStats.HP.Value);
            _prevHealth = _health.Value;
            _health.AddObserver(OnDamaged);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            SetState(MOVE_STATE);
        }

        private void OnDamaged(float value)
        {
            if (value < _prevHealth)
            {
                SetState(DAMAGED_STATE);
            }
            _prevHealth = _health.Value;
        }

        public override void Update()
        {
            base.Update();
            _health.Value = unit.UnitStats.HP.Value;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _health.RemoveObserver(OnDamaged);
        }

        private GenericGroundUnit unit;
        public override GenericGroundUnit data => unit;


    }
}
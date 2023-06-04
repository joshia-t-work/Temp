using System.Collections;
using System.Collections.Generic;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using UnityEngine;
using System;
using DKP.UnitSystem;

namespace DKP.UnitSystem
{
    public class GenericMainCharacterController : MonoBehaviour
    {
        [SerializeField]
        GenericMainCharacterSTM StateMachine;

        public bool isOn;

        #region Input Handlers
        private Observable<bool> jumpValue = new Observable<bool>(false);
        private Observable<float> horizontalInput = new Observable<float>(0f);
        #endregion

        private void OnEnable()
        {
            InputManager.MovementInput.Value1.AddObserver(movementInputListener);
            InputManager.DashInput.Value1.AddObserver(dashInputListener);
            InputManager.AttackInput.Value1.AddObserver(attackInputListener);
            jumpValue.AddObserver(jumpValueChangeListener);
            horizontalInput.AddObserver(horizontalInputChangeListener);
        }

        private void OnDisable()
        {
            InputManager.MovementInput.Value1.RemoveObserver(movementInputListener);
            InputManager.DashInput.Value1.RemoveObserver(dashInputListener);
            InputManager.AttackInput.Value1.RemoveObserver(attackInputListener);
            jumpValue.RemoveObserver(jumpValueChangeListener);
            horizontalInput.RemoveObserver(horizontalInputChangeListener);
        }

        private void movementInputListener(Vector2 obj)
        {
            if (isOn)
            {
                horizontalInput.Value = Math.Sign(InputManager.MovementInput.Value1.Value.x);
                jumpValue.Value = InputManager.MovementInput.Value1.Value.y > 0.4f;
            }
        }

        private void attackInputListener(bool obj)
        {
            if (isOn)
            {
                if (obj)
                {
                    StateMachine.AttackKeyEvent.Invoke();
                }
            }
        }

        private void dashInputListener(bool obj)
        {
            if (isOn)
            {
                if (obj)
                {
                    StateMachine.DashKeyEvent.Invoke();
                }
            }
        }

        private void jumpValueChangeListener(bool obj)
        {
            if (obj)
            {
                StateMachine.JumpKeyEvent.Invoke();
            }
        }

        private void horizontalInputChangeListener(float obj)
        {
            StateMachine.HorizontalMoveEvent.Invoke(obj);
        }
    }
}

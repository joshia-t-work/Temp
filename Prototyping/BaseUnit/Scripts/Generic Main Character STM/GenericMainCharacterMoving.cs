using System;
using System.Collections;
using System.Collections.Generic;
using DKP.ObserverSystem;
using DKP.Singletonmanager;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Main Character Moving State", menuName = "SO/States/Generic Main Character/Moving")]
    public class GenericMainCharacterMoving : StateWithType<GenericMainCharacterSTM, GenericMainCharacter>
    {
        public override string Name => GenericMainCharacterSTM.MOVING_STATE;

        public override void OnStateEnter(string stateFrom)
        {
            Data.animator.SetBool("isRunning", true);
            ThisSTM.HorizontalMoveEvent.AddListener(HorizontalMoveEventListener);
            ThisSTM.DashKeyEvent.AddListener(DashKeyEventListener);
            ThisSTM.AttackKeyEvent.AddListener(AttackKeyEventListener);
            ThisSTM.JumpKeyEvent.AddListener(JumpKeyEventListener);
            Data.isGrounded.AddObserver(GroundedEventListener);
        }

        public override void OnStateExit(string stateFrom)
        {
            Data.animator.SetBool("isRunning", false);
            ThisSTM.HorizontalMoveEvent.RemoveListener(HorizontalMoveEventListener);
            ThisSTM.DashKeyEvent.RemoveListener(DashKeyEventListener);
            ThisSTM.AttackKeyEvent.RemoveListener(AttackKeyEventListener);
            ThisSTM.JumpKeyEvent.RemoveListener(JumpKeyEventListener);
            Data.isGrounded.RemoveObserver(GroundedEventListener);
        }

        public void GroundedEventListener(bool grounded)
        {
            if (grounded && ThisSTM.HorizontalInput == 0)
            {
                ChangeState(GenericMainCharacterSTM.IDLE_STATE);
            }
        }

        private void HorizontalMoveEventListener(float obj)
        {
            ThisSTM.HorizontalInput = obj;
            if (obj == 0 && Data.Rb.velocity.y == 0)
            {
                ChangeState(GenericMainCharacterSTM.IDLE_STATE);
            }
        }

        private void DashKeyEventListener()
        {
            if (Data.dashCharge > 0)
            {
                ChangeState(GenericMainCharacterSTM.DASH_STATE);
            }
        }

        private void AttackKeyEventListener()
        {
            if (Data.Rb.velocity.y.Equals(0))
            {
                ChangeState(GenericMainCharacterSTM.ATTACK_STATE);
            }
        }

        private void JumpKeyEventListener()
        {
            Data.Jump();
        }

        public override void FixedUpdate() {
            float targetSpeed = ThisSTM.HorizontalInput * Data.moveSpeed;
            float speedDif = targetSpeed - Data.Rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? Data.acceleration : Data.frictionDecceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, Data.velPower) * Mathf.Sign(speedDif);

            Data.Rb.AddForce(movement * Vector2.right);
            Data.animator.SetFloat("yVelocity", Data.Rb.velocity.y);
        }
    }
}

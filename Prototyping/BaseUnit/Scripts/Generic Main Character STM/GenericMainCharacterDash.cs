using System;
using System.Collections;
using System.Collections.Generic;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Main Character Dash State", menuName = "SO/States/Generic Main Character/Dash")]
    public class GenericMainCharacterDash : StateWithType<GenericMainCharacterSTM, GenericMainCharacter>
    {
        public override string Name => GenericMainCharacterSTM.DASH_STATE;
        public float dashSpeed = 10f;
        public float dashDuration = 5f;
        private float initGravityScale;
        private bool isDashing = false;
        public override void OnStateEnter(string stateFrom)
        {
            initGravityScale = Data.Rb.gravityScale;
            Data.animator.SetBool("isDashing", true);
            isDashing = true;
            STM.StartCoroutine(Dash());
            ThisSTM.AttackKeyEvent.AddListener(AttackEventListener);
            ThisSTM.JumpKeyEvent.AddListener(JumpEventListener);
        }
        
        IEnumerator Dash(){
            Data.dashCharge -= 1;
            Debug.Log("Dashing init velocity: " + Data.Rb.velocity);
            Data.Rb.gravityScale = 0;
            yield return new WaitForSeconds(dashDuration);
            Debug.Log("Dashing final velocity: " + Data.Rb.velocity);
            ChangeState(GenericMainCharacterSTM.IDLE_STATE);
        }

        public override void OnStateExit(string stateFrom)
        {
            isDashing = false;
            Data.Rb.gravityScale = initGravityScale;
            Data.animator.SetBool("isDashing", false);
            ThisSTM.AttackKeyEvent.RemoveListener(AttackEventListener);
            ThisSTM.JumpKeyEvent.RemoveListener(JumpEventListener);
        }

        private void JumpEventListener()
        {
            if (Data.isGrounded.Value)
            {
                ChangeState(GenericMainCharacterSTM.MOVING_STATE);
            }
        }

        private void AttackEventListener()
        {
            ChangeState(GenericMainCharacterSTM.ATTACK_STATE);
        }

        public override void FixedUpdate()
        {
            if (isDashing)
            {
                Data.Rb.velocity = new Vector2(Mathf.Sign(Data.xOrientation) * dashSpeed, 0);
            }
        }
    }
}

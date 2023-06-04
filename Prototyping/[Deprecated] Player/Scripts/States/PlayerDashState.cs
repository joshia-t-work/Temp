using System;
using System.Collections;
using System.Collections.Generic;
using DKP.Input;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace DKP.Player
{
    [Obsolete("Player is merged to GenericMainCharacter")]
    [CreateAssetMenu(fileName = "Player Dash State", menuName = "SO/States/Player/Dash")]
    public class PlayerDashState : State<GenericMainCharacter>
    {
        public override string Name => "Dash";
        //public float dashSpeed = 10f;
        //public float dashDuration = 5f;
        //private float initGravityScale;
        //public override void OnStateEnter()
        //{
        //    initGravityScale = Data.Rb.gravityScale;
        //    Data.animator.SetBool("isDashing", true);
        //    STM.StartCoroutine(Dash());
        //}
        
        //IEnumerator Dash(){
        //    Data.dashCharge -= 1;
        //    Data.Rb.velocity = new Vector2(Mathf.Sign(Data.xOrientation) * dashSpeed, 0);
        //    Data.Rb.gravityScale = 0;
        //    yield return new WaitForSeconds(dashDuration);
        //    ChangeState(PlayerController.MOVING_STATE);
        //}

        //public override void Update()
        //{
        //    ChangeStateHandler();
        //}

        //private void ChangeStateHandler()
        //{
        //    if (InputManager.AttackInput.Value)
        //    {
        //        ChangeState(PlayerController.ATTACK_STATE);
        //    }

        //    if (Data.jumpInput && Data.isGrounded.Value)
        //    {
        //        ChangeState(PlayerController.MOVING_STATE);
        //    }
        //}

        //public override void OnStateExit()
        //{
        //    Data.Rb.gravityScale = initGravityScale;
        //    Data.animator.SetBool("isDashing", false);
        //}
    }
}

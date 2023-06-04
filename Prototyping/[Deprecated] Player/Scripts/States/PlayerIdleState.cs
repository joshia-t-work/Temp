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
    [CreateAssetMenu(fileName = "Player Idle State", menuName = "SO/States/Player/Idle")]
    public class PlayerIdleState : State<GenericMainCharacter>
    {
        public override string Name => "Idle";
        //public override void OnStateEnter()
        //{
        //    Data.animator.SetBool("isRunning", false);
        //}

        //public override void Update()
        //{
        //    ChangeStateHandler();
        //}

        //public override void FixedUpdate()
        //{
        //    // Friction to get rid of the remaining velocity from previous states
        //    float movement = Mathf.Pow(Mathf.Abs(Data.Rb.velocity.x) * Data.frictionDecceleration, Data.velPower) * Mathf.Sign(-Data.Rb.velocity.x);
        //    Data.Rb.AddForce(movement * Vector2.right);
        //}

        
        //private void ChangeStateHandler(){
        //    if (Data.horizontal != 0 || Data.jumpInput)
        //    {
        //        ChangeState(PlayerController.MOVING_STATE);
        //    }

        //    if (InputManager.DashInput.Value && Data.dashCharge > 0)
        //    {
        //        ChangeState(PlayerController.DASH_STATE);
        //    }

        //    if (InputManager.AttackInput.Value)
        //    {
        //        ChangeState(PlayerController.ATTACK_STATE);
        //    }
        //}
    }
}

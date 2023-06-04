using System;
using System.Collections;
using System.Collections.Generic;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace DKP.Player
{
    [Obsolete("Player is merged to GenericMainCharacter")]
    [CreateAssetMenu(fileName = "Player Moving State", menuName = "SO/States/Player/Moving")]
    public class PlayerMovingState : State<GenericMainCharacter>
    {
        public override string Name => "Moving";
        //[SerializeField] private float moveSpeed = 10f;
        //[SerializeField] private float acceleration = 4f;
        //[SerializeField] private float jumpSpeed = 6f;

        

        //public override void OnStateEnter()
        //{
        //    Data.animator.SetBool("isRunning", true);
        //    if (Data.jumpInput && Data.isGrounded.Value)
        //    {
        //        Jump();
        //    }
        //}

        //public override void Update()
        //{
        //    if (Data.jumpInput && Data.isGrounded.Value)
        //    {
        //        Jump();
        //    }

        //    ChangeStateHandler(); 
        //}

        //public override void FixedUpdate() {
        //    float targetSpeed = Data.horizontal * moveSpeed;
        //    float speedDif = targetSpeed - Data.Rb.velocity.x;
        //    float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? acceleration : Data.frictionDecceleration;
        //    float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, Data.velPower) * Mathf.Sign(speedDif);
            
        //    Data.Rb.AddForce(movement * Vector2.right);
        //}

        //private void Jump(){
        //    Data.Rb.velocity = new Vector2(Data.Rb.velocity.x, 0);
        //    Data.Rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        //}

        //private void ChangeStateHandler(){
        //    if (Data.horizontal == 0 && Data.Rb.velocity.y == 0)
        //    {
        //        ChangeState(PlayerController.IDLE_STATE);
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

        //public override void OnStateExit()
        //{
        //    Data.animator.SetBool("isRunning", false);
        //}
    }
}

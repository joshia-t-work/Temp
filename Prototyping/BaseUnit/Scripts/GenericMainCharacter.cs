using DKP.ObserverSystem;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.UnitSystem
{
    /// <summary>
    /// Represents a generic main character
    /// </summary>
    public class GenericMainCharacter : SkilledUnit
    {
        // Components
        public Animator animator;
        public Rigidbody2D Rb => rb;
        public BoxCollider2D bc;
        public Transform attackPoint;
        public Transform model;

        public string CurrentExpression = "";

        #region Graphics
        [HideInInspector]
        public float xOrientation;
        private Vector3 initialScale;
        #endregion

        [Header("Player Friction")]
        public float frictionDecceleration = 6f;
        public float velPower = 0.9f;
        public Observable<bool> isGrounded;
        public float jumpSpeed = 6f;
        public float moveSpeed = 10f;
        public float acceleration = 4f;
        [ReadOnly]
        public int dashCharge = 1;

        public override void Awake()
        {
            base.Awake();
            initialScale = model.localScale;
            isGrounded = new Observable<bool>(false);
            isGrounded.AddObserver(isGroundedListener);
        }

        public override void Update()
        {
            base.Update();

            animator.SetFloat("yVelocity", rb.velocity.y);
            isGrounded.Value = checkIsGrounded();
            if (isGrounded.Value == true)
            {
                dashCharge = 1;
            }
        }

        public void ChangePlayerOrientation(float orientation)
        {
            if (orientation != 0)
            {
                xOrientation = orientation;
                Vector3 scale = model.localScale;
                scale.x = xOrientation * initialScale.x;
                model.localScale = scale;
            }
        }

        public void Jump()
        {
            if (isGrounded.Value)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, 0);
                Rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }

        public bool checkIsGrounded()
        {
            Vector3 downChecker = new Vector3(0, -0.1f, 0);
            return Physics2D.OverlapBoxAll(bc.bounds.center + downChecker, bc.bounds.size, 0, LayerMask.GetMask("Ground")).Length > 0;
        }

        private void isGroundedListener(bool value)
        {
            animator.SetBool("isGrounded", value);
        }

        public override void OnDestroy()
        {
            isGrounded.RemoveObserver(isGroundedListener);
        }

        private void FixedUpdate()
        {
            float movement = Mathf.Pow(Mathf.Abs(Rb.velocity.x) * frictionDecceleration, velPower) * Mathf.Sign(-Rb.velocity.x);
            Rb.AddForce(movement * Vector2.right);
        }
    }
}
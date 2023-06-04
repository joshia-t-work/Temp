using System;
using System.Collections;
using System.Collections.Generic;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Main Character Attack State", menuName = "SO/States/Generic Main Character/Attack")]
    public class GenericMainCharacterAttack : StateWithType<GenericMainCharacterSTM, GenericMainCharacter>
    {
        public override string Name => GenericMainCharacterSTM.ATTACK_STATE;
        [SerializeField] private float attackAnimationDelay = 0.1f;
        [SerializeField] private float attackRadius = 0.5f;
        [Tooltip("Time in seconds to execute next combo before attack finishes.")]
        [SerializeField] private float listenAttackKeyThreshold = 0.1f;
        private bool shouldAttack;
        private bool shouldListenAttack;
        private int combo = 0;
        public override void OnStateEnter(string stateFrom)
        {
            Data.animator.SetBool("isAttacking", true);
            STM.StartCoroutine(Attack());
            if (stateFrom != Name)
                combo = 0;
            Data.animator.SetInteger("comboState", combo);
            ThisSTM.AttackKeyEvent.AddListener(AttackEventListener);
            shouldAttack = false;
        }

        private void AttackEventListener()
        {
            if (shouldListenAttack)
                shouldAttack = true;
        }

        IEnumerator Attack() {
            yield return new WaitForSeconds(attackAnimationDelay);
            DoAttack();
            float remainingAttackDuration = Data.UnitStats.AttackSpeed.Value - attackAnimationDelay - listenAttackKeyThreshold;
            yield return new WaitForSeconds(remainingAttackDuration);
            shouldListenAttack = true;
            yield return new WaitForSeconds(listenAttackKeyThreshold);
            shouldListenAttack = false;
            if (shouldAttack)
            {
                combo += 1;
                Data.animator.SetInteger("comboState", combo);
                ChangeState(GenericMainCharacterSTM.ATTACK_STATE);
            } else
            {
                ChangeState(GenericMainCharacterSTM.IDLE_STATE);
            }
        }

        private void DoAttack() 
        {
            RaycastHit2D[] enemiesToDamage = Physics2D.CircleCastAll(Data.attackPoint.position, attackRadius, Vector2.zero, 0, LayerMask.GetMask("Unit"));
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Rigidbody2D enemyRb = enemiesToDamage[i].rigidbody;
                if (enemyRb != null)
                {
                    if (enemyRb.TryGetComponent(out Unit enemy))
                    {
                        if (enemy.UnitTeam != Data.UnitTeam)
                        {
                            enemy.TakeDamage(Data.UnitStats.AttackDamage.Value, IDamagable.DamageTypes.Physical, Data);
                            break;
                        }
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            // Friction to get rid of the remaining velocity from previous states
            float movement = Mathf.Pow(Mathf.Abs(Data.Rb.velocity.x) * Data.frictionDecceleration, Data.velPower) * Mathf.Sign(-Data.Rb.velocity.x);
            Data.Rb.AddForce(movement * Vector2.right);
        }
#if UNITY_EDITOR
        public override void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Data.attackPoint.position, attackRadius);
        }
#endif

        public override void OnStateExit(string stateFrom)
        {
            Data.animator.SetBool("isAttacking", false);
            ThisSTM.AttackKeyEvent.RemoveListener(AttackEventListener);
        }
    }
}

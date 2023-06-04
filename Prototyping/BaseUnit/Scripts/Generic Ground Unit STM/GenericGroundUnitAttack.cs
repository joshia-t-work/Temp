using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Ground Unit Attack State", menuName = "SO/States/Generic Ground Unit/Attack")]
    public class GenericGroundUnitAttack : State<GenericGroundUnit>
    {
        public override string Name => GenericGroundUnitSTM.ATTACK_STATE;
        private Coroutine attackCoroutine;
        private Unit lockedTarget;
        public override void OnStateEnter(string stateFrom)
        {
            lockedTarget = null;
            Data.Stop();
            attackCoroutine = Data.StartCoroutine(Attack());
        }
        public override void OnStateExit(string stateFrom)
        {
            if (attackCoroutine != null)
            {
                Data.StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
        public override void Update()
        {
            if (Data.ColliderOnAttackRange.Count == 0)
            {
                ChangeState(GenericGroundUnitSTM.MOVE_STATE);
            } else
            {
                if (lockedTarget == null)
                {
                    lockedTarget = Data.ColliderOnAttackRange[0].attachedRigidbody.GetComponent<Unit>();
                }
                else
                {
                    if (lockedTarget.UnitStats.HP.Value <= 0f)
                    {
                        lockedTarget = null;
                    }
                }
            }
        }
        IEnumerator Attack()
        {
            yield return new WaitForSeconds(Data.UnitStats.AttackSpeed.Value);
            if (lockedTarget != null)
            {
                lockedTarget.TakeDamage(Data.UnitStats.AttackDamage.Value, IDamagable.DamageTypes.Physical, Data);
            }
            attackCoroutine = Data.StartCoroutine(Attack());
        }
    }
}
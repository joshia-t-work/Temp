using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Ground Unit Move State", menuName = "SO/States/Generic Ground Unit/Move")]
    public class GenericGroundUnitMove : State<GenericGroundUnit>
    {
        public override string Name => GenericGroundUnitSTM.MOVE_STATE;
        private Coroutine moveCoroutine;
        //WaitForSeconds wait = new WaitForSeconds(0.2f);
        //public override void OnStateEnter()
        //{
        //    moveCoroutine = Data.StartCoroutine(Move());
        //}
        //public override void OnStateExit()
        //{
        //    if (moveCoroutine != null)
        //    {
        //        Data.StopCoroutine(moveCoroutine);
        //        moveCoroutine = null;
        //    }
        //}
        public override void Update()
        {
            if (Data.ColliderOnAttackRange.Count > 0)
            {
                ChangeState(GenericGroundUnitSTM.ATTACK_STATE);
            } else
            {
                Data.Move();
            }
        }
        //IEnumerator Move()
        //{
        //    yield return wait;
        //    Data.Move();
        //    moveCoroutine = Data.StartCoroutine(Move());
        //}
    }
}
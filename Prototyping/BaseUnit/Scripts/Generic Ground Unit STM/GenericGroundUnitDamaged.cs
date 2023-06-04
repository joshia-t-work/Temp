using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Ground Unit Damaged State", menuName = "SO/States/Generic Ground Unit/Damaged")]
    public class GenericGroundUnitDamaged : State<GenericGroundUnit>
    {
        public override string Name => GenericGroundUnitSTM.DAMAGED_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            Data.KnockBack();
            STM.StartCoroutine(Damaged());
        }

        IEnumerator Damaged(){
            yield return new WaitForSeconds(0.3f);
            ChangeState(GenericGroundUnitSTM.MOVE_STATE);
        }
        //public override void Update()
        //{
        //    if (Data.ColliderOnAttackRange.Count > 0)
        //    {
        //        ChangeState(GenericGroundUnitSTM.ATTACK_STATE);
        //    } else
        //    {
        //        // Data.Damaged();
        //    }
        //}
    }
}
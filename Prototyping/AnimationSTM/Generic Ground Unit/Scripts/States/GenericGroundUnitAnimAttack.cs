using AnimationSTM;
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Basic Zombie Attack State", menuName = "SO/States/Basic Zombie/Anim/Attack")]
    public class GenericGroundUnitAnimAttack : State<GenericGroundUnitAnimSTM>
    {
        public override string Name => GenericGroundUnitAnimSTM.ATTACK_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            Data.Anim.SetBool("isAttacking", true);
        }
        public override void OnStateExit(string stateFrom)
        {
            Data.Anim.SetBool("isAttacking", false);
        }


        // STATE CHANGER
        public override void Update()
        {
            if (Data.LogicState == GenericGroundUnitAnimSTM.DAMAGED_STATE)
            {
                ChangeState(GenericGroundUnitAnimSTM.DAMAGED_STATE);
            } 
            
            else if (Data.LogicState == GenericGroundUnitAnimSTM.MOVE_STATE)
            {
                ChangeState(GenericGroundUnitAnimSTM.MOVE_STATE);
            }
        }
    }
}
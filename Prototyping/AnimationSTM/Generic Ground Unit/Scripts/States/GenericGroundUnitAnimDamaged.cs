using AnimationSTM;
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Basic Zombie Damaged State", menuName = "SO/States/Basic Zombie/Anim/Damaged")]
    public class GenericGroundUnitAnimDamaged : State<GenericGroundUnitAnimSTM>
    {
        public override string Name => GenericGroundUnitAnimSTM.DAMAGED_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            Data.Anim.SetBool("isDamaged", true);            
        }
        public override void OnStateExit(string stateFrom)
        {
            Data.Anim.SetBool("isDamaged", false);                 
        }


        // STATE CHANGER
        public override void Update()
        {
            if (Data.LogicState == GenericGroundUnitAnimSTM.ATTACK_STATE)
            {
                ChangeState(GenericGroundUnitAnimSTM.ATTACK_STATE);
            } 
            
            else if (Data.LogicState == GenericGroundUnitAnimSTM.MOVE_STATE)
            {
                ChangeState(GenericGroundUnitAnimSTM.MOVE_STATE);
            }
        }
    }
}
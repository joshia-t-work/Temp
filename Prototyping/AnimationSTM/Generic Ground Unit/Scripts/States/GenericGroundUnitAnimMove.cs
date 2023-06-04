using AnimationSTM;
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Basic Zombie Move State", menuName = "SO/States/Basic Zombie/Anim/Move")]
    public class GenericGroundUnitAnimMove : State<GenericGroundUnitAnimSTM>
    {
        public override string Name => GenericGroundUnitAnimSTM.MOVE_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            
        }
        public override void OnStateExit(string stateFrom)
        {
            
        }


        // STATE CHANGER
        public override void Update()
        {
            if (Data.LogicState == GenericGroundUnitAnimSTM.ATTACK_STATE)
            {
                ChangeState(GenericGroundUnitAnimSTM.ATTACK_STATE);
            } 
            
            else if (Data.LogicState == GenericGroundUnitAnimSTM.DAMAGED_STATE)
            {
                ChangeState(GenericGroundUnitAnimSTM.DAMAGED_STATE);
            }
        }
    }
}
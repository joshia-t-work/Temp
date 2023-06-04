using System.Collections;
using System.Collections.Generic;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Main Character Expression State", menuName = "SO/States/Generic Main Character/Expression")]
    public class GenericMainCharacterExpression : StateWithType<GenericMainCharacterSTM, GenericMainCharacter>
    {
        public override string Name => GenericMainCharacterSTM.EXPRESSION_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            if (Data.CurrentExpression != "")
            {
                Data.animator.SetBool(Data.CurrentExpression, true);
            }
        }

        public override void OnStateExit(string stateFrom)
        {
            if (Data.CurrentExpression != "")
            {
                Data.animator.SetBool(Data.CurrentExpression, false);
            }
        }
    }
}

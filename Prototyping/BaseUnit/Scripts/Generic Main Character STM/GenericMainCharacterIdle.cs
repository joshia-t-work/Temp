using DKP.StateMachineSystem;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Generic Main Character Idle State", menuName = "SO/States/Generic Main Character/Idle")]
    public class GenericMainCharacterIdle : StateWithType<GenericMainCharacterSTM, GenericMainCharacter>
    {
        public override string Name => GenericMainCharacterSTM.IDLE_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            if (ThisSTM.HorizontalInput != 0)
            {
                ChangeState(GenericMainCharacterSTM.MOVING_STATE);
            }

            ThisSTM.AttackKeyEvent.AddListener(AttackEventListener);
            ThisSTM.HorizontalMoveEvent.AddListener(HorizontalMoveEventListener);
            ThisSTM.JumpKeyEvent.AddListener(JumpEventListener);
            ThisSTM.DashKeyEvent.AddListener(DashEventListener);
        }

        public override void OnStateExit(string stateFrom)
        {
            ThisSTM.AttackKeyEvent.RemoveListener(AttackEventListener);
            ThisSTM.HorizontalMoveEvent.RemoveListener(HorizontalMoveEventListener);
            ThisSTM.JumpKeyEvent.RemoveListener(JumpEventListener);
            ThisSTM.DashKeyEvent.RemoveListener(DashEventListener);
        }

        private void AttackEventListener()
        {
            ChangeState(GenericMainCharacterSTM.ATTACK_STATE);
        }

        private void HorizontalMoveEventListener(float obj)
        {
            ThisSTM.HorizontalInput = obj;
            if (obj != 0)
            {
                ChangeState(GenericMainCharacterSTM.MOVING_STATE);
            }
        }

        private void JumpEventListener()
        {
            Data.Jump();
            ChangeState(GenericMainCharacterSTM.MOVING_STATE);
        }

        private void DashEventListener()
        {
            if (Data.dashCharge > 0)
            {
                ChangeState(GenericMainCharacterSTM.DASH_STATE);
            }
        }
    }
}

using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using UnityEngine;

namespace AnimationSTM{
    public class GenericGroundUnitAnimSTM : StateMachineMono<GenericGroundUnitAnimSTM>
    {
        // STATES
        public const string MOVE_STATE = "Move";
        public const string ATTACK_STATE = "Attack";
        public const string DAMAGED_STATE = "Damaged";
        public override GenericGroundUnitAnimSTM data => this;
        public Animator Anim;
        [HideInInspector]
        public GenericGroundUnitSTM UnitSTM;
        [HideInInspector]
        public string LogicState;

        public override void Awake() {
            base.Awake();
            UnitSTM = GetComponent<GenericGroundUnitSTM>();
        }

        public override void Update(){
            base.Update();
            LogicState = UnitSTM.CurrentState.Name;
        }
    }

}

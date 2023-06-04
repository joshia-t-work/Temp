using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using UnityEngine;

namespace DKP.UnitSystem
{
    /// <summary>
    /// Handles a generic main character by receiving move, attack, dash and expression inputs
    /// </summary>
    public class GenericMainCharacterSTM : StateMachineMono<GenericMainCharacter>
    {
        public const string IDLE_STATE = "IDLE";
        public const string EXPRESSION_STATE = "EXPRESSION";
        public const string MOVING_STATE = "MOVING";
        public const string ATTACK_STATE = "ATTACK";
        public const string DASH_STATE = "DASH";
        public override GenericMainCharacter data => characterData;

        [SerializeField]
        private GenericMainCharacter characterData;

        #region Input Handlers
        public GameEvent JumpKeyEvent = new GameEvent();
        public GameEvent AttackKeyEvent = new GameEvent();
        public GameEvent DashKeyEvent = new GameEvent();
        public GameEvent<float> HorizontalMoveEvent = new GameEvent<float>();
        
        [ReadOnly]
        public float HorizontalInput;
        #endregion

        public override void Awake()
        {
            base.Awake();
            HorizontalMoveEvent.AddListener(HorizontalMoveEventListener);
        }

        public override void OnDestroy()
        {
            HorizontalMoveEvent.RemoveListener(HorizontalMoveEventListener);
            base.OnDestroy();
        }

        private void HorizontalMoveEventListener(float obj)
        {
            HorizontalInput = obj;
            data.ChangePlayerOrientation(obj);
        }
        //public override void FixedUpdate()
        //{
        //    base.FixedUpdate();
        //    float movement = Mathf.Pow(Mathf.Abs(data.Rb.velocity.x) * data.frictionDecceleration, data.velPower) * Mathf.Sign(-data.Rb.velocity.x);
        //    data.Rb.AddForce(movement * Vector2.right);
        //}
    }
}

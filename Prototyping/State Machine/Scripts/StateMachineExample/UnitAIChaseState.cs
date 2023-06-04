using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Debugging
{
    [CreateAssetMenu(fileName = "Unit Chase State", menuName = "SO/States/Unit/Chase")]
    public class UnitAIChaseState : State<UnitAI>
    {
        public override string Name => "Chase";
        public override void OnStateEnter(string stateFrom)
        {
            Data.SpriteRenderer.color = Data.ChaseColor;
        }
        public override void Update()
        {
            if (Physics2D.OverlapBox(Data.transform.position, Vector2.one * 5f, 0f) == null)
            {
                ChangeState(UnitAI.IDLE_STATE);
            }
        }
        public override void OnDrawGizmos()
        {
            Gizmos.color = Data.ChaseColor;
            Gizmos.DrawWireCube(Data.transform.position, Vector2.one * 5f);
        }
    }
}
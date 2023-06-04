using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Debugging
{
    [CreateAssetMenu(fileName = "Unit Idle State", menuName = "SO/States/Unit/Idle")]
    public class UnitAIIdleState : State<UnitAI>
    {
        public override string Name => "Idle";
        private float m_Time = 0f;
        public override void OnStateEnter(string stateFrom)
        {
            Data.SpriteRenderer.color = Data.IdleColor;
            m_Time = 0f;
        }
        public override void Update()
        {
            if (Physics2D.OverlapBox(Data.transform.position, Vector2.one * 3f, 0f) != null)
            {
                m_Time += Time.deltaTime;
            } else
            {
                m_Time = Mathf.Max(0f, m_Time - Time.deltaTime);
            }
            if (m_Time > 3f)
            {
                ChangeState(UnitAI.CHASE_STATE);
            }
        }
        public override void OnDrawGizmos()
        {
            Gizmos.color = Data.IdleColor;
            if (m_Time > 0)
            {
                Gizmos.color = Data.ChaseColor;
            }
            Gizmos.DrawWireCube(Data.transform.position, Vector2.one * 3f);
            if (m_Time > 0)
            {
                Gizmos.DrawWireCube(Data.transform.position, Vector2.one * (3f - 2f * m_Time / 3f));
            }
        }
    }
}
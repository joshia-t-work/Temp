using DKP.StateMachineSystem;
using DKP.UnitSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DKP.Debugging
{
    public class UnitAI : StateMachineMono<UnitAI>
    {
        public const string IDLE_STATE = "Idle";
        public const string CHASE_STATE = "Chase";
        public Color ChaseColor;
        public Color IdleColor;
        public SpriteRenderer SpriteRenderer;
        public override UnitAI data => this;
    }
}
using System.Collections;
using System.Collections.Generic;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using UnityEngine;
using System;
using DKP.UnitSystem;

namespace DKP.Player
{
    [Obsolete("Player is merged to GenericMainCharacter")]
    public class PlayerController : StateMachineMono<GenericMainCharacter>
    {
        public const string IDLE_STATE = "Idle";
        public const string MOVING_STATE = "Moving";
        public const string ATTACK_STATE = "Attack";
        public const string DASH_STATE = "Dash";
        public override GenericMainCharacter data => playerData;
        
        [SerializeField]
        private GenericMainCharacter playerData;
    }
}

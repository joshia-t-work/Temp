using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.CutsceneSystem
{
    /// <summary>
    /// Represents State for a GenericGroundUnit that can move and attack
    /// </summary>
    public class IGCharacterSTM : StateMachineMono<IGCharacterSTM>
    {
        public override IGCharacterSTM data => this;
        //        Idle Animation
        //- Walking Animation
        //- Depressed Walking Animation
        //- Running Animation
        //- Talking Animation
        //- Angry Animation
        //- Charisma Animation(Basically nge-flex)
        //- Basic Attack Animation(3 combo) [Bare Hand]
        //- Basic Attack Animation(3 combo - extendable) [Axe]
        //- Cutting Wood Animation
        //- Damaged Animation
        //- Dying Animation
        //- Jumping Animation
        //- Dash Animation
        public const string EXPRESSION_STATE = "Expression";
        public const string ATTACK_STATE = "Attack";
        public const string MOVE_STATE = "Move";
    }
}
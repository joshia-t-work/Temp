using DKP.StateMachineSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    [CreateAssetMenu(fileName = "WinLose Survival State", menuName = "SO/States/WinLose/Survival")]
    public class WinLoseSurvival : State<WinLoseSTM>
    {
        public override string Name => WinLoseSTM.SURVIVAL_STATE;
    }
}
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    [CreateAssetMenu(fileName = "WinLose Init State", menuName = "SO/States/WinLose/Init")]
    public class WinLoseInit : State<WinLoseSTM>
    {
        public override string Name => WinLoseSTM.INIT_STATE;
    }
}
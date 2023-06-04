using DKP.CameraSystem;
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Camera None State", menuName = "SO/States/Camera/None")]
    public class CameraNone : State<CameraController>
    {
        public override string Name => CameraSTM.NONE_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            Data.isEnabled = false;
        }
        public override void OnStateExit(string stateFrom)
        {
            Data.isEnabled = true;
        }
    }
}
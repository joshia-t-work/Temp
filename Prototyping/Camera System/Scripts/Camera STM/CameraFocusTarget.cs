using DKP.CameraSystem;
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Camera Focus Target State", menuName = "SO/States/Camera/Focus Target")]
    public class CameraFocusTarget : State<CameraController>
    {
        public override string Name => CameraSTM.FOCUS_TARGET_STATE;
        public override void Update()
        {
            if (CameraController.CameraTarget != null)
            {
                Data.SetTarget(CameraController.CameraTarget.position, 5f);
            }
        }
    }
}
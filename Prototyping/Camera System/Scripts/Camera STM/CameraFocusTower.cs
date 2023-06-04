using DKP.CameraSystem;
using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    [CreateAssetMenu(fileName = "Camera Focus Tower State", menuName = "SO/States/Camera/Focus Tower")]
    public class CameraFocusTower : State<CameraController>
    {
        public override string Name => CameraSTM.FOCUS_TOWER_STATE;
        public override void Update()
        {
            if (Data.AllyTowers.Count > 0)
            {
                Data.SetTarget(Data.AllyTowers[0].transform.position, 20f);
            }
        }
    }
}
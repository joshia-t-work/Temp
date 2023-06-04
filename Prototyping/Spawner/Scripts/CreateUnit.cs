using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SpawnSystem
{
    [CreateAssetMenu(fileName = "Unit", menuName = "SO/Create Unit")]
    public class CreateUnit : ScriptableObject
    {
        public Transform Prefab;
        [Tooltip("Wait time after the unit finished spawning")]
        public float Cooldown;
    }
}

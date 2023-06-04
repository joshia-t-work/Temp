using DKP.ObserverSystem.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Game
{
    /// <summary>
    /// Event container for all game events
    /// </summary>
    [CreateAssetMenu(fileName = "Game Events", menuName = "SO/Game Event/Group/Game")]
    public class GameEvents : ScriptableObject
    {
        /// <summary>
        /// Death of a unit (object killSource, Unit killed)
        /// </summary>
        [SerializeField]
        public GameEventObjectObject UnitDeath;
    }
}
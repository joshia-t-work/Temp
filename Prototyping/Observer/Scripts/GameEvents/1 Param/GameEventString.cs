using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObserverSystem.GameEvents
{
    [CreateAssetMenu(fileName = "Game Event String", menuName = "SO/Game Event/1 Param/String")]
    public class GameEventString : GlobalGameEvent<string> { }
}
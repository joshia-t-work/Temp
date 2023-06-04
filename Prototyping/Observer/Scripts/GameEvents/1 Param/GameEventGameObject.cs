using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObserverSystem.GameEvents
{
    [CreateAssetMenu(fileName = "Game Event GameObject", menuName = "SO/Game Event/1 Param/GameObject")]
    public class GameEventGameObject : GlobalGameEvent<GameObject> { }
}
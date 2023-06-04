using DKP.ObserverSystem;
using DKP.ObserverSystem.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.UnitSystem
{
    /// <summary>
    /// Represents a generic tower
    /// </summary>
    public class GenericTower : GenericGroundUnit
    {
        [SerializeField]
        GameEventGameObject towerCreationEvent;

        [SerializeField]
        GameEventGameObject towerDestructionEvent;

        public virtual void Start()
        {
            towerCreationEvent?.Invoke(gameObject);
        }
        public override IEnumerator LateDestroy()
        {
            yield return new WaitForFixedUpdate();
            gameObject.SetActive(false);
            towerDestructionEvent?.Invoke(gameObject);
        }
    }
}
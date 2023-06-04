using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.UnitSystem
{
    public interface IUnit
    {
        public Transform Model { get; }
        public void Spawn();

    }
}
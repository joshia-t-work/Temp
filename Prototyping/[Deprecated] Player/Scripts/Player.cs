using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Player
{
    [Obsolete("Player is merged to GenericMainCharacter")]
    public class Player : Unit
    {
        private void Start()
        {
            SetMaxHealth(100);
            SetHealth(100);
        }
    }
}



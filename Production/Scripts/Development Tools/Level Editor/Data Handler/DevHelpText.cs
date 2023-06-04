#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor.Data
{
    public class DevHelpText : DevBaseMovable
    {
        public override ISerializableData Data => null;

        public override void SetData(ISerializableData data)
        {
            throw new NotImplementedException();
        }
    }
}
#endif
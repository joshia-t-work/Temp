#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor.Data
{
    [Obsolete("Deprecated as of 2.0")]
    public class DevMainThread : DevBaseScript<SerializableMainThread, SerializableMainThreadAction, DevMainThreadAction>
    {
        public override ISerializableData Data => throw new NotImplementedException();
    }
}
#endif
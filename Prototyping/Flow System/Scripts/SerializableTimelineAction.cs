using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    [Serializable]
    public class SerializableTimelineAction : ISerializableData
    {
    }
    public enum DialogueActionTypes
    {
        Cutscene,
        Character,
        Setting,
    }
}
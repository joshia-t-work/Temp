using System;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.SaveSystem
{
    [Serializable]
    public class GameData : IData
    {
        public static string s_DataFileName => "GameData.dat";
        public string DataFileName => s_DataFileName;
        [HideInInspector]
        public float[] ResourceValues;
    }
}
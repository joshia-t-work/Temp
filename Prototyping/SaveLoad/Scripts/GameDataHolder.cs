﻿using DKP.ResourceSystem;
using UnityEngine;

namespace DKP.SaveSystem
{
    public class GameDataHolder : MonoBehaviour
    {
        [SerializeField]
        private Resource[] Resources;
        private GameData GameData = new GameData();
        private void Awake()
        {
            for (int i = 0; i < Resources.Length; i++)
            {
                Resources[i].SetDefaultValue();
            }
        }
        public void Load()
        {
            GameData.ResourceValues = SaveManager.LoadGameData<GameData>(GameData.s_DataFileName).ResourceValues;
            for (int i = 0; i < Resources.Length; i++)
            {
                Resources[i].SetValue(GameData.ResourceValues[i]);
            }
        }

        public void Save()
        {
            GameData.ResourceValues = new float[Resources.Length];
            for (int i = 0; i < Resources.Length; i++)
            {
                GameData.ResourceValues[i] = Resources[i].GetValue();
            }
            SaveManager.SaveGameData(GameData);
        }
    }
}
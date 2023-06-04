#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor
{
    public class DevChangeLog : MonoBehaviour
    {
        [SerializeField]
        DevUpdateLog history;
        [SerializeField]
        TMP_Text versionText;
        [SerializeField]
        TMP_Text updateText;
        int versionIndex = int.MaxValue;

        private void OnEnable()
        {
            updateDisplay();
        }

        public void SwitchVersion(int delta)
        {
            versionIndex = versionIndex + delta;
            updateDisplay();
        }

        private void updateDisplay()
        {
            versionIndex = Mathf.Clamp(versionIndex, 0, history.Log.Length - 1);
            versionText.text = $"Update Log v{history.Log[versionIndex].Version}";
            updateText.text = history.Log[versionIndex].Desc;
        }
    }
}
#endif
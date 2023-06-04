#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevRightclickable : MonoBehaviour
    {
        public string ContextMenuText;
        [HideInInspector]
        public DevEditorDataContainer DevData;
        public ContextMenuTypes MenuType;
        public enum ContextMenuTypes
        {
            Default,
            Destroy
        }
        private void Awake()
        {
            DevData = GetComponent<DevEditorDataContainer>();
        }
    }
}
#endif
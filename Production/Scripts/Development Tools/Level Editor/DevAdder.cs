#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevAdder : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text destroyCM;
        private DevActiveSwitch switcher;

        public static DevAdder Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            switcher = GetComponent<DevActiveSwitch>();
        }
        private void OnEnable()
        {
            if (DevMouseControls.rightClicked == null)
            {
                switch (DevEditorDataContainer.EditingWindow)
                {
                    case "Design":
                        switcher.Switch(2);
                        break;
                    default:
                        switcher.Switch(0);
                        break;
                }
            } else
            {
                switch (DevMouseControls.rightClicked.MenuType)
                {
                    case DevRightclickable.ContextMenuTypes.Default:
                        switcher.Switch(0);
                        break;
                    case DevRightclickable.ContextMenuTypes.Destroy:
                        destroyCM.text = $"Remove {DevMouseControls.rightClicked.ContextMenuText}";
                        switcher.Switch(1);
                        break;
                    default:
                        switcher.Switch(0);
                        break;
                }
            }
            //AddCharacterScriptButton.interactable = (DevDataContainer.GameBookData.Characters.Count > 0);
        }
        public void AddCharacter(DevPrefabInstantiator prefabInstantiator)
        {
            Add<DevImageContainer>(prefabInstantiator, DevEditorDataContainer.AddImageContainer());
        }
        public void AddActionScript(DevPrefabInstantiator prefabInstantiator)
        {
            Add<DevActionScript>(prefabInstantiator, DevEditorDataContainer.AddActionScript());
        }
        private void Add<TData>(DevPrefabInstantiator prefabInstantiator, ISerializableData data) where TData : IDevDataContainer
        {
            Transform newTransform = prefabInstantiator.Add();
            TData dataContainer = newTransform.GetComponent<TData>();
            ((ISerializableWorldObject)data).WorldPosition = new SerializableVector3(newTransform.position.x, newTransform.position.y, newTransform.position.z);
            dataContainer.SetData(data);
            dataContainer.Link();
        }
        public void Destroy()
        {
            DevRootLayout root = DevMouseControls.rightClicked.GetComponentInParent<DevRootLayout>();
            DevMouseControls.rightClicked.transform.SetParent(null);
            root?.RefreshRect(() =>
            {
                Destroy(DevMouseControls.rightClicked.gameObject);
            });
        }
    }
}
#endif
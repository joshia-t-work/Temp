#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor.Data
{
    [Obsolete("Deprecated as of 2.0")]
    public class DevScriptAction : DevBaseScriptAction<SerializableScript, SerializableScriptAction>
    {
        [Header("Deserialize")]
        [SerializeField]
        TMP_Dropdown _actionType;
        [SerializeField]
        TMP_Dropdown _scene;

        protected override void changeEventListener(object data)
        {
            if (data == DevEditorDataContainer.GameBookData.SceneSetting.Images)
            {
                SceneImagesListChangeObserver(DevEditorDataContainer.GameBookData.SceneSetting.Images);
            }
            for (int i = 0; i < DevEditorDataContainer.GameBookData.SceneSetting.Images.Count; i++)
            {
                if (data == DevEditorDataContainer.GameBookData.SceneSetting.Images[i])
                {
                    if (_scene.options.Count != DevEditorDataContainer.GameBookData.SceneSetting.Images.Count)
                    {
                        SceneImagesListChangeObserver(DevEditorDataContainer.GameBookData.SceneSetting.Images);
                    }
                    _scene.options[i].text = DevEditorDataContainer.GameBookData.SceneSetting.Images[i].Name;
                }
            }
        }
        public void OnActionChange(int val)
        {
            SerializableScriptAction.ScriptActionType = (ScriptActionTypes)val;
        }
        public void OnSceneImageChange(int val)
        {
            SerializableScriptAction.SceneImageIndex = val;
        }

        private void SceneImagesListChangeObserver(List<SerializableImageText> sceneImages)
        {
            while (sceneImages.Count != _scene.options.Count)
            {
                if (sceneImages.Count > _scene.options.Count)
                {
                    _scene.options.Add(new TMP_Dropdown.OptionData(sceneImages[_scene.options.Count].Name));
                }
                else if (sceneImages.Count < _scene.options.Count)
                {
                    _scene.options.RemoveAt(_scene.options.Count - 1);
                }
            }
        }

        public override void SetData(ISerializableData data)
        {
            base.SetData(data);
            _actionType.value = (int)SerializableScriptAction.ScriptActionType;
            SceneImagesListChangeObserver(DevEditorDataContainer.GameBookData.SceneSetting.Images);
            _scene.value = SerializableScriptAction.SceneImageIndex;
        }

        public override void Link()
        {
            base.Link();
        }
    }
}
#endif
#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor.Data
{
    public class DevImageText : DevBaseDataContainer
    {
        public override ISerializableData Data => SerializableImageText;
        [Header("Deserialize")]
        [SerializeField]
        DevSerializedImage _image;
        [SerializeField]
        TMP_InputField _name;

        public SerializableImageText SerializableImageText { get; private set; }
        public void OnNameChange(string val)
        {
            SerializableImageText.SetName(val);
            ChangeEvent.Invoke(SerializableImageText);
        }
        protected override void OnDestroy()
        {
            SerializableImageText.Destroy();
            for (int i = 0; i < DevEditorDataContainer.GameBookData.Images.Count; i++)
            {
                if (DevEditorDataContainer.GameBookData.Images[i].Images.Contains(SerializableImageText))
                {
                    DevEditorDataContainer.GameBookData.Images[i].Images.Remove(SerializableImageText);
                    break;
                }
            }
        }
        public override void SetData(ISerializableData data)
        {
            SerializableImageText imageText = (SerializableImageText)data;
            SerializableImageText = imageText;
            _name.text = imageText.Name;
            _image.SetData(imageText.Image);
            ChildData.Add(_image);
        }
    }
}
#endif
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

namespace DKP.Development.LevelEditor.Data
{
    public class DevImageContainer : DevBaseMovable
    {
        public override ISerializableData Data => SerializableImageContainer;
        public virtual SerializableImageContainer SerializableImageContainer { get; protected set; }
        [SerializeField]
        DevPrefabInstantiator imageInstantiator;
        [SerializeField]
        TMP_InputField imageText;
        protected override void OnDestroy()
        {
            base.OnDestroy();
            SerializableImageContainer.Destroy();
            DevEditorDataContainer.GameBookData.Images.ImageContainers.Remove(SerializableImageContainer);
        }

        public void UIAddImage()
        {
            AddImage();
            ChangeEvent.Invoke(SerializableImageContainer.Images);
        }

        public void SetName(string name)
        {
            SerializableImageContainer.SetObjectName(name);
        }

        public DevImageText AddImage()
        {
            SerializableImageText serializableCharacterExpression = new SerializableImageText();
            //serializableCharacterExpression.Owner = SerializableImageContainer;
            SerializableImageContainer.Images.Add(serializableCharacterExpression);

            return AddImage(serializableCharacterExpression);
        }

        public DevImageText AddImage(SerializableImageText expressionData)
        {
            Transform newTransform = imageInstantiator.Add();
            DevImageText expression = newTransform.GetComponent<DevImageText>();
            expression.SetData(expressionData);
            //DevEditorDataContainer.Components.Add(expressionData, expression);
            return expression;
        }

        public override void SetData(ISerializableData data)
        {
            base.SetData(data);
            SerializableImageContainer imageContainer = (SerializableImageContainer)data;
            SerializableImageContainer = imageContainer;
            imageText.SetTextWithoutNotify(SerializableImageContainer.ObjectName);
            for (int i = 0; i < imageContainer.Images.Count; i++)
            {
                if (i >= ChildData.Count)
                {
                    SerializableImageText imageData = imageContainer.Images[i];
                    DevImageText imageText = AddImage(imageData);
                    ChildData.Add(imageText);
                }
            }
        }
    }
}
#endif
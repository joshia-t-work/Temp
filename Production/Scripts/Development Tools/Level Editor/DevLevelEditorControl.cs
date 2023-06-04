#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevLevelEditorControl : DevMouseControls
    {
        public static DevLevelEditorControl I;
        protected override void Awake()
        {
            base.Awake();
            I = this;
        }

        public void ResetCam()
        {
            setCamPos(Vector3.zero);
            setCamOrtho(SerializableCameraPosition.DEFAULT_ZOOM);
        }

        protected override void setCamOrtho(float val)
        {
            base.setCamOrtho(val);
            switch (DevEditorDataContainer.EditingWindow)
            {
                case "Design":
                    DevEditorDataContainer.GameBookData.DesignerCamera.Zoom = val;
                    break;
                default:
                    DevEditorDataContainer.GameBookData.EditorCamera.Zoom = val;
                    break;
            }
        }

        protected override void setCamPos(Vector3 pos)
        {
            base.setCamPos(pos);
            switch (DevEditorDataContainer.EditingWindow)
            {
                case "Design":
                    DevEditorDataContainer.GameBookData.DesignerCamera.Position = new SaveSystem.Data.SerializableVector3(pos.x, pos.y, pos.z);
                    break;
                default:
                    DevEditorDataContainer.GameBookData.EditorCamera.Position = new SaveSystem.Data.SerializableVector3(pos.x, pos.y, pos.z);
                    break;
            }
        }
    }
}
#endif
#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.FlowSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevActionScript : DevBaseMovable
    {
        [Header("References")]
        [SerializeField] TMP_InputField title;
        [SerializeField] TMP_InputField desc;
        [SerializeField] Image status;
        [SerializeField] Color statusSuccess;
        [SerializeField] Color statusWarning;
        [SerializeField] Color statusFail;
        [SerializeField, ReadOnly]
        string tempName;
        string tempDesc;
        bool lintStatus = true;
        bool isDuplicate = true;
        bool isLatched = false;
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct;

        public override ISerializableData Data => SerializableActionScript;
        public SerializableActionScript SerializableActionScript;

        protected override void OnDestroy()
        {
            cts.Cancel();
            base.OnDestroy();
            Unlatch();
            SerializableActionScript.Destroy();
            DevEditorDataContainer.GameBookData.ActionScripts.ActionScripts.Remove(SerializableActionScript);
        }
        public void UpdateTitle(string text)
        {
            if (text != tempName)
            {
                if (DevEditorDataContainer.GameBookData.ActionScripts[text] == null)
                {
                    isDuplicate = false;
                } else
                {
                    isDuplicate = true;
                }
            } else
            {
                isDuplicate = false;
            }
            UpdateStatus();
        }
        private void UpdateStatus()
        {
            if (lintStatus && !isDuplicate)
            {
                status.color = statusSuccess;
            }
            else
            {
                if (!lintStatus)
                {
                    status.color = statusFail;
                }
                else
                {
                    status.color = statusWarning;
                }
            }
        }
        public void SaveTitle(string text)
        {
            if (text != tempName)
            {
                if (DevEditorDataContainer.GameBookData.ActionScripts[text] == null)
                {
                    tempName = text;
                    SerializableActionScript.SetObjectName(tempName);
                } else
                {
                    title.text = tempName;
                }
            }
        }
        public void UpdateDesc(string text)
        {
            tempDesc = text;
            SerializableActionScript.Description = tempDesc;
        }
        public void EditThis()
        {
            Unlatch();
            DevActionScriptEditor.ActionScript = SerializableActionScript;
            DevActionScriptEditor.I.gameObject.SetActive(true);
        }

        public override void SetData(ISerializableData data)
        {
            base.SetData(data);
            ct = cts.Token;
            SerializableActionScript = (SerializableActionScript)data;
            SerializableActionScript.OnNameChanged += OnDataChanged;
            SerializableActionScript.OnModified += Latch;
            Latch();
            transform.position = new Vector3(SerializableActionScript.WorldPosition.X, SerializableActionScript.WorldPosition.Y, SerializableActionScript.WorldPosition.Z);
            title.text = SerializableActionScript.ObjectName;
            tempName = SerializableActionScript.ObjectName;
            desc.text = SerializableActionScript.Description;
            status.color = statusSuccess;
        }

        private void OnDataChanged(string oldName, string newname)
        {
            tempName = newname;
            title.SetTextWithoutNotify(newname);
        }

        public override void Link()
        {
            base.Link();
            //Latch();
        }

        private void Latch()
        {
            if (!isLatched)
            {
                isLatched = true;
                for (int i = 0; i < SerializableActionScript.Actions.Count; i++)
                {
                    if (SerializableActionScript.Actions[i] is ILatchable latchable)
                    {
                        latchable.Latch(latchResponse);
                    }
                }
            }
        }

        private void Unlatch()
        {
            isLatched = false;
            for (int i = 0; i < SerializableActionScript.Actions.Count; i++)
            {
                if (SerializableActionScript.Actions[i] is ILatchable latchable)
                {
                    latchable.Unlatch();
                }
            }
        }

        private void latchResponse(CommandResponse obj, string err)
        {
            switch (obj)
            {
                case CommandResponse.Success:
                    break;
                case CommandResponse.Fail:
                    try
                    {
                        Task.Run(async () =>
                        {
                            await Task.Delay(100);
                            lintStatus = false;
                            UpdateStatus();
                            DevConsole.PushText(err);
                        }, ct);
                    }
                    catch (TaskCanceledException) { }
                    break;
                default:
                    break;
            }
        }
    }
}
#endif
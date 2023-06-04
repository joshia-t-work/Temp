#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor.Data
{
    [Obsolete("Removed in 2.3")]
    public class DevLinker : DevRaycastable
    {
        public override ISerializableData Data => ParentAction;
        public BaseSerializableAction ParentAction = null;

        [SerializeField]
        Color unlinkedColor;

        [SerializeField]
        Color linkedColor;

        [SerializeField]
        Image image;

        [SerializeField]
        int linkIndex = 0;

        [SerializeField]
        LineRenderer lineRenderer;

        public Observable<bool> isLinked = new Observable<bool>();
        public LinkType LinkType;
        [HideInInspector]
        public DevLinker LinkedObject = null;
        private bool _isHeld;
        private Vector3 _pos;
        Observable<Vector3> myPos = new Observable<Vector3>(Vector3.zero);
        Observable<Vector3> linkedPos = new Observable<Vector3>(Vector3.zero);
        Observable<bool> lineRendererStatus = new Observable<bool>(false);

        RectTransform rectTransform;
        Vector2 anchorMin;
        Vector2 anchorMax;
        Vector2 anchorPosition;

        public void SetFlip(int flipped)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            switch (flipped)
            {
                case 0:
                    rectTransform.anchorMin = anchorMin;
                    rectTransform.anchorMax = anchorMax;
                    rectTransform.anchoredPosition = anchorPosition;
                    break;
                case 1:
                    rectTransform.anchorMin = new Vector2(1f - anchorMin.x, 1f - anchorMin.y);
                    rectTransform.anchorMax = new Vector2(1f - anchorMax.x, 1f - anchorMax.y);
                    rectTransform.anchoredPosition = -anchorPosition;
                    break;
                case 2:
                    rectTransform.anchorMin = new Vector2(1f - anchorMin.x, anchorMin.y);
                    rectTransform.anchorMax = new Vector2(1f - anchorMax.x, anchorMax.y);
                    rectTransform.anchoredPosition = new Vector2(-anchorPosition.x, anchorPosition.y);
                    break;
                case 3:
                    if (LinkType == LinkType.To)
                    {
                        rectTransform.anchorMin = new Vector2(1f - anchorMin.x, anchorMin.y);
                        rectTransform.anchorMax = new Vector2(1f - anchorMax.x, anchorMax.y);
                        rectTransform.anchoredPosition = new Vector2(-anchorPosition.x, anchorPosition.y);
                    }
                    else
                    {
                        rectTransform.anchorMin = anchorMin;
                        rectTransform.anchorMax = anchorMax;
                        rectTransform.anchoredPosition = anchorPosition;
                    }
                    break;
                case 4:
                    if (LinkType == LinkType.From)
                    {
                        rectTransform.anchorMin = new Vector2(1f - anchorMin.x, anchorMin.y);
                        rectTransform.anchorMax = new Vector2(1f - anchorMax.x, anchorMax.y);
                        rectTransform.anchoredPosition = new Vector2(-anchorPosition.x, anchorPosition.y);
                    }
                    else
                    {
                        rectTransform.anchorMin = anchorMin;
                        rectTransform.anchorMax = anchorMax;
                        rectTransform.anchoredPosition = anchorPosition;
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            anchorMin = rectTransform.anchorMin;
            anchorMax = rectTransform.anchorMax;
            anchorPosition = rectTransform.anchoredPosition;
            myPos.AddObserver(myPosMovedListener);
            linkedPos.AddObserver(linkedPosMovedListener);
            lineRendererStatus.AddObserver(lineRendererListener);
            isLinked.AddObserverAndCall(isLinkedListener);
        }

        protected override void OnDestroy()
        {
            myPos.RemoveObserver(myPosMovedListener);
            linkedPos.RemoveObserver(linkedPosMovedListener);
            lineRendererStatus.RemoveObserver(lineRendererListener);
            isLinked.RemoveObserver(isLinkedListener);
        }

        private void isLinkedListener(bool obj)
        {
            if (obj)
            {
                image.color = linkedColor;
            } else
            {
                image.color = unlinkedColor;
            }
        }

        private void myPosMovedListener(Vector3 pos)
        {
            lineRenderer.SetPosition(0, pos);
        }
        private void linkedPosMovedListener(Vector3 pos)
        {
            lineRenderer.SetPosition(1, pos);
        }

        private void lineRendererListener(bool val)
        {
            lineRenderer.enabled = val;
        }

        public void SetHeld(bool held, Vector3 pos)
        {
            _isHeld = held;
            _pos = pos;
        }
        public void SetLink(DevLinker link)
        {
            if (link == null)
            {
                isLinked.Value = false;
                LinkedObject.isLinked.Value = false;
                LinkedObject = link;
                ParentAction.NextAction[linkIndex] = null;
            } else
            {
                //IDevDataContainer linkedObj = link.GetComponentInParent<IDevDataContainer>();
                LinkedObject = link;
                ParentAction.NextAction[linkIndex] = link.ParentAction;
                isLinked.Value = true;
                LinkedObject.isLinked.Value = true;
                //if (linkedObj != null)
                //{
                //    Debug.Log(linkedObj.Data.GetType());
                //    Debug.Log(nextAction);
                //}
            }
        }
        public override void SetData(ISerializableData data)
        {
            ParentAction = (BaseSerializableAction)data;
            SetFlip(ParentAction.Flipped);
        }
        public override void Link()
        {
            if (LinkType == LinkType.To)
            {
                return;
            }
            if (ParentAction.NextAction[linkIndex] == null)
            {
                return;
            }
            IDevDataContainer targetActionComponent = DevEditorDataContainer.GetComponent<IDevDataContainer>(ParentAction.NextAction[linkIndex]);
            if (targetActionComponent == null)
            {
                return;
            }
            DevLinker[] linkers = ((MonoBehaviour)targetActionComponent).GetComponentsInChildren<DevLinker>();
            for (int i = 0; i < linkers.Length; i++)
            {
                if (linkers[i].LinkType == LinkType.To)
                {
                    isLinked.Value = true;
                    LinkedObject = linkers[i];
                    LinkedObject.isLinked.Value = true;
                    return;
                }
            }
            Debug.LogWarning("Failed to link");
        }
        //IEnumerator delayedLink(ReferenceObservable<BaseSerializableAction> link, bool create = false)
        //{
        //    yield return new WaitForSeconds(1f);
        //    DevCharacterScriptAction targetCharacterAction;
        //    nextAction = link;
        //    SerializableCharacterScriptAction targetCharacterActionData = (SerializableCharacterScriptAction)nextAction.Value;
        //    if (targetCharacterActionData == null)
        //    {
        //        yield break;
        //    }
        //    if (DevDataContainer.CharacterScriptActions.TryGetValue(targetCharacterActionData, out targetCharacterAction))
        //    {
        //        if (targetCharacterAction != null)
        //        {
        //            DevLinker[] linkers = targetCharacterAction.GetComponentsInChildren<DevLinker>();
        //            for (int i = 0; i < linkers.Length; i++)
        //            {
        //                if (linkers[i].LinkType == LinkType.To)
        //                {
        //                    LinkedObject = linkers[i];
        //                    yield break;
        //                }
        //            }
        //        }
        //    }
        //}
        private void Update()
        {
            if (_isHeld)
            {
                lineRendererStatus.Value = true;
                myPos.Value = transform.position;
                linkedPos.Value = _pos;
                return;
            }
            if (LinkedObject == null)
            {
                lineRendererStatus.Value = false;
            } else
            {
                myPos.Value = transform.position;
                linkedPos.Value = LinkedObject.transform.position;
                lineRendererStatus.Value = true;
                bool temp = lineRendererStatus.Value;
            }
        }

        //private void OnDrawGizmos()
        //{
        //    if (ParentAction is SerializableCharacterScriptAction)
        //    {
        //        if (((SerializableCharacterScriptAction)ParentAction).Parent != null)
        //        {
        //            GUIStyle style = new GUIStyle();
        //            style.normal.textColor = Color.black;
        //            Handles.Label(transform.position, ((SerializableCharacterScriptAction)ParentAction).Parent.GetHashCode().ToString(), style);
        //        }
        //    }
        //    if (ParentAction is SerializableScriptAction)
        //    {
        //        if (((SerializableScriptAction)ParentAction).Parent != null)
        //        {
        //            GUIStyle style = new GUIStyle();
        //            style.normal.textColor = Color.black;
        //            Handles.Label(transform.position, ((SerializableScriptAction)ParentAction).Parent.GetHashCode().ToString(), style);
        //        }
        //    }
        //}
    }
    public enum LinkType
    {
        To,
        From,
    }
}
#endif
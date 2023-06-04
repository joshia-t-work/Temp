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
    public class DevRootLayout : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTransform;
        public void RefreshRect(Action action)
        {
            StartCoroutine(Refresh(action));
        }
        IEnumerator Refresh(Action action)
        {
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}
#endif
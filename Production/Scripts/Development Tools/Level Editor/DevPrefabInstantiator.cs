#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevPrefabInstantiator : MonoBehaviour
    {
        [SerializeField]
        Transform prefabToModify;

        DevRootLayout rootLayout;

        private void Awake()
        {
            rootLayout = GetComponentInParent<DevRootLayout>();
        }

        public void UIAdd()
        {
            Add();
        }

        public Transform Add()
        {
            Transform newTransform = Instantiate(prefabToModify, transform.parent);
            newTransform.gameObject.SetActive(true);
            newTransform.position = (Vector2)DevLevelEditorControl.rightClickPos;
            newTransform.SetSiblingIndex(Mathf.Max(0, transform.parent.childCount - 3));
            rootLayout?.RefreshRect(null);
            DevEditorDataContainer.Objects.Enqueue(newTransform.gameObject);
            return newTransform;
        }
        public void Remove()
        {
            prefabToModify.SetParent(null);
            rootLayout?.RefreshRect(() =>
            {
                Destroy(prefabToModify.gameObject);
            });
        }
    }
}
#endif
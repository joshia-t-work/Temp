#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor
{
    public class DevActiveSwitch : MonoBehaviour
    {
        [SerializeField]
        GameObject[] gameObjects;
        int prevIndex = 0;
        private void Awake()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (i != prevIndex)
                {
                    TrySetActive(gameObjects[i], false);
                }
            }
        }
        public void Switch(int index)
        {
            if (prevIndex > -1)
            {
                TrySetActive(gameObjects[prevIndex], false);
            }
            TrySetActive(gameObjects[index], true);
            prevIndex = index;
            DevRootLayout root = GetComponent<DevRootLayout>();
            if (root == null)
            {
                root = GetComponentInParent<DevRootLayout>();
            }
            root?.RefreshRect(null);
        }
        private void TrySetActive(GameObject gameObject, bool val)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(val);
            }
        }
    }
}
#endif
#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.FileSystem;
using DKP.Input;
using DKP.SaveSystem;
using DKP.SaveSystem.Data;
using DKP.SceneSystem;
using DKP.Singletonmanager;
using MyBox.Internal;
using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor.Data
{
    public class DevFileHandler : MonoBehaviour
    {
        static DevFileHandler I;
        [SerializeField] TMP_Text CurrentFileDisplay;
        private void Awake()
        {
            I = this;
        }
        IEnumerator refresh()
        {
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
        }
        private void SetCurrentFile(string path)
        {
            CurrentFileDisplay.text = Path.GetFileName(path);
            StartCoroutine(refresh());
        }
        public static void TryLoadPath(string path)
        {
            try
            {
                SerializableGameBook gamebook;
                void errorCB()
                {
                    Debug.LogWarning($"File failed to load: {path}");
                    gamebook = new SerializableGameBook();
                }
                gamebook = SaveManager.LoadGameData<SerializableGameBook>(folderPath: "", dataFileName: path, errorCB);
                DevEditorDataContainer.TrySetData(gamebook);
            }
            catch (SerializationException)
            {
                return;
            }
            finally
            {
                PlayerPrefs.SetString("editor_dkpbook_path", path);
                I.SetCurrentFile(path);
            }
        }
        /// <summary>
        /// Shorthand for <c>SaveManager.SaveGameData(DevEditorDataContainer.GameBookData);</c>
        /// </summary>
        public static void SaveFileToCache()
        {
            SaveManager.SaveGameData(DevEditorDataContainer.GameBookData);
        }
        public static void SaveFile(string path)
        {
            SaveManager.SaveGameData(DevEditorDataContainer.GameBookData, path);
            I.SetCurrentFile(path);
        }
        public static void NewFile()
        {
            PlayerPrefs.SetString("editor_dkpbook_path", "");
            I.SetCurrentFile("untitled.dkpbook");
        }
    }
}
#endif
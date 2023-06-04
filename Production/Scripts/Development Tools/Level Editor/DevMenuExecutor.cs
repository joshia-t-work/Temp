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
    public class DevMenuExecutor : MonoBehaviour
    {
        [SerializeField]
        GameObject EditorGameObject;
        [SerializeField]
        GameObject DesignerGameObject;
        [SerializeField]
        DevMouseControls Camera;
        [SerializeField]
        GameObject Console;

        private void Start()
        {
            StartCoroutine(lateStart());
        }

        IEnumerator lateStart()
        {
            yield return new WaitForSecondsRealtime(1f);
            LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
        }

        public void UIFileNew()
        {
            DevFileHandler.NewFile();
            SerializableGameBook book = new SerializableGameBook();
            book.Version = DevEditorDataContainer.Instance.Version;
            DevEditorDataContainer.TrySetData(book);
        }
        public void UIFileOpen()
        {
            FileExplorer.LoadBook((path) =>
            {
                DevFileHandler.TryLoadPath(path);
            });
        }
        public void UIFileSaveAs()
        {
            FileExplorer.SaveBook((path) => DevFileHandler.SaveFile(path));
        }
        public void UIFileSaveToCache()
        {
            DevFileHandler.SaveFileToCache();
        }
        public void UIFileSave()
        {
            string path = PlayerPrefs.GetString(DevEditorPrefs.EDITOR_SAVED_FILE_PATH, "");
            if (path == "")
            {
                return;
            }
            DevFileHandler.SaveFile(path);
        }
        public void UIFileExit()
        {
            Application.Quit();
        }

        public void UIViewReset()
        {
            DevLevelEditorControl.I.ResetCam();
        }

        public void UIViewToggleConsole()
        {
            Console.SetActive(!Console.activeSelf);
        }

        public void UIViewConsoleClear()
        {
            DevConsole.ClearText();
        }

        //public void UIDesign()
        //{
        //    EditorGameObject.SetActive(false);
        //    DesignerGameObject.SetActive(true);
        //    Camera.SetData(DevEditorDataContainer.GameBookData.DesignerCamera);
        //    DevEditorDataContainer.EditingWindow = "Design";
        //}

        //public void UIEdit()
        //{
        //    DesignerGameObject.SetActive(false);
        //    EditorGameObject.SetActive(true);
        //    Camera.SetData(DevEditorDataContainer.GameBookData.EditorCamera);
        //    DevEditorDataContainer.EditingWindow = "Edit";
        //}

        public void UITestScene()
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream stream = new FileStream(Application.persistentDataPath + "/Saves/test.baiwd", FileMode.Create);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.SceneSetting);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.MainThread);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.Scripts);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.Characters);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.Backgrounds);
            //for (int i = 0; i < DevEditorDataContainer.GameBookData.Images.ImageContainers.Count; i++)
            //{
            //    for (int ii = 0; ii < DevEditorDataContainer.GameBookData.Images.ImageContainers[i].Images.Count; ii++)
            //    {
            //        bf.Serialize(stream, DevEditorDataContainer.GameBookData.Images.ImageContainers[i].Images[ii].Image);
            //    }
            //    bf.Serialize(stream, DevEditorDataContainer.GameBookData.Images.ImageContainers[i]);
            //}
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.Images);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.CharacterScripts);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.Triggers);
            //bf.Serialize(stream, DevEditorDataContainer.GameBookData.ActionScripts);
            //stream.Close();

            DevFileHandler.SaveFileToCache();
            SceneLoader.LoadScene("Game");
        }

    }
}
#endif
using DKP.SaveSystem.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DKP.Development.LevelEditor
{
    public class DevLoader : MonoBehaviour
    {
        [SerializeField]
        TMP_Text versionCompare;

        SerializableGameBook gameDataToLoad;

        public void SetData(SerializableGameBook gameBookData)
        {
            gameDataToLoad = gameBookData;
            if (gameBookData.Version == null)
            {
                versionCompare.text = $"Save Version: null\nEditor Version: {DevEditorDataContainer.Instance.Version}";
            } else
            {
                versionCompare.text = $"Save Version: {gameBookData.Version}\nEditor Version: {DevEditorDataContainer.Instance.Version}";
            }
        }

        public void UIContinue()
        {
            DevEditorDataContainer.SetData(gameDataToLoad);
            gameObject.SetActive(false);
        }

        public void UICancel()
        {
            gameObject.SetActive(false);
        }
    }
}

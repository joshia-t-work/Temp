using DKP.SceneSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] editorObjects;
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    private void Awake()
    {
        for (int i = 0; i < editorObjects.Length; i++)
        {
            editorObjects[i].SetActive(true);
        }
    }
#endif
    public void UIExit()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneLoader.LoadScene("Game");
    }
    public void LoadEditor()
    {
        SceneLoader.LoadScene("Dev Level Editor");
    }
}

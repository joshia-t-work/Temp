using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenMenu(GameObject box)
    {
        box.transform.LeanMoveLocal(new Vector2(0, 0), 1).setEaseOutQuart();
    }

    public void CloseMenu(GameObject box)
    {
        box.transform.LeanMoveLocal(new Vector3(0, -800), 1).setEaseInQuart();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

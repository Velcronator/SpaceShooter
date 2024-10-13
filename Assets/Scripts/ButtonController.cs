using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class ButtonController : MonoBehaviour
{
    public void OnLevelString(string level)
    {
        CanvasFader.instance.FadeOutToLevel(level);
    }

    public void OnLevelInt(int level)
    {
        CanvasFader.instance.FadeOutToLevel(level);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnRestart()
    {
        CanvasFader.instance.FadeOutToLevel(SceneManager.GetActiveScene().name);
    }

    public void OnRestartLevel()
    {
        CanvasFader.instance.FadeOutToLevel(SceneManager.GetActiveScene().name);
    }

    public void OnNextLevel()
    {
        CanvasFader.instance.FadeOutToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


}

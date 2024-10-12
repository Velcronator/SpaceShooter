using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private void Start()
    {
        EndGameManager.instance.RegisterPanelController(this);
        HideScreens();
    }

    public void ShowWinScreen()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _winScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _loseScreen.SetActive(true);
    }

    public void HideScreens()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
    }

    //public void RestartGame()
    //{
    //    HideScreens();
    //    GameManager.Instance.RestartGame();
    //}

    //public void QuitGame()
    //{
    //    Application.Quit();
    //}


}

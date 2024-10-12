using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager instance;
    public bool _gameOver = false;

    private PanelController _panelController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this); 
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartResolveSequence()
    {
        StopCoroutine(nameof(ResolveSequence));
        StartCoroutine(ResolveSequence());
    }

    private IEnumerator ResolveSequence()
    {
        yield return new WaitForSeconds(2f);
        ResolveGame();
    }

    public void WinGame()
    {
        _panelController.ShowWinScreen();
        //unlock the next level
        //score
        _gameOver = true;
    }

    public void LoseGame()
    {
        _panelController.ShowLoseScreen();
        //reload the current level
        _gameOver = true;
    }

    public void ResolveGame()
    {
        if (_gameOver == false)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void RegisterPanelController(PanelController panelController)
    {
        _panelController = panelController;
    }


}

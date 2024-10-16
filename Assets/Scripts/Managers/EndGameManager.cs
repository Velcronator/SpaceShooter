using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager instance;
    public bool _gameOver = false;

    private PanelController _panelController;
    private TextMeshProUGUI _scoreText;

    public int _score = 0;
    [HideInInspector] public string levelUnlock = "LevelUnlock";

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

    public void UpdateScore(int scoreValue)
    {
        _score += scoreValue;
        _scoreText.text = "Score: " + _score.ToString();
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
        ScoreSet();
        _panelController.ShowWinScreen();
        _gameOver = true;
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevel > PlayerPrefs.GetInt(levelUnlock, 0))
        {
            PlayerPrefs.SetInt(levelUnlock, nextLevel);
        }
    }

    public void LoseGame()
    {
        ScoreSet();
        _panelController.ShowLoseScreen();
        _gameOver = true;
    }

    public void ScoreSet()
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, _score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);

        if (_score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, _score);
        }
        // reset the score
        _score = 0;
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

    public void RegisterScoreText(TextMeshProUGUI scoreText)
    {
        _scoreText = scoreText;
    }


}

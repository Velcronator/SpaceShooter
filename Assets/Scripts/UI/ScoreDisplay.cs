using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private void OnEnable()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int score = PlayerPrefs.GetInt("Score" + sceneName, 0);
        int highScore = PlayerPrefs.GetInt("HighScore" + sceneName, 0);

        _scoreText.text = "Your Score: " + score.ToString();
        _highScoreText.text = "High Score: " + highScore.ToString();
    }
}

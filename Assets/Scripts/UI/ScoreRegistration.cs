using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRegistration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI scoreText = GetComponent<TextMeshProUGUI>();
        EndGameManager.instance.RegisterScoreText(scoreText);
        scoreText.text = "Score: " + EndGameManager.instance._score;
    }
}

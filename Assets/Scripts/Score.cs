using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recordCounter;
    [SerializeField] private TextMeshProUGUI _scoreCounter;
    
    public void UpdateScore()
    {
        var currentScore = PlayerPrefs.GetInt("score");
        if (currentScore != Convert.ToInt32(_scoreCounter.text))
        {
            if (currentScore > Convert.ToInt32(_recordCounter.text))
            {
                PlayerPrefs.SetInt("record", currentScore);
                _recordCounter.text = currentScore.ToString();
            }

            PlayerPrefs.SetInt("score", currentScore);
            _scoreCounter.text = currentScore.ToString();
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("score", 0);
        _recordCounter.text = PlayerPrefs.GetInt("record").ToString();
    }

    private void FixedUpdate()
    {
        UpdateScore();
    }
}

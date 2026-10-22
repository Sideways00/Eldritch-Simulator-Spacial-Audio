using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            UpdateScore();
        }
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.totalScore.ToString();
    }   
}

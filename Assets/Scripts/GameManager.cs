using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreTxt;
    [SerializeField] private TMP_Text computerScoreTxt;
    [SerializeField] private int targetScore;

    private int _playerScore;
    private int _computerScore;

    void Start()
    {
        _playerScore = 0;
        _computerScore = 0;
        UpdateScoreTexts();
        targetScore = PlayerPrefs.GetInt("target");
    }

    public void PlayerScored()
    {
        _playerScore++;
        CheckForEndGame("You Win!");
    }

    public void ComputerScored()
    {
        _computerScore++;
        CheckForEndGame("You Lose!");
    }

    private void CheckForEndGame(string endText)
    {
        if (_playerScore >= targetScore || _computerScore >= targetScore)
        {
            PlayerPrefs.SetString("end", endText);
            SceneManager.LoadScene("EndScene");
        }
        UpdateScoreTexts();
    }

    private void UpdateScoreTexts()
    {
        playerScoreTxt.text = _playerScore.ToString();
        computerScoreTxt.text = _computerScore.ToString();
    }
}
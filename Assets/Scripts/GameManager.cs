using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreTxt;
    [SerializeField] private TMP_Text computerScoreTxt;
    [SerializeField] private int targetScore;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject playerPaddle;
    [SerializeField] private GameObject computerPaddle;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float powerUpSpawnInterval = 10f;
    [SerializeField] private float powerUpLifetime = 5f;
    [SerializeField] private float powerUpDuration = 5f;

    private int _playerScore;
    private int _computerScore;
    private readonly List<GameObject> _balls = new List<GameObject>();

    void Start()
    {
        _playerScore = 0;
        _computerScore = 0;
        UpdateScoreTexts();
        targetScore = PlayerPrefs.GetInt("target");
        _balls.Add(Instantiate(ballPrefab));  
        StartCoroutine(SpawnPowerUps());
    }

    public void PlayerScored(Ball ball)
    {
        HandleBallScored(ball, ref _playerScore);
        CheckForEndGame("You Win!");
    }

    public void ComputerScored(Ball ball)
    {
        HandleBallScored(ball, ref _computerScore);
        CheckForEndGame("You Lose!");
    }

    private void HandleBallScored(Ball ball, ref int score)
    {
        score++;
        if (_balls.Count > 1)
        {
            _balls.Remove(ball.gameObject);
            Destroy(ball.gameObject);
        }
        else
        {
            ball.ResetBall();
        }
        UpdateScoreTexts();
    }

    private void CheckForEndGame(string endText)
    {
        if (_playerScore >= targetScore || _computerScore >= targetScore)
        {
            PlayerPrefs.SetString("end", endText);
            SceneManager.LoadScene("EndScene");
        }
    }

    private void UpdateScoreTexts()
    {
        playerScoreTxt.text = _playerScore.ToString();
        computerScoreTxt.text = _computerScore.ToString();
    }

    public void ActivatePowerUp(PowerUp.PowerUpType powerUpType, GameObject powerUpObject)
    {
        switch (powerUpType)
        {
            case PowerUp.PowerUpType.IncreasePlayerPaddleSize:
                StartCoroutine(HandlePaddleSizeChange(playerPaddle, true));
                break;

            case PowerUp.PowerUpType.DecreasePlayerPaddleSize:
                StartCoroutine(HandlePaddleSizeChange(playerPaddle, false));
                break;

            case PowerUp.PowerUpType.IncreaseComputerPaddleSize:
                StartCoroutine(HandlePaddleSizeChange(computerPaddle, true));
                break;

            case PowerUp.PowerUpType.DecreaseComputerPaddleSize:
                StartCoroutine(HandlePaddleSizeChange(computerPaddle, false));
                break;

            case PowerUp.PowerUpType.FreezePlayerPaddle:
                StartCoroutine(HandleFreezePaddle(playerPaddle));
                break;

            case PowerUp.PowerUpType.FreezeComputerPaddle:
                StartCoroutine(HandleFreezePaddle(computerPaddle));
                break;

            case PowerUp.PowerUpType.InvisibleBall:
                _balls[0].GetComponent<Ball>().ActivateInvisibility(powerUpDuration);
                break;

            case PowerUp.PowerUpType.SplitBall:
                SplitBall();
                break;
        }

        Destroy(powerUpObject);
    }

    private void SplitBall()
    {
        GameObject newBall = Instantiate(ballPrefab, _balls[0].transform.position, Quaternion.identity);
        _balls.Add(newBall);
    }

    private IEnumerator HandlePaddleSizeChange(GameObject paddle, bool increaseSize)
    {
        var paddleScript = paddle.GetComponent<Paddle>();

        if (increaseSize)
        {
            paddleScript.IncreaseSize();
        }
        else
        {
            paddleScript.DecreaseSize();
        }

        yield return new WaitForSeconds(powerUpDuration);

        if (increaseSize)
        {
            paddleScript.DecreaseSize();
        }
        else
        {
            paddleScript.IncreaseSize();
        }
    }

    private IEnumerator HandleFreezePaddle(GameObject paddle)
    {
        var paddleScript = paddle.GetComponent<Paddle>();
        paddleScript.Freeze();

        yield return new WaitForSeconds(powerUpDuration);

        paddleScript.Unfreeze();
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);
            Vector2 spawnPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            GameObject spawnedPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyPowerUpAfterTime(spawnedPowerUp, powerUpLifetime));
        }
    }

    private IEnumerator DestroyPowerUpAfterTime(GameObject powerUp, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        if (powerUp != null)
        {
            Destroy(powerUp);
        }
    }
}

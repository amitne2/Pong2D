using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreTxt;
    [SerializeField] private TMP_Text computerScoreTxt;
    [SerializeField] private int targetScore;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject playerPaddle;
    [SerializeField] private GameObject computerPaddle;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float powerUpSpawnInterval = 10f;
    [SerializeField] private float powerUpLifetime = 7f;
    [SerializeField] private float powerUpDuration = 5f;

    private int _playerScore;
    private int _computerScore;

    void Start()
    {
        _playerScore = 0;
        _computerScore = 0;
        UpdateScoreTexts();
        targetScore = PlayerPrefs.GetInt("target");
        StartCoroutine(SpawnPowerUps());
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
                ball.GetComponent<Ball>().ActivateInvisibility(powerUpDuration);
                break;
        }

        Destroy(powerUpObject);
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

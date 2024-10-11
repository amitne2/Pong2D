using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreTxt;
    [SerializeField] private TMP_Text computerScoreTxt;
    [SerializeField] private int targetScore;

    // References to ball and paddles
    [SerializeField] private GameObject ball; // Assign the ball object here
    [SerializeField] private GameObject playerPaddle; // Assign the player paddle here
    [SerializeField] private GameObject computerPaddle; // Assign the computer paddle here

    // Power-up variables
    [SerializeField] private GameObject powerUpPrefab; // Assign your Power-Up prefab here
    [SerializeField] private float powerUpSpawnInterval = 10f; // Time between power-up spawns
    [SerializeField] private float powerUpLifetime = 7f; // How long power-up stays active if not collected
    [SerializeField] private float powerUpDuration = 5f; // Duration the power-up effect lasts after activation

    private int _playerScore;
    private int _computerScore;

    void Start()
    {
        _playerScore = 0;
        _computerScore = 0;
        UpdateScoreTexts();
        targetScore = PlayerPrefs.GetInt("target");
        StartCoroutine(SpawnPowerUps()); // Start the power-up spawning
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

            case PowerUp.PowerUpType.SplitBall:
                // ball.Split(); // Assuming you have a Split method in your Ball script
                break;
        }

        // Destroy the power-up object after activation
        Destroy(powerUpObject);
    }

    private IEnumerator HandlePaddleSizeChange(GameObject paddle, bool increaseSize)
    {
        var paddleScript = paddle.GetComponent<Paddle>();

        if (increaseSize)
        {
            paddleScript.IncreaseSize(); // Assuming IncreaseSize() method increases the paddle size
        }
        else
        {
            paddleScript.DecreaseSize(); // Assuming DecreaseSize() method decreases the paddle size
        }

        yield return new WaitForSeconds(powerUpDuration);

        if (increaseSize)
        {
            paddleScript.DecreaseSize(); // Reset to original size after duration
        }
        else
        {
            paddleScript.IncreaseSize(); // Reset to original size after duration
        }
    }

    private IEnumerator HandleFreezePaddle(GameObject paddle)
    {
        var paddleScript = paddle.GetComponent<Paddle>();
        paddleScript.Freeze(); // Freeze the paddle

        yield return new WaitForSeconds(powerUpDuration); // Wait for the duration of the freeze

        paddleScript.Unfreeze(); // Unfreeze the paddle
    }

    // Coroutine to spawn and manage power-ups
    private IEnumerator SpawnPowerUps()
    {
        while (true) // Keep spawning power-ups until the game ends
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);

            // Randomize power-up position within screen bounds
            Vector2 spawnPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f)); // Adjust bounds to fit your game

            // Instantiate power-up at random position
            GameObject spawnedPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            // Start the coroutine to destroy the power-up if not collected in time
            StartCoroutine(DestroyPowerUpAfterTime(spawnedPowerUp, powerUpLifetime));
        }
    }

    // Coroutine to destroy power-up after lifetime duration if not collected
    private IEnumerator DestroyPowerUpAfterTime(GameObject powerUp, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        // Destroy the power-up if it still exists (was not collected)
        if (powerUp != null)
        {
            Destroy(powerUp);
        }
    }
}

using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        IncreasePlayerPaddleSize,
        DecreasePlayerPaddleSize,
        FreezePlayerPaddle,
        IncreaseComputerPaddleSize,
        DecreaseComputerPaddleSize,
        FreezeComputerPaddle,
        SplitBall,
        InvisibleBall
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            PowerUpType randomPowerUp = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.ActivatePowerUp(randomPowerUp, gameObject);
        }
    }
}
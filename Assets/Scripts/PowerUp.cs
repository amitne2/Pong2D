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
        SplitBall
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the ball
        if (other.gameObject.name == "Ball")
        {
            // Choose a random power-up type
            PowerUpType randomPowerUp = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);

            // Get the GameManager instance and apply the power-up
            GameManager gameManager = FindObjectOfType<GameManager>();
            
            // Pass the random power-up and this gameObject to be destroyed after activation
            gameManager.ActivatePowerUp(randomPowerUp, gameObject);
        }
    }
}
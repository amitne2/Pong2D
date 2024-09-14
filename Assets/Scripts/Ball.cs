using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreTxt;
    [SerializeField] private TMP_Text computerScoreTxt;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float startingSpeed;
    [SerializeField] private float maxStartAngle;
    private float _speed;
    private Vector2 _direction;
    private int _playerScore;
    private int _computerScore;
    private int _targetScore;

    void Start()
    {
        _speed = startingSpeed;
        _targetScore = PlayerPrefs.GetInt("target");
        _playerScore = _computerScore = 0;
        playerScoreTxt.text = computerScoreTxt.text = "0";
        ResetBall();
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        float angle = CalcRandomAngle();
        dir.y = Mathf.Tan(angle * Mathf.Deg2Rad);
        dir = dir.normalized;
        rb.velocity = dir * _speed;
    }

    private float CalcRandomAngle()
    {
        float angle = 0;
        do
        {
            angle = Random.Range(-maxStartAngle, maxStartAngle);
        } while (angle is > -10f and < 10f); // We want that angle wont be to the left or to the right directly

        return angle;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Left")
        {
            UpdateScore(ref _computerScore, computerScoreTxt, "You Lose!");
        }
        else if (collision.gameObject.name == "Right")
        {
            UpdateScore(ref _playerScore, playerScoreTxt, "You Win!");
        }
        else if (collision.gameObject.name == "PlayerPaddle") 
        {
            IncreaseBallSpeed(); // Each time the player hits the ball it gets faster
        }
    }

    private void IncreaseBallSpeed()
    {
        // Increase the speed slightly
        _speed += 0.5f;

        // Keep the current direction of the ball and adjust velocity
        rb.velocity = rb.velocity.normalized * _speed;
    }
    
    private void UpdateScore(ref int score, TMP_Text scoreTxt, string endText)
    {
        score++;
        _speed = startingSpeed;
        if (_targetScore == score)
        {
            PlayerPrefs.SetString("end", endText);
            SceneManager.LoadScene("EndScene");
        }
        scoreTxt.text = score.ToString();
        ResetBall();
    }
}
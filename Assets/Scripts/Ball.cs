using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float startingSpeed;
    [SerializeField] private float maxStartAngle;
    private float _speed;
    private GameManager _gameManager;

    void Start()
    {
        _speed = startingSpeed;
        _gameManager = FindObjectOfType<GameManager>();
        ResetBall();
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        float angle = CalcRandomAngle();
        dir.y = Mathf.Tan(angle * Mathf.Deg2Rad);
        dir = dir.normalized;
        rb.velocity = dir * startingSpeed;
    }

    private float CalcRandomAngle()
    {
        float angle;
        do
        {
            angle = Random.Range(-maxStartAngle, maxStartAngle);
        } while (angle is > -10f and < 10f);

        return angle;
    }

    public void ActivateInvisibility(float duration)
    {
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(DeactivateInvisibility(duration));
    }

    private IEnumerator DeactivateInvisibility(float duration)
    {
        yield return new WaitForSeconds(duration);
        GetComponent<Renderer>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Left")
        {
            _gameManager.ComputerScored();
            ResetBall();
        }
        else if (collision.gameObject.name == "Right")
        {
            _gameManager.PlayerScored();
            ResetBall();
        }
        else if (collision.gameObject.name == "PlayerPaddle")
        {
            IncreaseBallSpeed();
        }
    }

    private void IncreaseBallSpeed()
    {
        _speed += 0.5f;
        rb.velocity = rb.velocity.normalized * _speed;
    }
}

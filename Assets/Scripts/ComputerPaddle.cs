using UnityEngine;

public class ComputerPaddle : Paddle
{
    [SerializeField] private GameObject ball;

    public override void Start()
    {
        base.Start();
        Speed = PlayerPrefs.GetFloat("speed", 3f);
    }

    void FixedUpdate()
    {
        MovePaddle();
    }

    public override void MovePaddle()
    {
        Vector2 ballPosition = ball.transform.position;
        float targetY = Mathf.Clamp(ballPosition.y, MinY, MaxY);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), Speed * Time.fixedDeltaTime);
        Rb.MovePosition(newPosition);
    }
}
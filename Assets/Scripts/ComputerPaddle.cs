using UnityEngine;

public class ComputerPaddle : Paddle
{
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
        GameObject closestBall = FindClosestBall();
        if (closestBall != null)
        {
            Vector2 ballPosition = closestBall.transform.position;
            float targetY = Mathf.Clamp(ballPosition.y, MinY, MaxY);
            Vector2 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), Speed * Time.fixedDeltaTime);
            Rb.MovePosition(newPosition);
        }
    }

    private GameObject FindClosestBall()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        GameObject closestBall = null;
        float closestDistance = Mathf.Infinity;

        foreach (Ball ball in balls)
        {
            float distanceToBall = Vector2.Distance(transform.position, ball.transform.position);
            if (distanceToBall < closestDistance)
            {
                closestDistance = distanceToBall;
                closestBall = ball.gameObject;
            }
        }

        return closestBall; 
    }
}
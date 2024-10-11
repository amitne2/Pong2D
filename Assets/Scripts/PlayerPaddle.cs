using UnityEngine;

public class PlayerPaddle : Paddle
{
    [SerializeField] private float speed;

    public override void Start()
    {
        base.Start();
        Speed = speed;
    }

    void Update()
    {
        MovePaddle();
    }

    public override void MovePaddle()
    {
        float input = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(0, input * Speed);
        Rb.velocity = movement;

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, MinY, MaxY);
        transform.position = pos;
    }
}
using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private float _paddleHeight;
    private float _minY, _maxY;

    void Start()
    {
        _paddleHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
        _minY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + _paddleHeight;
        _maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - _paddleHeight;
    }

    void Update()
    {
        float input = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(0, input * speed);

        rb.velocity = movement;

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, _minY, _maxY);
        transform.position = pos;
    }
}
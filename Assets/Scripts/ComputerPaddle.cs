using UnityEngine;

public class ComputerPaddle : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private GameObject ball; 
    [SerializeField] private Rigidbody2D rb;
    private float _paddleHeight;
    private float _minY, _maxY;

    void Start()
    {
        _paddleHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
        _minY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + _paddleHeight;
        _maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - _paddleHeight;
    }

    void FixedUpdate()
    {
        Vector2 ballPosition = ball.transform.position;
        
        float targetY = Mathf.Clamp(ballPosition.y, _minY, _maxY);
        
        Vector2 newPosition = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), speed * Time.fixedDeltaTime);
        
        rb.MovePosition(newPosition);
    }
}
using UnityEngine;

public abstract class Paddle : MonoBehaviour
{
    public float Speed { get; set; }
    public Rigidbody2D Rb { get; set; }
    public float PaddleHeight { get; set; }
    public float MinY { get; set; }
    public float MaxY { get; set; }

    [SerializeField] protected float increaseAmount = 0.5f; 
    [SerializeField] protected float decreaseAmount = 0.5f; 

    private float originalSpeed; 

    public virtual void Start()
    {
        PaddleHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
        MinY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + PaddleHeight;
        MaxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - PaddleHeight;
        Rb = GetComponent<Rigidbody2D>();
    }

    public abstract void MovePaddle();

    public void IncreaseSize()
    {
        Vector3 newSize = transform.localScale;
        newSize.y += increaseAmount;
        transform.localScale = newSize;
        PaddleHeight = GetComponent<BoxCollider2D>().bounds.extents.y; 
    }

    public void DecreaseSize()
    {
        Vector3 newSize = transform.localScale;
        newSize.y = Mathf.Max(newSize.y - decreaseAmount, 0.1f); 
        transform.localScale = newSize;
        PaddleHeight = GetComponent<BoxCollider2D>().bounds.extents.y; 
    }

   
    public void Freeze()
    {
        originalSpeed = Speed;
        Speed = 0; 
    }

    public void Unfreeze()
    {
        Speed = originalSpeed; 
    }
}

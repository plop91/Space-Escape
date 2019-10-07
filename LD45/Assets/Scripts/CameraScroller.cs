using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CameraScroller : MonoBehaviour
{
    public bool canMove = true;
    private int speed;
    private Player player;
    private Vector2 playerMove;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        playerMove = player.movement;
        speed = player.scrollSpeed;
    }
    private void Update()
    {
        playerMove = player.movement;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + ((playerMove *player.speed)+ new Vector2(speed, 0)) * Time.fixedDeltaTime);
        }
    }
}

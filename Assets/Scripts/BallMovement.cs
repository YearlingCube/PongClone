using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
	private Rigidbody2D rb;
    private GameManger GM;
    public Vector2 velocity { get; private set; }

	[SerializeField] private float Speed = 12;

    [SerializeField] private float MaxBounceAngle = 45;

    [SerializeField] private float ServeAngle = 45;

    [SerializeField] private float ResetTime;

    private bool overridePosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManger>();
        Serve(Paddle.Side.Left);
    }

    private void FixedUpdate()
    {
        if (!overridePosition)
        rb.velocity = velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Paddle")
        {
            BounceFromPaddle(collision.collider);
        }
        else
        {
        Bounce();
        }
        if (!GM.inMenu)
            FindObjectOfType<AudioManager>().Play("Bounce");
    }
    private void Serve(Paddle.Side side)
    {
        Vector2 serveDirection = new Vector2(Mathf.Cos(ServeAngle * Mathf.Deg2Rad), Mathf.Sin(ServeAngle * Mathf.Deg2Rad));
        serveDirection.y = -serveDirection.y;
        if (side == Paddle.Side.Left)
            serveDirection.x = -serveDirection.x;

        velocity = serveDirection * Speed;
    }
    private void Bounce()
    {
        velocity = new Vector2(velocity.x, -velocity.y);
    }
    private void BounceFromPaddle(Collider2D collider)
    {
        float colYExtent = collider.bounds.extents.y;
        float YOffset = transform.position.y - collider.transform.position.y;

        float yRatio = YOffset / colYExtent;
        float BounceAngle = MaxBounceAngle * yRatio * Mathf.Deg2Rad;

        Vector2 BounceDirection = new Vector2(Mathf.Cos(BounceAngle), Mathf.Sin(BounceAngle));

        BounceDirection.x *= Mathf.Sign(-velocity.x);
        velocity = BounceDirection * Speed;
    }
    public void ResetBall(Paddle.Side side)
    {
        StartCoroutine(ResetRoutine(side));
    }
    private IEnumerator ResetRoutine(Paddle.Side side)
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        overridePosition = true;

        yield return new WaitForSeconds(ResetTime);

        overridePosition = false;
        Serve(side);
    }
}

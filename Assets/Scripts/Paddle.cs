using System.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isAi = false;

    private BallMovement Ball;
    private BoxCollider2D col;

    private float RandomYOffset;

    public float Speed;

    private Vector2 fowardDirection;
    private bool firstIncoming;

    public enum Side { Left, Right }
    [SerializeField] private Side side;

    [SerializeField] private float ResetTime;
    private bool OverridePostion;

    private void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
        col = GetComponent<BoxCollider2D>();

        if (side == Side.Left)
            fowardDirection = Vector2.right;
        else if (side == Side.Right)
            fowardDirection = Vector2.left;
    }
    void Update()
    {
        if (!OverridePostion)
            MovePaddle();
    }
    private void MovePaddle()
    {
        float targetYPosition = GetNewYPosition();

        ClampPosition(ref targetYPosition);

        transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
    }
    private void ClampPosition(ref float yPosition)
    {
        float minY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;

        yPosition = Mathf.Clamp(yPosition, minY, maxY);
    }
    private float GetNewYPosition()
    {
        float result = transform.position.y;
        if (isAi)
        {
            if (BallIncoming())
            {
                if (firstIncoming)
                {
                    firstIncoming = false;
                    RandomYOffset = GetRandomOffset();
                }

                result = Mathf.MoveTowards(transform.position.y, Ball.transform.position.y + RandomYOffset, Speed * Time.deltaTime);
            }
            else
            {
                firstIncoming = true;
            }
        }
        else
        {
            float movement = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime;
            result = transform.position.y + movement;
        }
        return result;
    }
    private bool BallIncoming()
    {
        float dotP = Vector2.Dot(Ball.velocity, fowardDirection);
        return dotP < 0f;
    }
    private float GetRandomOffset()
    {
        float maxOffset = col.bounds.extents.y;
        return Random.Range(-maxOffset, maxOffset);
    }
    public void Reset()
    {
        StartCoroutine(ResetRoutine());
    }
    private IEnumerator ResetRoutine()
    {
        OverridePostion = true;
        float startPosition = transform.position.y;
        for(float timer = 0; timer > ResetTime; timer += Time.deltaTime)
        {
            float TargetPosition = Mathf.Lerp(startPosition, 0f, timer / ResetTime);
            transform.position = new Vector3(transform.position.x, TargetPosition, transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        OverridePostion = false;
    }
}

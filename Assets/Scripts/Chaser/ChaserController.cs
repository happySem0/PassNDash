using UnityEngine;

/// <summary>
/// Controls the behavior of a chaser GameObject.
/// Chasers chase the ball on their side of the field and trigger game over on collision.
/// </summary>
public class ChaserController : MonoBehaviour
{
    [Tooltip("Reference to the BallController in the scene.")]
    public BallController ball;

    [Tooltip("Reference to the player on this side.")]
    public BallPasser player;

    [Tooltip("Defines the bounds of this chaser's area (side of the field).")]
    public Collider areaBounds;

    [Tooltip("Speed at which the chaser moves.")]
    public float chaseSpeed = 5f;

    [Tooltip("If true, chaser will wander instead of chase.")]
    public bool isWandering = false;

    private Vector3 wanderTarget;

    // Cached reference to the GameManager singleton
    private GameManager gameManager;

    /// <summary>
    /// Unity's Start method is called before the first frame update.
    /// Here, we cache the GameManager instance for efficiency and clarity.
    /// </summary>
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        // If chaser is set to wander, move randomly within area bounds
        if (isWandering)
        {
            Wander();
        }
        else
        {
            // Only chase if the ball is within this chaser's area
            if (areaBounds.bounds.Contains(ball.transform.position))
            {
                ChaseBall();
            }
            else
            {
                // If ball is not on this side, start wandering
                isWandering = true;
                SetNewWanderTarget();
            }
        }
    }

    /// <summary>
    /// Moves the chaser towards the ball's position.
    /// </summary>
    private void ChaseBall()
    {
        Vector3 direction = (ball.transform.position - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * chaseSpeed * Time.deltaTime;

        // Clamp position to stay within area bounds
        newPosition = ClampPositionToBounds(newPosition);
        transform.position = newPosition;
    }

    /// <summary>
    /// Moves the chaser towards a random point within its area.
    /// </summary>
    private void Wander()
    {
        // If close to the wander target, pick a new one
        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
        {
            SetNewWanderTarget();
        }

        Vector3 direction = (wanderTarget - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * chaseSpeed * Time.deltaTime;
        newPosition = ClampPositionToBounds(newPosition);
        transform.position = newPosition;
    }

    /// <summary>
    /// Picks a new random target within the area for wandering.
    /// </summary>
    private void SetNewWanderTarget()
    {
        Bounds bounds = areaBounds.bounds;
        wanderTarget = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y, // Keep chaser on ground
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// Ensures the chaser stays within its assigned area.
    /// </summary>
    private Vector3 ClampPositionToBounds(Vector3 position)
    {
        Bounds bounds = areaBounds.bounds;
        return new Vector3(
            Mathf.Clamp(position.x, bounds.min.x, bounds.max.x),
            position.y,
            Mathf.Clamp(position.z, bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// Handles collision with the ball or player.
    /// If chaser collides with either, triggers game over.
    /// </summary>
    /// <param name="other">The collider this chaser collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball.gameObject)
        {
            // Notify the GameManager that the game is over
            gameManager.GameOver("Chaser caught the ball!");
        }
        else if (other.gameObject == player.gameObject && player.HasBall())
        {
            gameManager.GameOver("Chaser caught the player with the ball!");
        }
    }
}
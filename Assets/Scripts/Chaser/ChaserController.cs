using UnityEngine;

/// <summary>
/// Controls the behavior of a chaser GameObject.
/// The chaser stays within its assigned area, chases the ball if the ball is in that area,
/// and wanders randomly if the ball is not in the area. If the chaser collides with the ball
/// or the player holding the ball, the game ends.
/// </summary>
public class ChaserController : MonoBehaviour
{
    [Tooltip("Reference to the BallController in the scene. This is the ball the chaser will chase.")]
    public BallController ball;

    [Tooltip("Reference to the player on this side. Used to check if the player has the ball.")]
    public BallPasser player;

    [Tooltip("Defines the bounds of this chaser's area (side of the field). The chaser never leaves this area.")]
    public Collider areaBounds;

    [Tooltip("Speed at which the chaser moves when chasing or wandering.")]
    public float chaseSpeed = 5f;

    // This variable tracks whether the chaser is currently wandering (true) or chasing the ball (false).
    [Tooltip("If true, chaser will wander instead of chase.")]
    public bool isWandering = false;

    // The current target position for wandering. When the chaser is wandering, it moves toward this point.
    private Vector3 wanderTarget;

    // Cached reference to the GameManager singleton for triggering game over.
    private GameManager gameManager;

    /// <summary>
    /// Unity's Start method is called before the first frame update.
    /// Here, we cache the GameManager instance for efficiency and clarity.
    /// </summary>
    private void Start()
    {
        // GameManager.Instance is a common Unity singleton pattern for accessing global managers.
        gameManager = GameManager.Instance;
    }

    /// <summary>
    /// Unity's Update method is called once per frame.
    /// This method decides whether the chaser should chase the ball or wander,
    /// based on whether the ball is inside the chaser's area.
    /// </summary>
    private void Update()
    {
        // Check if the ball is inside this chaser's area using only the X and Z (horizontal) position.
        // This ignores the Y (vertical) position, so the ball can be high in the air and still be considered "in area".
        Vector3 ballPos = ball.transform.position;
        Bounds bounds = areaBounds.bounds;

        // Create a new Vector3 for the ball's horizontal position, using the area's Y center.
        Vector3 ballHorizontal = new Vector3(ballPos.x, bounds.center.y, ballPos.z);

        // Check if the horizontal position is within the area's bounds.
        bool ballInArea = bounds.Contains(ballHorizontal);

        if (ballInArea)
        {
            // Ball is in the area: chase it.
            isWandering = false;
            ChaseBall();
        }
        else
        {
            // Ball is not in the area: wander randomly.
            if (!isWandering)
            {
                isWandering = true;
                SetNewWanderTarget();
            }
            Wander();
        }
    }

    /// <summary>
    /// Moves the chaser toward the ball's position, but keeps it inside its area.
    /// This method is called only if the ball is inside the chaser's area.
    /// </summary>
    private void ChaseBall()
    {
        // Calculate the direction from the chaser to the ball and normalize it to get a unit vector.
        Vector3 direction = (ball.transform.position - transform.position).normalized;
        // Calculate the new position by moving in the direction of the ball at the specified speed.
        Vector3 newPosition = transform.position + direction * chaseSpeed * Time.deltaTime;
        // Clamp the new position so the chaser never leaves its assigned area.
        newPosition = ClampPositionToBounds(newPosition);
        // Move the chaser to the new position.
        transform.position = newPosition;
    }

    /// <summary>
    /// Moves the chaser toward a random point within its area.
    /// If the chaser is close to its wander target, pick a new one.
    /// This method is called only if the ball is not in the chaser's area.
    /// </summary>
    private void Wander()
    {
        // If the chaser is close to its wander target, pick a new random target in the area.
        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
        {
            SetNewWanderTarget();
        }
        // Calculate the direction to the wander target.
        Vector3 direction = (wanderTarget - transform.position).normalized;
        // Move toward the wander target at the specified speed.
        Vector3 newPosition = transform.position + direction * chaseSpeed * Time.deltaTime;
        // Clamp the new position so the chaser never leaves its assigned area.
        newPosition = ClampPositionToBounds(newPosition);
        // Move the chaser to the new position.
        transform.position = newPosition;
    }

    /// <summary>
    /// Picks a new random target within the area for wandering.
    /// This ensures the chaser always stays within its assigned area.
    /// </summary>
    private void SetNewWanderTarget()
    {
        // Get the bounds of the assigned area collider.
        Bounds bounds = areaBounds.bounds;
        // Pick a random point within the bounds for the chaser to wander toward.
        wanderTarget = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y, // Keep chaser on the ground (do not change Y)
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// Ensures the chaser stays within its assigned area by clamping its position.
    /// This prevents the chaser from crossing the boundary of its area.
    /// </summary>
    /// <param name="position">The position to clamp.</param>
    /// <returns>A position clamped within the area bounds.</returns>
    private Vector3 ClampPositionToBounds(Vector3 position)
    {
        // Get the bounds of the assigned area collider.
        Bounds bounds = areaBounds.bounds;
        // Clamp the X and Z coordinates to stay within the bounds.
        return new Vector3(
            Mathf.Clamp(position.x, bounds.min.x, bounds.max.x),
            position.y, // Keep the original Y position (height)
            Mathf.Clamp(position.z, bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// Unity's OnTriggerEnter is called when this GameObject's collider (set as "Is Trigger")
    /// enters another collider. If the chaser collides with the ball or the player holding the ball,
    /// the game ends by calling GameManager.GameOver.
    /// </summary>
    /// <param name="other">The collider this chaser collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        // If the chaser collides with the ball, trigger game over.
        if (other.gameObject == ball.gameObject)
        {
            // This line ends the game if the chaser catches the ball.
            gameManager.GameOver("Chaser caught the ball!");
        }
        // If the chaser collides with the player and the player has the ball, trigger game over.
        else if (other.gameObject == player.gameObject)
        {
            // Check if the player has the ball.
            if (player.HasBall())
            {
                // This line ends the game if the chaser catches the player holding the ball.
                gameManager.GameOver("Chaser caught the player with the ball!");
            }
        }
    }
}
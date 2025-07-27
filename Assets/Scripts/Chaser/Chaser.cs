using UnityEngine;

/// <summary>
/// Contains the core logic for chaser behavior (chasing, wandering, and bounds checking).
/// This class is independent of Unity's MonoBehaviour and can be tested separately.
/// </summary>
public class Chaser
{
    private BallController ball;      // Reference to the ball to chase
    private BallPasser player;        // Reference to the player (for future extension)
    private Collider areaBounds;      // The area the chaser is allowed to move in
    private float chaseSpeed;         // How fast the chaser moves
    private Vector3 wanderTarget;     // Current target for wandering
    public bool IsWandering { get; private set; } // Whether the chaser is wandering

    /// <summary>
    /// Constructor for the Chaser logic class.
    /// </summary>
    /// <param name="ball">Reference to the BallController (the ball to chase).</param>
    /// <param name="player">Reference to the BallPasser (the player on this side).</param>
    /// <param name="areaBounds">Collider defining the chaser's allowed area.</param>
    /// <param name="chaseSpeed">Speed at which the chaser moves.</param>
    public Chaser(BallController ball, BallPasser player, Collider areaBounds, float chaseSpeed)
    {
        this.ball = ball;
        this.player = player;
        this.areaBounds = areaBounds;
        this.chaseSpeed = chaseSpeed;
        this.IsWandering = false;
        this.wanderTarget = Vector3.zero;
    }

    /// <summary>
    /// Decides whether to chase the ball or wander, and updates the chaser's position accordingly.
    /// </summary>
    /// <param name="chaserTransform">The transform of the chaser GameObject.</param>
    public void Tick(Transform chaserTransform)
    {
        Vector3 ballPos = ball.transform.position;
        Bounds bounds = areaBounds.bounds;
        Vector3 ballHorizontal = new Vector3(ballPos.x, bounds.center.y, ballPos.z);
        bool ballInArea = bounds.Contains(ballHorizontal);

        if (ballInArea)
        {
            // Ball is in the area: chase it.
            IsWandering = false;
            ChaseBall(chaserTransform);
        }
        else
        {
            // Ball is not in the area: wander randomly.
            if (!IsWandering)
            {
                IsWandering = true;
                SetNewWanderTarget(chaserTransform.position);
            }
            Wander(chaserTransform);
        }
    }

    /// <summary>
    /// Moves the chaser toward the ball's position, keeping it inside its area.
    /// </summary>
    /// <param name="chaserTransform">The transform of the chaser GameObject.</param>
    private void ChaseBall(Transform chaserTransform)
    {
        Vector3 direction = (ball.transform.position - chaserTransform.position).normalized;
        Vector3 newPosition = chaserTransform.position + direction * chaseSpeed * Time.deltaTime;
        newPosition = ClampPositionToBounds(newPosition);
        chaserTransform.position = newPosition;
    }

    /// <summary>
    /// Moves the chaser toward a random point within its area.
    /// If the chaser is close to its wander target, pick a new one.
    /// </summary>
    /// <param name="chaserTransform">The transform of the chaser GameObject.</param>
    private void Wander(Transform chaserTransform)
    {
        if (Vector3.Distance(chaserTransform.position, wanderTarget) < 0.5f)
        {
            SetNewWanderTarget(chaserTransform.position);
        }
        Vector3 direction = (wanderTarget - chaserTransform.position).normalized;
        Vector3 newPosition = chaserTransform.position + direction * chaseSpeed * Time.deltaTime;
        newPosition = ClampPositionToBounds(newPosition);
        chaserTransform.position = newPosition;
    }

    /// <summary>
    /// Picks a new random target within the area for wandering.
    /// </summary>
    /// <param name="currentPosition">The current position of the chaser.</param>
    private void SetNewWanderTarget(Vector3 currentPosition)
    {
        Bounds bounds = areaBounds.bounds;
        wanderTarget = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            currentPosition.y, // Keep chaser on the ground
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// Ensures the chaser stays within its assigned area by clamping its position.
    /// </summary>
    /// <param name="position">The position to clamp.</param>
    /// <returns>A position clamped within the area bounds.</returns>
    private Vector3 ClampPositionToBounds(Vector3 position)
    {
        Bounds bounds = areaBounds.bounds;
        return new Vector3(
            Mathf.Clamp(position.x, bounds.min.x, bounds.max.x),
            position.y,
            Mathf.Clamp(position.z, bounds.min.z, bounds.max.z)
        );
    }
}
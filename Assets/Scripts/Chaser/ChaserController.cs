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

    // The Chaser logic class instance
    private Chaser chaser;

    // Cached reference to the GameManager singleton for triggering game over.
    private GameManager gameManager;

    /// <summary>
    /// Unity's Start method is called before the first frame update.
    /// Here, we cache the GameManager instance and initialize the Chaser logic class.
    /// </summary>
    private void Start()
    {
        gameManager = GameManager.Instance;
        // Initialize the Chaser logic class with references and parameters.
        chaser = new Chaser(ball, player, areaBounds, chaseSpeed);
    }

    /// <summary>
    /// Unity's Update method is called once per frame.
    /// Delegates the chaser's movement logic to the Chaser class.
    /// </summary>
    private void Update()
    {
        chaser.Tick(transform);
    }

    /// <summary>
    /// Unity's OnTriggerEnter is called when this GameObject's collider (set as "Is Trigger")
    /// enters another collider. If the chaser collides with the ball or the player holding the ball,
    /// the game ends by calling GameManager.GameOver.
    /// </summary>
    /// <param name="other">The collider this chaser collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball.gameObject)
        {
            gameManager.GameOver("Chaser caught the ball!");
        }
        else if (other.gameObject == player.gameObject)
        {
            if (player.HasBall())
            {
                gameManager.GameOver("Chaser caught the player with the ball!");
            }
        }
    }
}
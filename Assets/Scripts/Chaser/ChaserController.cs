using UnityEngine;

/// <summary>
/// This MonoBehaviour script should be attached to the Chaser GameObject in Unity.
/// It acts as a bridge between Unity's event system and the core Chaser logic.
/// Exposes stamina settings to the Unity Inspector for easy tweaking.
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

    [Header("Chaser Stamina Settings")]
    [Tooltip("Maximum stamina value the chaser can have.")]
    public float maxStamina = 100f;

    [Tooltip("Amount of stamina lost per second while chasing.")]
    public float staminaDrainPerSecond = 1f;

    private Chaser chaser; // Instance of the core logic class
    private GameManager gameManager; // Reference to the GameManager singleton

    /// <summary>
    /// Unity's Start method is called before the first frame update.
    /// Here, we cache the GameManager instance and initialize the Chaser logic class,
    /// passing in the stamina settings from the Inspector.
    /// </summary>
    private void Start()
    {
        gameManager = GameManager.Instance;

        // Create the Chaser logic instance and set stamina properties from Inspector
        chaser = new Chaser(ball, player, areaBounds, chaseSpeed)
        {
            MaxStamina = maxStamina,
            StaminaDrainPerSecond = staminaDrainPerSecond,
            Stamina = maxStamina // Start with full stamina
        };
    }

    /// <summary>
    /// Unity's Update method is called once per frame.
    /// Delegates the chaser's movement and stamina logic to the Chaser class.
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

    /// <summary>
    /// Returns the current stamina value of the chaser.
    /// This method allows UI scripts to access the chaser's stamina in a safe and encapsulated way,
    /// without exposing the internal Chaser logic class directly.
    /// </summary>
    /// <returns>
    /// The current stamina value as a float. If the chaser logic is not initialized, returns 0.
    /// </returns>
    public float GetCurrentStamina()
    {
        // Check if the chaser logic instance exists before accessing its stamina value.
        // This prevents null reference errors if the chaser is not yet initialized.
        return chaser != null ? chaser.Stamina : 0f;
    }
}
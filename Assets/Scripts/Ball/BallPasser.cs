using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

/// <summary>
/// Handles passing the ball between players in a Unity scene.
/// Attach this script to each player GameObject.
/// </summary>
public class BallPasser : MonoBehaviour
{
    [Tooltip("The teammate to pass the ball to.")]
    public Transform teammate;

    [Tooltip("Reference to the BallController script on the ball.")]
    public BallController ball;

    [Tooltip("Input Action for passing the ball. Assign this in the Inspector.")]
    public InputActionReference passAction;

    [Tooltip("Force applied to the ball when passing.")]
    public float passForce = 10f;

    // Tracks if this player currently has the ball
    private bool hasBall = false;

    /// <summary>
    /// Called to give this player the ball.
    /// </summary>
    public void GiveBall()
    {
        if (!hasBall)
        {
            hasBall = true;
            ball.AttachToPlayer(transform); // Attach ball to this player
        }
    }

    /// <summary>
    /// Called when this player receives the ball from another player.
    /// </summary>
    /// <param name="ball">The BallController instance being received.</param>
    public void ReceiveBall(BallController ball)
    {
        hasBall = true;
        this.ball = ball; // Update the reference to the ball
        ball.AttachToPlayer(transform); // Attach ball to this player
    }

    /// <summary>
    /// Called to remove the ball from this player.
    /// </summary>
    public void TakeBallAway()
    {
        hasBall = false;
    }

    /// <summary>
    /// Returns true if this player currently has the ball.
    /// </summary>
    public bool HasBall()
    {
        return hasBall;
    }

    /// <summary>
    /// Unity method called when the script is enabled.
    /// Enables the input action for passing.
    /// </summary>
    private void OnEnable()
    {
        if (passAction != null)
            passAction.action.Enable(); // Enable the input action so it can detect input
    }

    /// <summary>
    /// Unity method called when the script is disabled.
    /// Disables the input action to avoid errors.
    /// </summary>
    private void OnDisable()
    {
        if (passAction != null)
            passAction.action.Disable(); // Disable the input action when not needed
    }

    /// <summary>
    /// Unity method called every frame.
    /// Checks for pass input using the new Input System and passes the ball if possible.
    /// </summary>
    void Update()
    {
        // Check if this player has the ball, a teammate is assigned, and the pass action was triggered this frame
        if (hasBall && teammate != null && passAction.action.WasPressedThisFrame())
        {
            Debug.Log($"{gameObject.name} is trying to pass the ball to {teammate.name}"); // Debug log

            // Calculate the direction from this player to the teammate
            Vector3 passDir = (teammate.position - transform.position).normalized;

            // Detach the ball and apply force in the direction of the teammate
            ball.DetachFromPlayer(passDir, passForce);

            // This player no longer has the ball
            TakeBallAway();
        }
    }
}

using UnityEngine;

/// <summary>
/// Controls the ball's attachment to and detachment from players.
/// Attach this script to the ball GameObject.
/// </summary>
public class BallController : MonoBehaviour
{
    // Reference to the Rigidbody component, which controls physics for the ball
    private Rigidbody rb;

    // Called when the script instance is being loaded
    void Awake()
    {
        // Get the Rigidbody component attached to the ball.
        // Rigidbody is a Unity component that allows GameObjects to be affected by physics.
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Attaches the ball to a player's transform.
    /// This disables physics so the ball stays with the player.
    /// </summary>
    /// <param name="playerTransform">The transform of the player to attach to.</param>
    public void AttachToPlayer(Transform playerTransform)
    {

        // Set the ball as a child of the player so it moves with them.
        // Parenting in Unity means the ball's position will follow the player's position.
        transform.parent = playerTransform;

        // Optionally, position the ball relative to the player (adjust as needed).
        // Here, we place the ball above the player.
        // transform.localPosition = Vector3.up;

        transform.localPosition = new Vector3(0, 1.5f, 0); // Example: 1.5 units above the player's pivot


        // Disable physics so the ball doesn't move on its own.
        // Setting isKinematic to true means the ball will not be affected by Unity's physics engine.
        rb.isKinematic = true;


    }

    /// <summary>
    /// Detaches the ball from the player and applies a force to pass it.
    /// This enables physics so the ball can move.
    /// </summary>
    /// <param name="direction">The direction to pass the ball.</param>
    /// <param name="force">The amount of force to apply.</param>
    public void DetachFromPlayer(Vector3 direction, float force)
    {
        // Unparent the ball so it is no longer attached to the player.
        // This allows the ball to move independently.
        transform.parent = null;

        // Enable physics so the ball can move.
        // Setting isKinematic to false means the ball will be affected by Unity's physics engine.
        rb.isKinematic = false;

        // Reset velocity before applying new force to avoid unexpected movement.
        rb.linearVelocity = Vector3.zero;

        // Apply an impulse force to throw/pass the ball.
        // AddForce with ForceMode.Impulse instantly applies a force to the Rigidbody.
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}

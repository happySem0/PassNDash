using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform carrier; // player currently holding the ball
    public Vector3 carryOffset = new Vector3(0, 1f, 0.5f); // position in front of the player

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (carrier != null)
        {
            // Follow the carrier smoothly
            Vector3 targetPosition = carrier.position + carrier.forward * carryOffset.z + Vector3.up * carryOffset.y;
            rb.MovePosition(targetPosition);
        }
    }



    public void AttachToPlayer(Transform player)
    {
        Debug.Log("Ball attached to: " + player.name);
        carrier = player;
        rb.isKinematic = true;
        rb.freezeRotation = true;
        rb.angularVelocity = Vector3.zero;
    }

    public void DetachFromPlayer(Vector3 passDirection, float passForce)
    {
        Debug.Log("Ball detached.");
        carrier = null;
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(passDirection.normalized * passForce, ForceMode.Impulse);
    }

    public bool IsCarried() => carrier != null;
}

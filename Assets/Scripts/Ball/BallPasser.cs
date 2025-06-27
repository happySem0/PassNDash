using UnityEngine;

public class BallPasser : MonoBehaviour
{
    public Transform teammate;
    public BallController ball;
    public KeyCode passKey = KeyCode.Space;
    public float passForce = 10f;

    private bool hasBall = false;

    public void GiveBall()
    {
        if (!hasBall)
        {
            hasBall = true;
            ball.AttachToPlayer(transform);
        }
    }

    public void ReceiveBall(BallController ball)
    {
        // Set a flag, parent the ball, or update state to indicate this player has the ball
        // Example:
        hasBall = true;
        this.ball = ball; // Update the reference!
        ball.AttachToPlayer(transform); // Use your existing attach logic
    }

    public void TakeBallAway()
    {
        hasBall = false;
    }

    void Update()
    {
        if (hasBall && Input.GetKeyDown(passKey))
        {
            Vector3 passDir = (teammate.position - transform.position).normalized;
            ball.DetachFromPlayer(passDir, passForce);
            TakeBallAway();
        }
    }
}

using UnityEngine;

public class BallPickup : MonoBehaviour
{
    private BallController ball;

    void Awake()
    {
        ball = GetComponent<BallController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        BallPasser passer = other.GetComponent<BallPasser>();
        if (passer != null)
        {
            passer.ReceiveBall(ball);
        }
    }
}
